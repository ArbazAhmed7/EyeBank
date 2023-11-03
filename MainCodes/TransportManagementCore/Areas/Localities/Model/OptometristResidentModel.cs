using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagementCore.Areas.Setup.Model
{
    public class OptometristGothResidentModel
    {
        public int OptometristResidentId { get; set; }
        public DateTime OptometristResidentTransDate { get; set; }
        public int ResidentAutoId { get; set; }
        public int LocalityAutoId { get; set; }
        public int AutoRefResidentId { get; set; }
        public int HasChiefComplain { get; set; }
        public string ChiefComplainRemarks { get; set; }
        public int HasOccularHistory { get; set; }
        public string OccularHistoryRemarks { get; set; }
        public int HasMedicalHistory { get; set; }
        public string MedicalHistoryRemarks { get; set; }
        public int DistanceVision_RightEye_Unaided { get; set; }
        public int DistanceVision_RightEye_WithGlasses { get; set; }
        public int DistanceVision_RightEye_PinHole { get; set; }
        public int DistanceVision_LeftEye_Unaided { get; set; }
        public int DistanceVision_LeftEye_WithGlasses { get; set; }
        public int DistanceVision_LeftEye_PinHole { get; set; }
        public int NearVision_RightEye { get; set; }
        public int NearVision_LeftEye { get; set; }

        public char Right_Spherical_Status { get; set; }
        public decimal Right_Spherical_Points { get; set; }
        public char Right_Cyclinderical_Status { get; set; }
        public decimal Right_Cyclinderical_Points { get; set; }
        public int Right_Axix_From { get; set; }
        public int Right_Axix_To { get; set; }
        public char Right_Near_Status { get; set; }
        public decimal Right_Near_Points { get; set; }
        public char Left_Spherical_Status { get; set; }
        public decimal Left_Spherical_Points { get; set; }
        public char Left_Cyclinderical_Status { get; set; }
        public decimal Left_Cyclinderical_Points { get; set; }
        public int IPD { get; set; }


        public int Left_Axix_From { get; set; }
        public int Left_Axix_To { get; set; }
        public char Left_Near_Status { get; set; }
        public decimal Left_Near_Points { get; set; }
        public int VisualAcuity_RightEye { get; set; }
        public int VisualAcuity_LeftEye { get; set; }
        public bool LeftSquint_VA { get; set; }

        public bool RightSquint_VA { get; set; }
        public bool LeftAmblyopic_VA { get; set; }

        public bool RightAmblyopic_VA { get; set; }


        public int NeedCycloRefraction_LeftEye { get; set; }
        public string NeedCycloRefractionRemarks_LeftEye { get; set; }

        public int NeedCycloRefraction_RightEye { get; set; }
        public string NeedCycloRefractionRemarks_RightEye { get; set; }

        public int RightSquint { get; set; }
        public int LeftSquint { get; set; }

        public int RightAmblyopic { get; set; }
        public int LeftAmblyopic { get; set; }

        public int Hirchberg_Distance { get; set; }
        public int Hirchberg_Near { get; set; }

        public int Ophthalmoscope_RightEye { get; set; }
        public int PupillaryReactions_RightEye { get; set; }
        public int CoverUncovertTest_RightEye { get; set; }
        public string CoverUncovertTestRemarks_RightEye { get; set; }
        public string ExtraOccularMuscleRemarks_RightEye { get; set; }
        public string ExtraOccularMuscleRemarks_LeftEye { get; set; }
        public int Ophthalmoscope_LeftEye { get; set; }
        public int PupillaryReactions_LeftEye { get; set; }
        public int CoverUncovertTest_LeftEye { get; set; }
        public string CoverUncovertTestRemarks_LeftEye { get; set; }


        //from this add on js
        public bool CycloplegicRefraction_RightEye { get; set; }
        public bool CycloplegicRefraction_LeftEye { get; set; }

        public bool Conjunctivitis_RightEye { get; set; }
        public bool Conjunctivitis_LeftEye { get; set; }
        public bool Scleritis_RightEye { get; set; }
        public bool Scleritis_LeftEye { get; set; }

        public bool Nystagmus_RightEye { get; set; }
        public bool Nystagmus_LeftEye { get; set; }
        public bool CornealDefect_RightEye { get; set; }
        public bool CornealDefect_LeftEye { get; set; }

        public bool Cataract_RightEye { get; set; }
        public bool Cataract_LeftEye { get; set; }
        public bool Keratoconus_RightEye { get; set; }
        public bool Keratoconus_LeftEye { get; set; }

        public bool Ptosis_RightEye { get; set; }
        public bool Ptosis_LeftEye { get; set; }
        public bool LowVision_RightEye { get; set; }
        public bool LowVision_LeftEye { get; set; }
        public bool RightPupilDefects { get; set; }
        public bool LeftPupilDefects { get; set; }

        public bool Pterygium_RightEye { get; set; }
        public bool Pterygium_LeftEye { get; set; }
        public bool ColorBlindness_RightEye { get; set; }
        public bool ColorBlindness_LeftEye { get; set; }

        public bool Others_RightEye { get; set; }
        public bool Others_LeftEye { get; set; }
        public bool Fundoscopy_RightEye { get; set; }
        public bool Fundoscopy_LeftEye { get; set; }

        public bool Surgery_RightEye { get; set; }
        public bool Surgery_LeftEye { get; set; }
        public bool CataractSurgery_RightEye { get; set; }
        public bool CataractSurgery_LeftEye { get; set; }

        public bool SurgeryPterygium_RightEye { get; set; }
        public bool SurgeryPterygium_LeftEye { get; set; }
        public bool SurgeryCornealDefect_RightEye { get; set; }
        public bool SurgeryCornealDefect_LeftEye { get; set; }

        public bool RightAmblyopia { get; set; }
        public bool LeftAmblyopia { get; set; }


        public bool SurgeryPtosis_RightEye { get; set; }
        public bool SurgeryPtosis_LeftEye { get; set; }
        public bool SurgeryKeratoconus_RightEye { get; set; }
        public bool SurgeryKeratoconus_LeftEye { get; set; }

        public bool Chalazion_RightEye { get; set; }
        public bool Chalazion_LeftEye { get; set; }
        public bool Hordeolum_RightEye { get; set; }
        public bool Hordeolum_LeftEye { get; set; }

        public bool SurgeryOthers_RightEye { get; set; }
        public bool SurgeryOthers_LeftEye { get; set; }

        public bool LeftSquint_Surgery { get; set; }

        public bool RightSquint_Surgery { get; set; }

        public int Douchrome { get; set; }
        public string Achromatopsia { get; set; }
        public int RetinoScopy_RightEye { get; set; }

        public string Condition_RightEye { get; set; }



        //New Requirment Property
        public int RightVisualFieldTestId { get; set; }
        public int LeftVisualFieldTestId { get; set; }
        public bool RightCycloplagicdrops { get; set; }
        public string RightMeridian1 { get; set; }
        public string RightMeridian2 { get; set; }
        public string RightAxisOfRetino { get; set; }
        public string RightNoGlowVisibile { get; set; }
        public char Right_CycloDrops_Spherical_Status { get; set; }
        public decimal Right_CycloDrops_Spherical_Points { get; set; }
        public char Right_CycloDrops_Cyclinderical_Status { get; set; }
        public decimal Right_CycloDrops_Cyclinderical_Points { get; set; }
        public int Right_CycloDrops_Axix { get; set; }
        public string Right_CycloDrops_FinalPrescription { get; set; }
        public bool LeftCycloplagicdrops { get; set; }
        public string LeftMeridian1 { get; set; }
        public string LeftMeridian2 { get; set; }
        public string LeftAxisOfRetino { get; set; }
        public string LeftNoGlowVisibile { get; set; }
        public char Left_CycloDrops_Spherical_Status { get; set; }
        public decimal Left_CycloDrops_Spherical_Points { get; set; }
        public char Left_CycloDrops_Cyclinderical_Status { get; set; }
        public decimal Left_CycloDrops_Cyclinderical_Points { get; set; }
        public int Left_CycloDrops_Axix { get; set; }
        public string Left_CycloDrops_FinalPrescription { get; set; }
        public int TreatmentId { get; set; }
        public string Medicines { get; set; }
        public string Prescription { get; set; }
        public bool NextVisit_ReferToHospital { get; set; }




    }
}
