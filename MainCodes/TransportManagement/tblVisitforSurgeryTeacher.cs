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
    
    public partial class tblVisitforSurgeryTeacher
    {
        public int VisitforSurgeryTeacherId { get; set; }
        public Nullable<System.DateTime> VisitforSurgeryTeacherTransDate { get; set; }
        public Nullable<int> TeacherAutoId { get; set; }
        public Nullable<int> HospitalAutoId { get; set; }
        public Nullable<int> DoctorAutoId_Ophthalmologist { get; set; }
        public Nullable<int> DoctorAutoId_Orthoptist { get; set; }
        public Nullable<int> DoctorAutoId_Surgeon { get; set; }
        public Nullable<int> DoctorAutoId_Optometrist { get; set; }
        public string Surgery_RightEye { get; set; }
        public string SurgeryRemarks_RightEye { get; set; }
        public string Surgery_LeftEye { get; set; }
        public string SurgeryRemarks_LeftEye { get; set; }
        public string Remarks_Surgeon { get; set; }
        public Nullable<System.DateTime> FollowupDate { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> EntDate { get; set; }
        public string EntOperation { get; set; }
        public string EntTerminal { get; set; }
        public string EntTerminalIP { get; set; }
    }
}
