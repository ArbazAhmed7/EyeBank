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
    
    public partial class tblOptometristMasterStudentAuditTrail
    {
        public int tblOptometristMasterStudent_Id { get; set; }
        public Nullable<int> OptometristStudentId { get; set; }
        public Nullable<System.DateTime> OptometristStudentTransDate { get; set; }
        public Nullable<int> StudentAutoId { get; set; }
        public Nullable<int> HasChiefComplain { get; set; }
        public string ChiefComplainRemarks { get; set; }
        public Nullable<int> HasOccularHistory { get; set; }
        public string OccularHistoryRemarks { get; set; }
        public Nullable<int> HasMedicalHistory { get; set; }
        public string MedicalHistoryRemarks { get; set; }
        public Nullable<int> DistanceVision_RightEye_Unaided { get; set; }
        public Nullable<int> DistanceVision_RightEye_WithGlasses { get; set; }
        public Nullable<int> DistanceVision_RightEye_PinHole { get; set; }
        public Nullable<int> NearVision_RightEye { get; set; }
        public Nullable<int> NeedCycloRefraction_RightEye { get; set; }
        public string NeedCycloRefractionRemarks_RightEye { get; set; }
        public Nullable<int> DistanceVision_LeftEye_Unaided { get; set; }
        public Nullable<int> DistanceVision_LeftEye_WithGlasses { get; set; }
        public Nullable<int> DistanceVision_LeftEye_PinHole { get; set; }
        public Nullable<int> NearVision_LeftEye { get; set; }
        public Nullable<int> NeedCycloRefraction_LeftEye { get; set; }
        public string NeedCycloRefractionRemarks_LeftEye { get; set; }
        public string Right_Spherical_Status { get; set; }
        public Nullable<decimal> Right_Spherical_Points { get; set; }
        public string Right_Cyclinderical_Status { get; set; }
        public Nullable<decimal> Right_Cyclinderical_Points { get; set; }
        public Nullable<int> Right_Axix_From { get; set; }
        public Nullable<int> Right_Axix_To { get; set; }
        public string Right_Near_Status { get; set; }
        public Nullable<decimal> Right_Near_Points { get; set; }
        public string Left_Spherical_Status { get; set; }
        public Nullable<decimal> Left_Spherical_Points { get; set; }
        public string Left_Cyclinderical_Status { get; set; }
        public Nullable<decimal> Left_Cyclinderical_Points { get; set; }
        public Nullable<int> Left_Axix_From { get; set; }
        public Nullable<int> Left_Axix_To { get; set; }
        public string Left_Near_Status { get; set; }
        public Nullable<decimal> Left_Near_Points { get; set; }
        public Nullable<int> Douchrome { get; set; }
        public string Achromatopsia { get; set; }
        public Nullable<int> RetinoScopy_RightEye { get; set; }
        public string CycloplegicRefraction_RightEye { get; set; }
        public string Condition_RightEye { get; set; }
        public string Meridian1_RightEye { get; set; }
        public string Meridian2_RightEye { get; set; }
        public string FinalPrescription_RightEye { get; set; }
        public Nullable<int> RetinoScopy_LeftEye { get; set; }
        public string CycloplegicRefraction_LeftEye { get; set; }
        public string Condition_LeftEye { get; set; }
        public string Meridian1_LeftEye { get; set; }
        public string Meridian2_LeftEye { get; set; }
        public string FinalPrescription_LeftEye { get; set; }
        public Nullable<int> Hirchberg_Distance { get; set; }
        public Nullable<int> Hirchberg_Near { get; set; }
        public Nullable<int> Ophthalmoscope_RightEye { get; set; }
        public Nullable<int> PupillaryReactions_RightEye { get; set; }
        public Nullable<int> CoverUncovertTest_RightEye { get; set; }
        public string CoverUncovertTestRemarks_RightEye { get; set; }
        public string ExtraOccularMuscleRemarks_RightEye { get; set; }
        public Nullable<int> Ophthalmoscope_LeftEye { get; set; }
        public Nullable<int> PupillaryReactions_LeftEye { get; set; }
        public Nullable<int> CoverUncovertTest_LeftEye { get; set; }
        public string CoverUncovertTestRemarks_LeftEye { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> EntDate { get; set; }
        public string EntOperation { get; set; }
        public string EntTerminal { get; set; }
        public string EntTerminalIP { get; set; }
        public string ExtraOccularMuscleRemarks_LeftEye { get; set; }
    }
}
