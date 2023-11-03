using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportManagement.Models;

namespace TransportManagement
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        secoffEntities EDX = new secoffEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (HttpContext.Current.Session["LoginUserId_TM"] != null)
            {
                LoanMenu();
                if (!Page.IsPostBack)
                {
                    string userID = Convert.ToString(HttpContext.Current.Session["LoginUserId_TM"]);
                    string UserName = Convert.ToString(HttpContext.Current.Session["LoginUserName_TM"]);
                    spnLoginas.InnerText = UserName;
                    if (userID == "")
                    {
                        Response.Redirect("~/Login.aspx");
                    }
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            try
            { 

                string myKey = System.Configuration.ConfigurationManager.AppSettings["CoreApplication"];
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                System.Net.Http.HttpResponseMessage response = client.GetAsync(myKey + "/Session/SessionStore/api/ClearSession").Result;
                Session.Clear();
                Session.Abandon();
                
                if (response.IsSuccessStatusCode)
                {
                }
                var res = EDX.sp_UserLogOut(Utilities.GetLoginUserID()).SingleOrDefault();
                string Id = Session["LoginUserId_TM"].ToString();
            }
            catch (Exception ex)
            {
                Response.Redirect("login.aspx");
            }
            finally
            {
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
                Response.Redirect("login.aspx");
            }
        }

        private void LoanMenu()
        {
            string loginUserID = Convert.ToString(Session["LoginUserId_TM"]);

            string menuLit = "";

            if (Session["MenuData"] != null)
            {
                menuLit = Convert.ToString(Session["MenuData"]);
            }
            else
            {
                var menuData = EDX.sp_GetMenuNew(loginUserID).ToList();
                Session["UserMenuData"] = menuData;

                var rootMenu = menuData.Where(b => b.IsRoot == true).OrderBy(o => o.ItemOrder).ToList();

                //Arbaz Work Start 
                var checkMVC = menuData.Where(b => b.IsMVC == true).ToList();
                //End

                StringBuilder sbMenuHtml = new StringBuilder();

                foreach (var root in rootMenu)
                {
                    if (!HaveAnyFormInRoot(menuData, root.FormId))
                        continue;

                    sbMenuHtml.Append("<ul class='navbar-nav '>");
                    sbMenuHtml.Append("<li class='nav-item dropdown active'>");
                    sbMenuHtml.Append("<a class='nav-link dropdown-toggle' href='#" + root.FormId + "' id='navbarDropdownMenuLink' data-toggle='dropdown' aria-haspopup='true' aria-expanded='false'>");
                    sbMenuHtml.Append(root.FormDescription);
                    sbMenuHtml.Append("</a>");

                    var menuItems = menuData.Where(b => b.ParentItem == root.FormId).OrderBy(o => o.ItemOrder).ToList();

                    sbMenuHtml.Append("<ul class='dropdown-menu' aria-labelledby='navbarDropdownMenuLink'>");
                    foreach (var menuItem in menuItems)
                    {
                        if (menuItem.IsParent == true)
                        {
                            if (menuData.Count(b => b.ParentItem == menuItem.FormId) == 0)
                                continue;

                            sbMenuHtml.Append(
                                    string.Format(
                                        "<li class='dropdown-submenu'><a class='dropdown-item dropdown-toggle' data-toggle='dropdown' href='#'>{0}</a>", menuItem.FormDescription
                                    )
                                );
                            sbMenuHtml.Append("<ul class='dropdown-menu'>");

                            var subMenus = menuData.Where(b => b.ParentItem == menuItem.FormId).OrderBy(o => o.ItemOrder).ToList();
                            foreach (var subMenu in subMenus)
                            {
                                sbMenuHtml.Append
                                (
                                string.Format
                                    (
                                    "<a class='dropdown-item' href='{0}'>{1}</a>", subMenu.FormPath, subMenu.FormDescription
                                    )
                                );
                            }

                            sbMenuHtml.Append("</ul>");
                            sbMenuHtml.Append("</li>");
                        }
                        else
                        { //Arbaz Work Start
                            if (menuItem.IsMVC == false)
                            {
                                sbMenuHtml.Append
                                (
                                string.Format
                                    (
                                    "<a class='dropdown-item' href='{0}'>{1}</a>", menuItem.FormPath, menuItem.FormDescription
                                    )
                                );
                            } //End
                            else {
                                sbMenuHtml.Append
                                    (
                                    string.Format
                                        (
                                        "<a class='dropdown-item' target = '_blank' href='{0}'>{1}</a>", menuItem.FormPath, menuItem.FormDescription
                                        //"<a class='dropdown-item' href='{0}'>{1}</a>", menuItem.FormPath, menuItem.FormDescription
                                        )
                                    );
                            }
                        }
                    }
                    sbMenuHtml.Append("</ul>");

                    sbMenuHtml.Append("</li>");
                    sbMenuHtml.Append("</ul>");
                }

                //foreach (var menuGroup in groupByModuleOrderQuery)
                //{
                //    var RootMenuTitle = menuGroup.Where(v => v.ModuleOrder == menuGroup.Key).ToList().FirstOrDefault();

                //    sbMenuHtml.Append("<ul class='navbar-nav '>");
                //    sbMenuHtml.Append("<li class='nav-item dropdown active'>");                    
                //    sbMenuHtml.Append("<a class='nav-link dropdown-toggle' href='#" + RootMenuTitle.ModuleName + "' id='navbarDropdownMenuLink' data-toggle='dropdown' aria-haspopup='true' aria-expanded='false'>");
                //    sbMenuHtml.Append(RootMenuTitle.ModuleName);
                //    sbMenuHtml.Append("</a>");

                //    sbMenuHtml.Append("<div class='dropdown-menu' aria-labelledby='navbarDropdownMenuLink'>");
                //    foreach (var menuitem in menuGroup)
                //    {                        
                //        sbMenuHtml.Append
                //            (
                //            string.Format
                //                (
                //                "<a class='dropdown-item' href='{0}'>{1}</a>", menuitem.FormPath, menuitem.FormDescription
                //                )
                //            );                        
                //        string mm = ($"\t{menuitem.FormDescription}, {menuitem.FormPath}");
                //    }
                //    sbMenuHtml.Append("</div>");
                //    sbMenuHtml.Append("</li>");
                //    sbMenuHtml.Append("</ul>");
                //}

                //generate menu from db             
                menuLit = sbMenuHtml.ToString();
                Session["MenuData"] = menuLit;
            }

            ltSidebarMenu.Text = menuLit;
        }

        private bool HaveAnyFormInRoot(List<sp_GetMenuNew_Result> MenuData, string RootName)
        {
            bool res = false;

            var ParentsInRoot = MenuData.Where(o => o.ParentItem == RootName);


            foreach (var item in ParentsInRoot)
            {
                if (item.IsParent == true)
                {
                    int cntFormInParent = MenuData.Count(o => o.ParentItem == item.FormId);

                    if (cntFormInParent > 0)
                    {
                        res = true;
                    }
                }
                else
                {
                    res = true;
                }
            }

            return res;
        }

        private void LoanMenuVerticalOld()
        {
            string loginUserID = Convert.ToString(Session["LoginUserId_TM"]);

            string menuLit = "";

            if (Session["MenuData"] != null)
            {
                menuLit = Convert.ToString(Session["MenuData"]);
            }
            else
            {
                var menuData = EDX.sp_GetMenu(loginUserID).ToList();
                var groupByModuleOrderQuery =
                    from menu in menuData
                    group menu by menu.ModuleOrder into newGroup
                    orderby newGroup.Key
                    select newGroup;

                StringBuilder sbMenuHtml = new StringBuilder();
                string AppName = "ESMS";

                sbMenuHtml.Append
                    (
                        @"<ul class='list-unstyled components'>
                        <p>" + AppName + "</p>"
                    );


                foreach (var menuGroup in groupByModuleOrderQuery)
                {
                    var RootMenuTitle = menuGroup.Where(v => v.ModuleOrder == menuGroup.Key).ToList().FirstOrDefault();

                    sbMenuHtml.Append("<li class='active' id='men1'>");
                    sbMenuHtml.Append("<a href = '#" + RootMenuTitle.ModuleName + "' data-toggle='collapse' aria-expanded='false' class='dropdown-toggle'>");
                    sbMenuHtml.Append(RootMenuTitle.ModuleName);
                    sbMenuHtml.Append("</a>");

                    sbMenuHtml.Append("<ul class='collapse list-unstyled' id='" + RootMenuTitle.ModuleName + "'>");
                    foreach (var menuitem in menuGroup)
                    {
                        sbMenuHtml.Append("<li>");
                        sbMenuHtml.Append
                            (
                            string.Format
                                (
                                "<a href = '{0}'> {1}</a>", menuitem.FormPath, menuitem.FormDescription
                                )
                            );
                        sbMenuHtml.Append("</li>");
                        string mm = ($"\t{menuitem.FormDescription}, {menuitem.FormPath}");
                    }
                    sbMenuHtml.Append("</ul>");
                    sbMenuHtml.Append("</li>");
                }

                sbMenuHtml.Append("</ul>");

                //generate menu from db             
                menuLit = sbMenuHtml.ToString();
                Session["MenuData"] = menuLit;
            }

            //ltSidebarMenu.Text = menuLit;
        }

        protected void lnkChangePass_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ChangePassword.aspx");
        }
    }
}