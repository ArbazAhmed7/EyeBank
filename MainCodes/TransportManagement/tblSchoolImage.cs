//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TransportManagement
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblSchoolImage
    {
        public int SchoolImageAutoId { get; set; }
        public int SchoolAutoId { get; set; }
        public byte[] SchoolPic { get; set; }
        public string FileType { get; set; }
        public Nullable<int> FileSize { get; set; }
        public Nullable<System.DateTime> CaptureDate { get; set; }
        public string CaptureRemarks { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> EntDate { get; set; }
        public string EntOperation { get; set; }
        public string EntTerminal { get; set; }
        public string EntTerminalIP { get; set; }
    }
}
