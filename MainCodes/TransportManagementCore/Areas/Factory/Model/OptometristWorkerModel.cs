using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagementCore.Areas.Goth.Model
{
    public class OptometristWorkerModel
    {
         public int OptometristWorkerId { get; set; } 
        public DateTime? OptometristWorkerTransDate { get; set; } 
        public int? WorkerAutoId { get; set; } 
        public int? HasChiefComplain { get; set; } 
        public string ChiefComplainRemarks { get; set; } 
        public int? HasOccularHistory { get; set; }
        public string OccularHistoryRemarks { get; set; } 
        public int? HasMedicalHistory { get; set; } 
        public string MedicalHistoryRemarks { get; set; } 
        public int? DistanceVision_RightEye_Unaided { get; set; } 
        public int? DistanceVision_RightEye_WithGlasses { get; set; } 
        public int? DistanceVision_RightEye_PinHole { get; set; } 
        public int? NearVision_RightEye { get; set; }
        public int? NeedCycloRefraction_RightEye { get; set; }
        public string NeedCycloRefractionRemarks_RightEye { get; set; } 
        public int? DistanceVision_LeftEye_Unaided { get; set; } 
        public int? DistanceVision_LeftEye_WithGlasses { get; set; }
        public int? DistanceVision_LeftEye_PinHole { get; set; } 
        public int? NearVision_LeftEye { get; set; } 
        public int? NeedCycloRefraction_LeftEye { get; set; } 
        public string NeedCycloRefractionRemarks_LeftEye { get; set; } 
        public string Right_Spherical_Status { get; set; }
        public decimal? Right_Spherical_Points { get; set; } 
        public string Right_Cyclinderical_Status { get; set; }
        public decimal? Right_Cyclinderical_Points { get; set; }
        public int? Right_Axix_From { get; set; }
        public int? Right_Axix_To { get; set; } 
        public string Right_Near_Status { get; set; }
        public decimal? Right_Near_Points { get; set; } 
        public string Left_Spherical_Status { get; set; } 
        public decimal? Left_Spherical_Points { get; set; }
        public string Left_Cyclinderical_Status { get; set; } 
        public decimal? Left_Cyclinderical_Points { get; set; }
        public int? Left_Axix_From { get; set; }
        public int? Left_Axix_To { get; set; } 
        public string Left_Near_Status { get; set; } 
        public decimal? Left_Near_Points { get; set; } 
        public int? Douchrome { get; set; }
        public string Achromatopsia { get; set; }
        public int? RetinoScopy_RightEye { get; set; } 
        public string CycloplegicRefraction_RightEye { get; set; }
        public string Condition_RightEye { get; set; }
        public string Meridian1_RightEye { get; set; }
        public string Meridian2_RightEye { get; set; }
        public string FinalPrescription_RightEye { get; set; } 
        public int? RetinoScopy_LeftEye { get; set; }
        public string CycloplegicRefraction_LeftEye { get; set; } 
        public string Condition_LeftEye { get; set; }
        public string Meridian1_LeftEye { get; set; }
        public string Meridian2_LeftEye { get; set; } 
        public string FinalPrescription_LeftEye { get; set; }
        public int? Hirchberg_Distance { get; set; } 
        public int? Hirchberg_Near { get; set; } 
        public int? Ophthalmoscope_RightEye { get; set; }
        public int? PupillaryReactions_RightEye { get; set; }
        public int? CoverUncovertTest_RightEye { get; set; } 
        public string CoverUncovertTestRemarks_RightEye { get; set; }
        public string ExtraOccularMuscleRemarks_RightEye { get; set; } 
        public int? Ophthalmoscope_LeftEye { get; set; }
        public int? PupillaryReactions_LeftEye { get; set; } 
        public int? CoverUncovertTest_LeftEye { get; set; }
        public string CoverUncovertTestRemarks_LeftEye { get; set; }
        public string UserId { get; set; } 
        public DateTime? EntDate { get; set; } 
        public string EntOperation { get; set; }
        public string EntTerminal { get; set; } 
        public string EntTerminalIP { get; set; } 
        public string ExtraOccularMuscleRemarks_LeftEye { get; set; }
        public string ApplicationID { get; set; } 
        public string FormId { get; set; } 
        public int? UserEmpId { get; set; } 
        public string UserEmpName { get; set; }
        public string UserEmpCode { get; set; } 
        public int? VisualAcuity_RightEye { get; set; } 
        public int? VisualAcuity_LeftEye { get; set; } 
        public int? Squint_VA { get; set; } 
        public int? Amblyopic_VA { get; set; } 
        public int? AutoRefWorkerId { get; set; } 
    }
}
