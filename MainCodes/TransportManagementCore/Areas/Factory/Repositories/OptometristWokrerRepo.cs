using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TransportManagementCore.Areas.Setup.Model;
using TransportManagementCore.Models;

namespace TransportManagementCore.Areas.Goth.Repositories
{
    public class OptometristWokrerRepo
    {
        DBHelper.DBHelper db;
        public OptometristWokrerRepo()
        {
            db = new DBHelper.DBHelper();
        }
        public async Task<DataTable> DbFunction(string ProcedureName, List<SqlParameter> parameters)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = this.db.GetDataTable(ProcedureName, CommandType.StoredProcedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                dt.Rows[0][0] = "Error :" + ex.Message.ToString();
            }
            return dt;
        }
        public DataTable GetForModelFromDB(string ProcedureName, List<SqlParameter> parameters)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = this.db.GetDataTable(ProcedureName, CommandType.StoredProcedure, parameters.ToArray());
            }
            catch (Exception ex)
            {
                dt.Rows[0][0] = "Error :" + ex.Message.ToString();
            }
            return dt;
        }

        public List<DropDownModel> DateList(DataTable dt)
        {
            List<DropDownModel> list = new List<DropDownModel>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DropDownModel dm = new DropDownModel();
                    dm.Code = Convert.ToString(row["Id"]);
                    dm.Text = Convert.ToString(row["Text"]);
                    list.Add(dm);
                }
            }
            return list;
        }
        public List<SqlParameter> SetModel(List<SqlParameter> para, OptometristWokerModel model)
        {
            para.Add(new("OptometristWorkerId", model.OptometristWorkerId));
            para.Add(new("OptometristWorkerTransDate", model.OptometristWorkerTransDate));
            para.Add(new("WorkerAutoId", model.WorkerAutoId));
            para.Add(new("AutoRefWorkerId", model.AutoRefWorkerId));
            para.Add(new("HasChiefComplain", model.HasChiefComplain));
            para.Add(new("ChiefComplainRemarks", model.ChiefComplainRemarks));
            para.Add(new("HasOccularHistory", model.HasOccularHistory));
            para.Add(new("OccularHistoryRemarks", model.OccularHistoryRemarks));
            para.Add(new("HasMedicalHistory", model.HasMedicalHistory));
            para.Add(new("DistanceVision_RightEye_Unaided", model.DistanceVision_RightEye_Unaided));
            para.Add(new("DistanceVision_RightEye_WithGlasses", model.DistanceVision_RightEye_WithGlasses));
            para.Add(new("DistanceVision_RightEye_PinHole", model.DistanceVision_RightEye_PinHole));
            para.Add(new("NearVision_RightEye", model.NearVision_RightEye));
            para.Add(new("NeedCycloRefraction_RightEye", model.NeedCycloRefraction_RightEye));
            para.Add(new("NeedCycloRefractionRemarks_RightEye", model.NeedCycloRefractionRemarks_RightEye));
            para.Add(new("DistanceVision_LeftEye_Unaided", model.DistanceVision_LeftEye_Unaided));
            para.Add(new("DistanceVision_LeftEye_WithGlasses", model.DistanceVision_LeftEye_WithGlasses));
            para.Add(new("DistanceVision_LeftEye_PinHole", model.DistanceVision_LeftEye_PinHole));
            para.Add(new("NearVision_LeftEye", model.NearVision_LeftEye));
            para.Add(new("NeedCycloRefraction_LeftEye", model.NeedCycloRefraction_LeftEye));
            para.Add(new("NeedCycloRefractionRemarks_LeftEye", model.NeedCycloRefractionRemarks_LeftEye));
            para.Add(new("Right_Spherical_Status", model.Right_Spherical_Status));
            para.Add(new("Right_Spherical_Points", model.Right_Spherical_Points));
            para.Add(new("Right_Cyclinderical_Status", model.Right_Cyclinderical_Status));
            para.Add(new("Right_Cyclinderical_Points", model.Right_Cyclinderical_Points));
            para.Add(new("Right_Axix_From", model.Right_Axix_From));
            para.Add(new("Right_Axix_To", model.Right_Axix_To));
            para.Add(new("Right_Near_Status", model.Right_Near_Status));
            para.Add(new("Right_Near_Points", model.Right_Near_Points));
            para.Add(new("Left_Spherical_Status", model.Left_Spherical_Status));
            para.Add(new("Left_Spherical_Points", model.Left_Spherical_Points));
            para.Add(new("Left_Cyclinderical_Status", model.Left_Cyclinderical_Status));
            para.Add(new("Left_Cyclinderical_Points", model.Left_Cyclinderical_Points));
            para.Add(new("Left_Axix_From", model.Left_Axix_From));
            para.Add(new("Left_Axix_To", model.Left_Axix_To));
            para.Add(new("Left_Near_Status", model.Left_Near_Status));
            para.Add(new("Left_Near_Points", model.Left_Near_Points));
            para.Add(new("IPD", model.IPD));
            para.Add(new("Douchrome", model.Douchrome));
            para.Add(new("Achromatopsia", model.Achromatopsia));
            para.Add(new("RetinoScopy_RightEye", model.RetinoScopy_RightEye));
            para.Add(new("Condition_RightEye", model.Condition_RightEye));
            //para.Add(new("RetinoScopy_LeftEye", model.RetinoScopy_LeftEye));
            //para.Add(new("Condition_LeftEye", model.Condition_LeftEye));
            para.Add(new("Hirchberg_Distance", model.Hirchberg_Distance));
            para.Add(new("Hirchberg_Near", model.Hirchberg_Near));
            para.Add(new("Ophthalmoscope_RightEye", model.Ophthalmoscope_RightEye));
            para.Add(new("PupillaryReactions_RightEye", model.PupillaryReactions_RightEye));
            para.Add(new("CoverUncovertTest_RightEye", model.CoverUncovertTest_RightEye));
            para.Add(new("CoverUncovertTestRemarks_RightEye", model.CoverUncovertTestRemarks_RightEye));
            para.Add(new("ExtraOccularMuscleRemarks_RightEye", model.ExtraOccularMuscleRemarks_RightEye));
            para.Add(new("Ophthalmoscope_LeftEye", model.Ophthalmoscope_LeftEye));
            para.Add(new("PupillaryReactions_LeftEye", model.PupillaryReactions_LeftEye));
            para.Add(new("CoverUncovertTest_LeftEye", model.CoverUncovertTest_LeftEye));
            para.Add(new("CoverUncovertTestRemarks_LeftEye", model.CoverUncovertTestRemarks_LeftEye)); 
            para.Add(new("CycloplegicRefraction_RightEye", model.CycloplegicRefraction_RightEye));
            para.Add(new("CycloplegicRefraction_LeftEye", model.CycloplegicRefraction_LeftEye));
            para.Add(new("Conjunctivitis_RightEye", model.Conjunctivitis_RightEye));
            para.Add(new("Conjunctivitis_LeftEye", model.Conjunctivitis_LeftEye));
            para.Add(new("Scleritis_RightEye", model.Scleritis_RightEye));
            para.Add(new("Scleritis_LeftEye", model.Scleritis_LeftEye));
            para.Add(new("Nystagmus_RightEye", model.Nystagmus_RightEye));
            para.Add(new("Nystagmus_LeftEye", model.Nystagmus_LeftEye));
            para.Add(new("CornealDefect_RightEye", model.CornealDefect_RightEye));
            para.Add(new("CornealDefect_LeftEye", model.CornealDefect_LeftEye));
            para.Add(new("Cataract_RightEye", model.Cataract_RightEye));
            para.Add(new("Cataract_LeftEye", model.Cataract_LeftEye));
            para.Add(new("Keratoconus_RightEye", model.Keratoconus_RightEye));
            para.Add(new("Keratoconus_LeftEye", model.Keratoconus_LeftEye));
            para.Add(new("Ptosis_RightEye", model.Ptosis_RightEye));
            para.Add(new("Ptosis_LeftEye", model.Ptosis_LeftEye));
            para.Add(new("LowVision_RightEye", model.LowVision_RightEye));
            para.Add(new("LowVision_LeftEye", model.LowVision_LeftEye));
            para.Add(new("Pterygium_RightEye", model.Pterygium_RightEye));
            para.Add(new("Pterygium_LeftEye", model.Pterygium_LeftEye));
            para.Add(new("ColorBlindness_RightEye", model.ColorBlindness_RightEye));
            para.Add(new("ColorBlindness_LeftEye", model.ColorBlindness_LeftEye));
            para.Add(new("Others_RightEye", model.Others_RightEye));
            para.Add(new("Others_LeftEye", model.Others_LeftEye));
            para.Add(new("Fundoscopy_RightEye", model.Fundoscopy_RightEye));
            para.Add(new("Fundoscopy_LeftEye", model.Fundoscopy_LeftEye)); 
            para.Add(new("Surgery_RightEye", model.Surgery_RightEye));
            para.Add(new("Surgery_LeftEye", model.Surgery_LeftEye));
            para.Add(new("CataractSurgery_RightEye", model.CataractSurgery_RightEye));
            para.Add(new("CataractSurgery_LeftEye", model.CataractSurgery_LeftEye));
            para.Add(new("SurgeryPterygium_RightEye", model.SurgeryPterygium_RightEye));
            para.Add(new("SurgeryPterygium_LeftEye", model.SurgeryPterygium_LeftEye));
            para.Add(new("SurgeryCornealDefect_RightEye", model.SurgeryCornealDefect_RightEye));
            para.Add(new("SurgeryCornealDefect_LeftEye", model.SurgeryCornealDefect_LeftEye));
            para.Add(new("SurgeryPtosis_RightEye", model.SurgeryPtosis_RightEye));
            para.Add(new("SurgeryPtosis_LeftEye", model.SurgeryPtosis_LeftEye));
            para.Add(new("SurgeryKeratoconus_RightEye", model.SurgeryKeratoconus_RightEye));
            para.Add(new("SurgeryKeratoconus_LeftEye", model.SurgeryKeratoconus_LeftEye));
            para.Add(new("Chalazion_RightEye", model.Chalazion_RightEye));
            para.Add(new("Chalazion_LeftEye", model.Chalazion_LeftEye));
            para.Add(new("Hordeolum_RightEye", model.Hordeolum_RightEye));
            para.Add(new("Hordeolum_LeftEye", model.Hordeolum_LeftEye));
            para.Add(new("SurgeryOthers_RightEye", model.SurgeryOthers_RightEye));
            para.Add(new("SurgeryOthers_LeftEye", model.SurgeryOthers_LeftEye));
            para.Add(new("CompanyAutoId", model.CompanyAutoId));
            para.Add(new("ExtraOccularMuscleRemarks_LeftEye", model.ExtraOccularMuscleRemarks_LeftEye));
            para.Add(new("VisualAcuity_RightEye", model.VisualAcuity_RightEye));
            para.Add(new("VisualAcuity_LeftEye", model.VisualAcuity_LeftEye));
            para.Add(new("LeftSquint_VA", model.LeftSquint_VA));
            para.Add(new("RightSquint_VA", model.RightSquint_VA));
            para.Add(new("LeftAmblyopic_VA", model.LeftAmblyopic_VA));
            para.Add(new("RightAmblyopic_VA", model.RightAmblyopic_VA));
            para.Add(new("RightPupilDefects", model.RightPupilDefects));
            para.Add(new("LeftPupilDefects", model.LeftPupilDefects));
            para.Add(new("RightAmblyopia", model.RightAmblyopia));
            para.Add(new("LeftAmblyopia", model.LeftAmblyopia));
            para.Add(new("LeftSquint_Surgery", model.LeftSquint_Surgery));
            para.Add(new("RightSquint_Surgery", model.RightSquint_Surgery));




            para.Add(new("LeftVisualFieldTestId", model.LeftVisualFieldTestId));
            para.Add(new("RightVisualFieldTestId", model.RightVisualFieldTestId));
            para.Add(new("RightCycloplagicdrops", model.RightCycloplagicdrops));
            para.Add(new("RightMeridian1", model.RightMeridian1));
            para.Add(new("RightMeridian2", model.RightMeridian2));
            para.Add(new("RightAxisOfRetino", model.RightAxisOfRetino));
            para.Add(new("RightNoGlowVisibile", model.RightNoGlowVisibile));
            para.Add(new("Right_CycloDrops_Spherical_Status", model.Right_CycloDrops_Spherical_Status));
            para.Add(new("Right_CycloDrops_Spherical_Points", model.Right_CycloDrops_Spherical_Points));
            para.Add(new("Right_CycloDrops_Cyclinderical_Status", model.Right_CycloDrops_Cyclinderical_Status));
            para.Add(new("Right_CycloDrops_Cyclinderical_Points", model.Right_CycloDrops_Cyclinderical_Points));
            para.Add(new("Right_CycloDrops_Axix", model.Right_CycloDrops_Axix));
            para.Add(new("Right_CycloDrops_FinalPrescription", model.Right_CycloDrops_FinalPrescription));
            para.Add(new("LeftCycloplagicdrops", model.LeftCycloplagicdrops));
            para.Add(new("LeftMeridian1", model.LeftMeridian1));
            para.Add(new("LeftMeridian2", model.LeftMeridian2));
            para.Add(new("LeftAxisOfRetino", model.LeftAxisOfRetino));
            para.Add(new("LeftNoGlowVisibile", model.LeftNoGlowVisibile));
            para.Add(new("Left_CycloDrops_Spherical_Status", model.Left_CycloDrops_Spherical_Status));
            para.Add(new("Left_CycloDrops_Spherical_Points", model.Left_CycloDrops_Spherical_Points));
            para.Add(new("Left_CycloDrops_Cyclinderical_Status", model.Left_CycloDrops_Cyclinderical_Status));
            para.Add(new("Left_CycloDrops_Cyclinderical_Points", model.Left_CycloDrops_Cyclinderical_Points));
            para.Add(new("Left_CycloDrops_Axix", model.Left_CycloDrops_Axix));
            para.Add(new("Left_CycloDrops_FinalPrescription", model.Left_CycloDrops_FinalPrescription));
            para.Add(new("TreatmentId", model.TreatmentId));
            para.Add(new("Medicines", model.Medicines));
            para.Add(new("Prescription", model.Prescription));
            para.Add(new("NextVisit_ReferToHospital", model.NextVisit_ReferToHospital));


            return para;
        }


        public OptometristWokerModel GetOptometristWokerLast(DataTable dt)
        {
            OptometristWokerModel auto = null;
            if (dt.Rows.Count > 0)
            {
                auto = new OptometristWokerModel();
                auto.OptometristWorkerId = Convert.ToInt32(dt.Rows[0]["OptometristWorkerId"]);
                auto.OptometristWorkerTransDate = Convert.ToDateTime(dt.Rows[0]["OptometristWorkerTransDate"]);
                auto.WorkerAutoId = Convert.ToInt32(dt.Rows[0]["WorkerAutoId"]);
                auto.CompanyAutoId = Convert.ToInt32(dt.Rows[0]["CompanyAutoId"]);
                auto.HasChiefComplain = Convert.ToInt32(dt.Rows[0]["HasChiefComplain"]);
                auto.ChiefComplainRemarks = Convert.ToString(dt.Rows[0]["ChiefComplainRemarks"]);
                auto.HasOccularHistory = Convert.ToInt32(dt.Rows[0]["HasOccularHistory"]);
                auto.OccularHistoryRemarks = Convert.ToString(dt.Rows[0]["OccularHistoryRemarks"]);
                auto.HasMedicalHistory = Convert.ToInt32(dt.Rows[0]["HasMedicalHistory"]);
                auto.MedicalHistoryRemarks = Convert.ToString(dt.Rows[0]["MedicalHistoryRemarks"]);
                auto.DistanceVision_RightEye_Unaided = Convert.ToInt32(dt.Rows[0]["DistanceVision_RightEye_Unaided"]);
                auto.DistanceVision_RightEye_WithGlasses = Convert.ToInt32(dt.Rows[0]["DistanceVision_RightEye_WithGlasses"]);
                auto.DistanceVision_RightEye_PinHole = Convert.ToInt32(dt.Rows[0]["DistanceVision_RightEye_PinHole"]);
                auto.NeedCycloRefraction_RightEye = Convert.ToInt32(dt.Rows[0]["NeedCycloRefraction_RightEye"]);
                auto.NeedCycloRefractionRemarks_RightEye = Convert.ToString(dt.Rows[0]["NeedCycloRefractionRemarks_RightEye"]);
                auto.DistanceVision_LeftEye_Unaided = Convert.ToInt32(dt.Rows[0]["DistanceVision_LeftEye_Unaided"]);
                auto.DistanceVision_LeftEye_WithGlasses = Convert.ToInt32(dt.Rows[0]["DistanceVision_LeftEye_WithGlasses"]);
                auto.DistanceVision_LeftEye_PinHole = Convert.ToInt32(dt.Rows[0]["DistanceVision_LeftEye_PinHole"]);
                auto.NearVision_LeftEye = Convert.ToInt32(dt.Rows[0]["NearVision_LeftEye"]);
                auto.NeedCycloRefraction_LeftEye = Convert.ToInt32(dt.Rows[0]["NeedCycloRefraction_LeftEye"]);
                auto.NeedCycloRefractionRemarks_LeftEye = Convert.ToString(dt.Rows[0]["NeedCycloRefractionRemarks_LeftEye"]);
                auto.Right_Spherical_Status = Convert.ToChar(dt.Rows[0]["Right_Spherical_Status"]);
                auto.Right_Spherical_Points = Convert.ToDecimal(dt.Rows[0]["Right_Spherical_Points"]);
                auto.Right_Cyclinderical_Status = Convert.ToChar(dt.Rows[0]["Right_Cyclinderical_Status"]);
                auto.Right_Cyclinderical_Points = Convert.ToDecimal(dt.Rows[0]["Right_Cyclinderical_Points"]);
                auto.Right_Axix_From = Convert.ToInt32(dt.Rows[0]["Right_Axix_From"]);
                auto.Right_Axix_To = Convert.ToInt32(dt.Rows[0]["Right_Axix_To"]);
                auto.Right_Near_Status = Convert.ToChar(dt.Rows[0]["Right_Near_Status"]); 
                auto.Right_Near_Points = Convert.ToDecimal(dt.Rows[0]["Right_Near_Points"]);
                auto.Left_Spherical_Status = Convert.ToChar(dt.Rows[0]["Left_Spherical_Status"]);
                auto.Left_Spherical_Points = Convert.ToDecimal(dt.Rows[0]["Left_Spherical_Points"]);
                auto.Left_Cyclinderical_Status = Convert.ToChar(dt.Rows[0]["Left_Cyclinderical_Status"]);
                auto.Left_Cyclinderical_Points = Convert.ToDecimal(dt.Rows[0]["Left_Cyclinderical_Points"]);
                auto.Left_Axix_From = Convert.ToInt32(dt.Rows[0]["Left_Axix_From"]);
                auto.Left_Axix_To = Convert.ToInt32(dt.Rows[0]["Left_Axix_To"]);
                auto.Left_Near_Status = Convert.ToChar(dt.Rows[0]["Left_Near_Status"]);
                auto.Left_Near_Points = Convert.ToDecimal(dt.Rows[0]["Left_Near_Points"]);
                
                auto.VisualAcuity_RightEye = Convert.ToInt32(dt.Rows[0]["VisualAcuity_RightEye"]);
                auto.LeftSquint_VA = Convert.ToBoolean(dt.Rows[0]["LeftSquint_VA"]);
                auto.RightSquint_VA = Convert.ToBoolean(dt.Rows[0]["RightSquint_VA"]);
                auto.LeftAmblyopic_VA = Convert.ToBoolean(dt.Rows[0]["LeftAmblyopic_VA"]);
                auto.RightAmblyopic_VA = Convert.ToBoolean(dt.Rows[0]["RightAmblyopic_VA"]);
                auto.AutoRefWorkerId = Convert.ToInt32(dt.Rows[0]["AutoRefWorkerId"]);
                auto.Hirchberg_Distance = Convert.ToInt32(dt.Rows[0]["Hirchberg_Distance"]);
                auto.Hirchberg_Near = Convert.ToInt32(dt.Rows[0]["Hirchberg_Near"]);
                auto.Ophthalmoscope_RightEye = Convert.ToInt32(dt.Rows[0]["Ophthalmoscope_RightEye"]);
                auto.Ophthalmoscope_LeftEye = Convert.ToInt32(dt.Rows[0]["Ophthalmoscope_LeftEye"]);
                auto.PupillaryReactions_RightEye = Convert.ToInt32(dt.Rows[0]["PupillaryReactions_RightEye"]);
                auto.CoverUncovertTest_RightEye = Convert.ToInt32(dt.Rows[0]["CoverUncovertTest_RightEye"]);
                auto.CoverUncovertTestRemarks_RightEye = Convert.ToString(dt.Rows[0]["CoverUncovertTestRemarks_RightEye"]);
                auto.ExtraOccularMuscleRemarks_RightEye = Convert.ToString(dt.Rows[0]["ExtraOccularMuscleRemarks_RightEye"]);
                auto.CoverUncovertTest_LeftEye = Convert.ToInt32(dt.Rows[0]["CoverUncovertTest_LeftEye"]);
                auto.CoverUncovertTestRemarks_LeftEye = Convert.ToString(dt.Rows[0]["CoverUncovertTestRemarks_LeftEye"]);
                auto.CycloplegicRefraction_RightEye = Convert.ToBoolean(dt.Rows[0]["CycloplegicRefraction_RightEye"]);
                auto.CycloplegicRefraction_LeftEye = Convert.ToBoolean(dt.Rows[0]["CycloplegicRefraction_LeftEye"]);
                auto.Conjunctivitis_RightEye = Convert.ToBoolean(dt.Rows[0]["Conjunctivitis_RightEye"]);
                auto.Conjunctivitis_LeftEye = Convert.ToBoolean(dt.Rows[0]["Conjunctivitis_LeftEye"]);
                auto.Scleritis_RightEye = Convert.ToBoolean(dt.Rows[0]["Scleritis_RightEye"]);
                auto.Scleritis_LeftEye = Convert.ToBoolean(dt.Rows[0]["Scleritis_LeftEye"]);
                auto.Nystagmus_RightEye = Convert.ToBoolean(dt.Rows[0]["Nystagmus_RightEye"]);
                auto.Nystagmus_LeftEye = Convert.ToBoolean(dt.Rows[0]["Nystagmus_LeftEye"]);
                auto.CornealDefect_RightEye = Convert.ToBoolean(dt.Rows[0]["CornealDefect_RightEye"]);
                auto.CornealDefect_LeftEye = Convert.ToBoolean(dt.Rows[0]["CornealDefect_LeftEye"]);
                auto.Cataract_RightEye = Convert.ToBoolean(dt.Rows[0]["Cataract_RightEye"]);
                auto.Cataract_LeftEye = Convert.ToBoolean(dt.Rows[0]["Cataract_LeftEye"]);  
                auto.Keratoconus_RightEye = Convert.ToBoolean(dt.Rows[0]["Keratoconus_RightEye"]);
                auto.Keratoconus_LeftEye = Convert.ToBoolean(dt.Rows[0]["Keratoconus_LeftEye"]);
                auto.Ptosis_RightEye = Convert.ToBoolean(dt.Rows[0]["Ptosis_RightEye"]);
                auto.Ptosis_LeftEye = Convert.ToBoolean(dt.Rows[0]["Ptosis_LeftEye"]);
                auto.LowVision_RightEye = Convert.ToBoolean(dt.Rows[0]["LowVision_RightEye"]);
                auto.LowVision_LeftEye = Convert.ToBoolean(dt.Rows[0]["LowVision_LeftEye"]);
                auto.Pterygium_RightEye = Convert.ToBoolean(dt.Rows[0]["Pterygium_RightEye"]);
                auto.Pterygium_LeftEye = Convert.ToBoolean(dt.Rows[0]["Pterygium_LeftEye"]);
                auto.ColorBlindness_RightEye = Convert.ToBoolean(dt.Rows[0]["ColorBlindness_RightEye"]);
                auto.ColorBlindness_LeftEye = Convert.ToBoolean(dt.Rows[0]["ColorBlindness_LeftEye"]);
                auto.Others_RightEye = Convert.ToBoolean(dt.Rows[0]["Others_RightEye"]);
                auto.Others_LeftEye = Convert.ToBoolean(dt.Rows[0]["Others_LeftEye"]);
                auto.Fundoscopy_RightEye = Convert.ToBoolean(dt.Rows[0]["Fundoscopy_RightEye"]);
                auto.Fundoscopy_LeftEye = Convert.ToBoolean(dt.Rows[0]["Fundoscopy_LeftEye"]);
                auto.Surgery_RightEye = Convert.ToBoolean(dt.Rows[0]["Surgery_RightEye"]);
                auto.Surgery_LeftEye = Convert.ToBoolean(dt.Rows[0]["Surgery_LeftEye"]);
                auto.CataractSurgery_RightEye = Convert.ToBoolean(dt.Rows[0]["CataractSurgery_RightEye"]);
                auto.CataractSurgery_LeftEye = Convert.ToBoolean(dt.Rows[0]["CataractSurgery_LeftEye"]);
                auto.SurgeryPterygium_RightEye = Convert.ToBoolean(dt.Rows[0]["SurgeryPterygium_RightEye"]);
                auto.SurgeryPterygium_LeftEye = Convert.ToBoolean(dt.Rows[0]["SurgeryPterygium_LeftEye"]);
                auto.SurgeryCornealDefect_RightEye = Convert.ToBoolean(dt.Rows[0]["SurgeryCornealDefect_RightEye"]);
                auto.SurgeryCornealDefect_LeftEye = Convert.ToBoolean(dt.Rows[0]["SurgeryCornealDefect_LeftEye"]);
                auto.SurgeryPtosis_RightEye = Convert.ToBoolean(dt.Rows[0]["SurgeryPtosis_RightEye"]);
                auto.SurgeryPtosis_LeftEye = Convert.ToBoolean(dt.Rows[0]["SurgeryPtosis_LeftEye"]);
                auto.SurgeryKeratoconus_RightEye = Convert.ToBoolean(dt.Rows[0]["SurgeryKeratoconus_RightEye"]);
                auto.SurgeryKeratoconus_LeftEye = Convert.ToBoolean(dt.Rows[0]["SurgeryKeratoconus_LeftEye"]);
                auto.Chalazion_RightEye = Convert.ToBoolean(dt.Rows[0]["Chalazion_RightEye"]);
                auto.Chalazion_LeftEye = Convert.ToBoolean(dt.Rows[0]["Chalazion_LeftEye"]);
                auto.Hordeolum_RightEye = Convert.ToBoolean(dt.Rows[0]["Hordeolum_RightEye"]);
                auto.Hordeolum_LeftEye = Convert.ToBoolean(dt.Rows[0]["Hordeolum_LeftEye"]);
                auto.SurgeryOthers_RightEye = Convert.ToBoolean(dt.Rows[0]["SurgeryOthers_RightEye"]);
                auto.SurgeryOthers_LeftEye = Convert.ToBoolean(dt.Rows[0]["SurgeryOthers_LeftEye"]);
                auto.Douchrome = Convert.ToInt32(dt.Rows[0]["Douchrome"]);
                auto.Achromatopsia = Convert.ToString(dt.Rows[0]["Achromatopsia"]);
                auto.RetinoScopy_RightEye = Convert.ToInt32(dt.Rows[0]["RetinoScopy_RightEye"]);
                auto.ExtraOccularMuscleRemarks_LeftEye = Convert.ToString(dt.Rows[0]["ExtraOccularMuscleRemarks_LeftEye"]);
                auto.RightPupilDefects = Convert.ToBoolean(dt.Rows[0]["RightPupilDefects"]);
                auto.LeftPupilDefects = Convert.ToBoolean(dt.Rows[0]["LeftPupilDefects"]);
                auto.LeftSquint_Surgery = Convert.ToBoolean(dt.Rows[0]["LeftSquint_Surgery"]);
                auto.RightSquint_Surgery = Convert.ToBoolean(dt.Rows[0]["RightSquint_Surgery"]);



                auto.IPD = Convert.ToInt32(dt.Rows[0]["IPD"]);
                auto.VisualAcuity_RightEye = Convert.ToInt32(dt.Rows[0]["VisualAcuity_RightEye"]);
                auto.VisualAcuity_LeftEye = Convert.ToInt32(dt.Rows[0]["VisualAcuity_LeftEye"]);
                auto.RightVisualFieldTestId= Convert.ToInt32(dt.Rows[0]["RightVisualFieldTestId"]);
                auto.LeftVisualFieldTestId = Convert.ToInt32(dt.Rows[0]["LeftVisualFieldTestId"]);
                auto.RightCycloplagicdrops = Convert.ToBoolean(dt.Rows[0]["RightCycloplagicdrops"]);
                auto.RightMeridian1 = Convert.ToString(dt.Rows[0]["RightMeridian1"]);
                auto.RightMeridian2 = Convert.ToString(dt.Rows[0]["RightMeridian2"]);
                auto.RightAxisOfRetino = Convert.ToString(dt.Rows[0]["RightAxisOfRetino"]);
                auto.RightNoGlowVisibile = Convert.ToString(dt.Rows[0]["RightNoGlowVisibile"]);
                auto.Right_CycloDrops_Spherical_Status= Convert.ToChar(dt.Rows[0]["Right_CycloDrops_Spherical_Status"]);
                auto.Right_CycloDrops_Spherical_Points= Convert.ToDecimal(dt.Rows[0]["Right_CycloDrops_Spherical_Points"]);
                auto.Right_CycloDrops_Cyclinderical_Status= Convert.ToChar(dt.Rows[0]["Right_CycloDrops_Cyclinderical_Status"]);
                auto.Right_CycloDrops_Cyclinderical_Points = Convert.ToDecimal(dt.Rows[0]["Right_CycloDrops_Cyclinderical_Points"]);
                auto.Right_CycloDrops_Axix = Convert.ToInt32(dt.Rows[0]["Right_CycloDrops_Axix"]);
                auto.Right_CycloDrops_FinalPrescription = Convert.ToString(dt.Rows[0]["Right_CycloDrops_FinalPrescription"]);  
                auto.LeftCycloplagicdrops = Convert.ToBoolean(dt.Rows[0]["LeftCycloplagicdrops"]);
                auto.LeftMeridian1 = Convert.ToString(dt.Rows[0]["LeftMeridian1"]);
                auto.LeftMeridian2 = Convert.ToString(dt.Rows[0]["LeftMeridian2"]);
                auto.LeftAxisOfRetino = Convert.ToString(dt.Rows[0]["LeftAxisOfRetino"]);
                auto.LeftNoGlowVisibile = Convert.ToString(dt.Rows[0]["LeftNoGlowVisibile"]);
                auto.Left_CycloDrops_Spherical_Status = Convert.ToChar(dt.Rows[0]["Left_CycloDrops_Spherical_Status"]);
                auto.Left_CycloDrops_Spherical_Points = Convert.ToDecimal(dt.Rows[0]["Left_CycloDrops_Spherical_Points"]);
                auto.Left_CycloDrops_Cyclinderical_Status = Convert.ToChar(dt.Rows[0]["Left_CycloDrops_Cyclinderical_Status"]);
                auto.Left_CycloDrops_Cyclinderical_Points = Convert.ToDecimal(dt.Rows[0]["Left_CycloDrops_Cyclinderical_Points"]);
                auto.Left_CycloDrops_Axix = Convert.ToInt32(dt.Rows[0]["Left_CycloDrops_Axix"]);
                auto.Left_CycloDrops_FinalPrescription = Convert.ToString(dt.Rows[0]["Left_CycloDrops_FinalPrescription"]);
                auto.TreatmentId = Convert.ToInt32(dt.Rows[0]["TreatmentId"]);
                auto.Medicines = Convert.ToString(dt.Rows[0]["Medicines"]);
                auto.Prescription = Convert.ToString(dt.Rows[0]["Prescription"]);
                auto.NextVisit_ReferToHospital = Convert.ToBoolean(dt.Rows[0]["NextVisit_ReferToHospital"]);
                


            }
            return auto;
        }
        public CompanyWorkerEnrollmentModel GetModel(DataTable dt)
        {
            CompanyWorkerEnrollmentModel CM = new CompanyWorkerEnrollmentModel();
            foreach (DataRow row in dt.Rows)
            {
                CM.WorkerAutoId = Convert.ToInt32(row["WorkerAutoId"]);
                CM.CompanyAutoId = Convert.ToInt32(row["CompanyAutoId"]);
                CM.CompanyCode = row["companyCode"].ToString();
                CM.WorkerCode = row["WorkerCode"].ToString();
                CM.WorkerName = row["WorkerName"].ToString();
                CM.RelationType = row["RelationType"].ToString();
                CM.RelationName = row["RelationName"].ToString();
                CM.Age = Convert.ToInt32(row["Age"].ToString());
                CM.GenderAutoId = Convert.ToInt32(row["GenderAutoId"].ToString());
                CM.CNIC = row["CNIC"].ToString();
                CM.DecreasedVision = Convert.ToBoolean(row["DecreasedVision"].ToString());
                CM.Near = Convert.ToBoolean(row["Near"].ToString());
                CM.Distance = Convert.ToBoolean(row["Distance"].ToString());
                CM.WearGlasses = Convert.ToBoolean(row["WearGlasses"].ToString());
                CM.Religion = Convert.ToBoolean(row["Religion"].ToString());
                CM.MobileNo = row["MobileNo"].ToString();
                CM.EnrollementDate = Convert.ToDateTime(row["EnrollmentDate"].ToString());
            }
            return CM;
        }
    }
}
