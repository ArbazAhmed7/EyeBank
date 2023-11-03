using System;

namespace TransportManagementCore.Models
{
    public class AutherizedFormRights
    {
        public string FormId { get; set; }
        public string FormDescription { get; set; }
        public Nullable<bool> IsRoot { get; set; }
        public Nullable<bool> IsParent { get; set; }
        public string ParentItem { get; set; }
        public Nullable<int> ItemOrder { get; set; }
        public string FormPath { get; set; }
        public Nullable<bool> IsMVC { get; set; }
        public bool CanView { get; set; }
    }
}