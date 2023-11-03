using System;
using System.Collections;

namespace TransportManagement.ReportClass
{
    public class EyeGlassPrescriptionComponent
    {
        public EyeGlassPrescriptionComponent()
        {

        }
    }
}

namespace EyeGlassPrescriptionProperties
{
    #region Properties_Collection
    public class ProcedureParameters
    {
        int _SchoolAutoId;
        int _ClassAutoId;
        int _SectionAutoId;
        int _StudentAutoId;
        string _StudentCode;
        string _StudentName;
        string _SchoolName;
        string _ClassCode;
        string _ClassSection;
        int _Age;
        string _OptometristStudentTransDate;
        string _OptometristName;
        string _Spherical_Right_Eye;
        string _Cyclinderical_Right_Eye;
        string _Axix_Right_Eye;
        string _NearAdd_Right_Eye;
        string _Spherical_Left_Eye;
        string _Cyclinderical_Left_Eye;
        string _Axix_Left_Eye;
        string _NearAdd_Left_Eye;

        public int SchoolAutoId
        {
            get { return _SchoolAutoId; }
            set { _SchoolAutoId = value; }
        }

        public int ClassAutoId
        {
            get { return _ClassAutoId; }
            set { _ClassAutoId = value; }
        }

        public int SectionAutoId
        {
            get { return _SectionAutoId; }
            set { _SectionAutoId = value; }
        }

        public int StudentAutoId
        {
            get { return _StudentAutoId; }
            set { _StudentAutoId = value; }
        }

        public string StudentCode
        {
            get { return _StudentCode; }
            set { _StudentCode = value; }
        }

        public string StudentName
        {
            get { return _StudentName; }
            set { _StudentName = value; }
        }

        public string SchoolName
        {
            get { return _SchoolName; }
            set { _SchoolName = value; }
        }

        public string ClassCode
        {
            get { return _ClassCode; }
            set { _ClassCode = value; }
        }

        public string ClassSection
        {
            get { return _ClassSection; }
            set { _ClassSection = value; }
        }

        public int Age
        {
            get { return _Age; }
            set { _Age = value; }
        }

        public string OptometristStudentTransDate
        {
            get { return _OptometristStudentTransDate; }
            set { _OptometristStudentTransDate = value; }
        }

        public string OptometristName
        {
            get { return _OptometristName; }
            set { _OptometristName = value; }
        }

        public string Spherical_Right_Eye
        {
            get { return _Spherical_Right_Eye; }
            set { _Spherical_Right_Eye = value; }
        }

        public string Cyclinderical_Right_Eye
        {
            get { return _Cyclinderical_Right_Eye; }
            set { _Cyclinderical_Right_Eye = value; }
        }

        public string Axix_Right_Eye
        {
            get { return _Axix_Right_Eye; }
            set { _Axix_Right_Eye = value; }
        }

        public string NearAdd_Right_Eye
        {
            get { return _NearAdd_Right_Eye; }
            set { _NearAdd_Right_Eye = value; }
        }

        public string Spherical_Left_Eye
        {
            get { return _Spherical_Left_Eye; }
            set { _Spherical_Left_Eye = value; }
        }

        public string Cyclinderical_Left_Eye
        {
            get { return _Cyclinderical_Left_Eye; }
            set { _Cyclinderical_Left_Eye = value; }
        }

        public string Axix_Left_Eye
        {
            get { return _Axix_Left_Eye; }
            set { _Axix_Left_Eye = value; }
        }

        public string NearAdd_Left_Eye
        {
            get { return _NearAdd_Left_Eye; }
            set { _NearAdd_Left_Eye = value; }
        }
    }

    public class ProcedureParametersCollection : CollectionBase
    {
        public void Add(ProcedureParameters app)
        {
            List.Add(app);
        }
        public void Delete(ProcedureParameters app)
        {
            List.Remove(app);
        }
        public void Add(int index)
        {
            if (index < 0 || index > List.Count)
            {
                throw new Exception("");
            }
            List.RemoveAt(index);
        }
    }
    #endregion Properties_Collection
}

namespace EyeGlassPrescriptionProperties_Teacher
{
    #region Properties_Collection
    public class ProcedureParameters
    {
        int _SchoolAutoId;
        int _TeacherAutoId;
        string _TeacherCode;
        string _TeacherName;
        string _SchoolName;
        int _Age;
        string _OptometristTeacherTransDate;
        string _OptometristName;
        string _Spherical_Right_Eye;
        string _Cyclinderical_Right_Eye;
        string _Axix_Right_Eye;
        string _NearAdd_Right_Eye;
        string _Spherical_Left_Eye;
        string _Cyclinderical_Left_Eye;
        string _Axix_Left_Eye;
        string _NearAdd_Left_Eye;

        public int SchoolAutoId
        {
            get { return _SchoolAutoId; }
            set { _SchoolAutoId = value; }
        }

        public int TeacherAutoId
        {
            get { return _TeacherAutoId; }
            set { _TeacherAutoId = value; }
        }

        public string TeacherCode
        {
            get { return _TeacherCode; }
            set { _TeacherCode = value; }
        }

        public string TeacherName
        {
            get { return _TeacherName; }
            set { _TeacherName = value; }
        }

        public string SchoolName
        {
            get { return _SchoolName; }
            set { _SchoolName = value; }
        }

        public int Age
        {
            get { return _Age; }
            set { _Age = value; }
        }

        public string OptometristTeacherTransDate
        {
            get { return _OptometristTeacherTransDate; }
            set { _OptometristTeacherTransDate = value; }
        }

        public string OptometristName
        {
            get { return _OptometristName; }
            set { _OptometristName = value; }
        }

        public string Spherical_Right_Eye
        {
            get { return _Spherical_Right_Eye; }
            set { _Spherical_Right_Eye = value; }
        }

        public string Cyclinderical_Right_Eye
        {
            get { return _Cyclinderical_Right_Eye; }
            set { _Cyclinderical_Right_Eye = value; }
        }

        public string Axix_Right_Eye
        {
            get { return _Axix_Right_Eye; }
            set { _Axix_Right_Eye = value; }
        }

        public string NearAdd_Right_Eye
        {
            get { return _NearAdd_Right_Eye; }
            set { _NearAdd_Right_Eye = value; }
        }

        public string Spherical_Left_Eye
        {
            get { return _Spherical_Left_Eye; }
            set { _Spherical_Left_Eye = value; }
        }

        public string Cyclinderical_Left_Eye
        {
            get { return _Cyclinderical_Left_Eye; }
            set { _Cyclinderical_Left_Eye = value; }
        }

        public string Axix_Left_Eye
        {
            get { return _Axix_Left_Eye; }
            set { _Axix_Left_Eye = value; }
        }

        public string NearAdd_Left_Eye
        {
            get { return _NearAdd_Left_Eye; }
            set { _NearAdd_Left_Eye = value; }
        }
    }

    public class ProcedureParametersCollection : CollectionBase
    {
        public void Add(ProcedureParameters app)
        {
            List.Add(app);
        }
        public void Delete(ProcedureParameters app)
        {
            List.Remove(app);
        }
        public void Add(int index)
        {
            if (index < 0 || index > List.Count)
            {
                throw new Exception("");
            }
            List.RemoveAt(index);
        }
    }
    #endregion Properties_Collection
}

namespace DailyReportProperties
{
    #region Properties_Collection
    public class ProcedureParameters
    {
        int _SchoolAutoId;
        string _SchoolName;
        DateTime _TransactionDate;
        int _Enrolled_Student;
        int _AutoRef_Student;
        int _Optometrist_Student;
        int _suggestedGlasses_Student;
        int _forCycloplagicRefraction_Student;
        int _withotherissues_Student;
        int _Surgery_Student;
        int _Enrolled_Teacher;
        int _AutoRef_Teacher;
        int _Optometrist_Teacher;
        int _suggestedGlasses_Teacher;
        int _forCycloplagicRefraction_Teacher;
        int _withotherissues_Teacher;
        int _Surgery_Teacher;

        public int SchoolAutoId
        {
            get { return _SchoolAutoId; }
            set { _SchoolAutoId = value; }
        }

        public string SchoolName
        {
            get { return _SchoolName; }
            set { _SchoolName = value; }
        }

        public DateTime TransactionDate
        {
            get { return _TransactionDate; }
            set { _TransactionDate = value; }
        }

        public int Enrolled_Student
        {
            get { return _Enrolled_Student; }
            set { _Enrolled_Student = value; }
        }

        public int AutoRef_Student
        {
            get { return _AutoRef_Student; }
            set { _AutoRef_Student = value; }
        }

        public int Optometrist_Student
        {
            get { return _Optometrist_Student; }
            set { _Optometrist_Student = value; }
        }

        public int suggestedGlasses_Student
        {
            get { return _suggestedGlasses_Student; }
            set { _suggestedGlasses_Student = value; }
        }

        public int forCycloplagicRefraction_Student
        {
            get { return _forCycloplagicRefraction_Student; }
            set { _forCycloplagicRefraction_Student = value; }
        }

        public int withotherissues_Student
        {
            get { return _withotherissues_Student; }
            set { _withotherissues_Student = value; }
        }

        public int Surgery_Student
        {
            get { return _Surgery_Student; }
            set { _Surgery_Student = value; }
        }

        public int Enrolled_Teacher
        {
            get { return _Enrolled_Teacher; }
            set { _Enrolled_Teacher = value; }
        }

        public int AutoRef_Teacher
        {
            get { return _AutoRef_Teacher; }
            set { _AutoRef_Teacher = value; }
        }

        public int Optometrist_Teacher
        {
            get { return _Optometrist_Teacher; }
            set { _Optometrist_Teacher = value; }
        }

        public int suggestedGlasses_Teacher
        {
            get { return _suggestedGlasses_Teacher; }
            set { _suggestedGlasses_Teacher = value; }
        }

        public int forCycloplagicRefraction_Teacher
        {
            get { return _forCycloplagicRefraction_Teacher; }
            set { _forCycloplagicRefraction_Teacher = value; }
        }

        public int withotherissues_Teacher
        {
            get { return _withotherissues_Teacher; }
            set { _withotherissues_Teacher = value; }
        }

        public int Surgery_Teacher
        {
            get { return _Surgery_Teacher; }
            set { _Surgery_Teacher = value; }
        }
    }

    public class ProcedureParametersCollection : CollectionBase
    {
        public void Add(ProcedureParameters app)
        {
            List.Add(app);
        }
        public void Delete(ProcedureParameters app)
        {
            List.Remove(app);
        }
        public void Add(int index)
        {
            if (index < 0 || index > List.Count)
            {
                throw new Exception("");
            }
            List.RemoveAt(index);
        }
    }
    #endregion Properties_Collection
}

namespace ReportforOptician_StudentProperties
{
    #region Properties_Collection
    public class ProcedureParameters
    {
        int _SchoolAutoId;
        int _ClassAutoId;
        int _SectionAutoId;
        int _StudentAutoId;
        string _StudentCode;
        string _StudentName;
        string _SchoolName;
        string _ClassCode;
        string _ClassSection;
        int _Age;
        string _OptometristStudentTransDate;
        string _OptometristName;
        string _Spherical_Right_Eye;
        string _Cyclinderical_Right_Eye;
        string _Axix_Right_Eye;
        string _NearAdd_Right_Eye;
        string _Spherical_Left_Eye;
        string _Cyclinderical_Left_Eye;
        string _Axix_Left_Eye;
        string _NearAdd_Left_Eye;

        public int SchoolAutoId
        {
            get { return _SchoolAutoId; }
            set { _SchoolAutoId = value; }
        }

        public int ClassAutoId
        {
            get { return _ClassAutoId; }
            set { _ClassAutoId = value; }
        }

        public int SectionAutoId
        {
            get { return _SectionAutoId; }
            set { _SectionAutoId = value; }
        }

        public int StudentAutoId
        {
            get { return _StudentAutoId; }
            set { _StudentAutoId = value; }
        }

        public string StudentCode
        {
            get { return _StudentCode; }
            set { _StudentCode = value; }
        }

        public string StudentName
        {
            get { return _StudentName; }
            set { _StudentName = value; }
        }

        public string SchoolName
        {
            get { return _SchoolName; }
            set { _SchoolName = value; }
        }

        public string ClassCode
        {
            get { return _ClassCode; }
            set { _ClassCode = value; }
        }

        public string ClassSection
        {
            get { return _ClassSection; }
            set { _ClassSection = value; }
        }

        public int Age
        {
            get { return _Age; }
            set { _Age = value; }
        }

        public string OptometristStudentTransDate
        {
            get { return _OptometristStudentTransDate; }
            set { _OptometristStudentTransDate = value; }
        }

        public string OptometristName
        {
            get { return _OptometristName; }
            set { _OptometristName = value; }
        }

        public string Spherical_Right_Eye
        {
            get { return _Spherical_Right_Eye; }
            set { _Spherical_Right_Eye = value; }
        }

        public string Cyclinderical_Right_Eye
        {
            get { return _Cyclinderical_Right_Eye; }
            set { _Cyclinderical_Right_Eye = value; }
        }

        public string Axix_Right_Eye
        {
            get { return _Axix_Right_Eye; }
            set { _Axix_Right_Eye = value; }
        }

        public string NearAdd_Right_Eye
        {
            get { return _NearAdd_Right_Eye; }
            set { _NearAdd_Right_Eye = value; }
        }

        public string Spherical_Left_Eye
        {
            get { return _Spherical_Left_Eye; }
            set { _Spherical_Left_Eye = value; }
        }

        public string Cyclinderical_Left_Eye
        {
            get { return _Cyclinderical_Left_Eye; }
            set { _Cyclinderical_Left_Eye = value; }
        }

        public string Axix_Left_Eye
        {
            get { return _Axix_Left_Eye; }
            set { _Axix_Left_Eye = value; }
        }

        public string NearAdd_Left_Eye
        {
            get { return _NearAdd_Left_Eye; }
            set { _NearAdd_Left_Eye = value; }
        }
    }

    public class ProcedureParametersCollection : CollectionBase
    {
        public void Add(ProcedureParameters app)
        {
            List.Add(app);
        }
        public void Delete(ProcedureParameters app)
        {
            List.Remove(app);
        }
        public void Add(int index)
        {
            if (index < 0 || index > List.Count)
            {
                throw new Exception("");
            }
            List.RemoveAt(index);
        }
    }
    #endregion Properties_Collection
}

namespace ReportforOptician_TeacherProperties
{
    #region Properties_Collection
    public class ProcedureParameters
    {
        int _SchoolAutoId;
        int _TeacherAutoId;
        string _TeacherCode;
        string _TeacherName;
        string _SchoolName;
        int _Age;
        string _OptometristTeacherTransDate;
        string _OptometristName;
        string _Spherical_Right_Eye;
        string _Cyclinderical_Right_Eye;
        string _Axix_Right_Eye;
        string _NearAdd_Right_Eye;
        string _Spherical_Left_Eye;
        string _Cyclinderical_Left_Eye;
        string _Axix_Left_Eye;
        string _NearAdd_Left_Eye;

        public int SchoolAutoId
        {
            get { return _SchoolAutoId; }
            set { _SchoolAutoId = value; }
        }

        public int TeacherAutoId
        {
            get { return _TeacherAutoId; }
            set { _TeacherAutoId = value; }
        }

        public string TeacherCode
        {
            get { return _TeacherCode; }
            set { _TeacherCode = value; }
        }

        public string TeacherName
        {
            get { return _TeacherName; }
            set { _TeacherName = value; }
        }

        public string SchoolName
        {
            get { return _SchoolName; }
            set { _SchoolName = value; }
        }

        public int Age
        {
            get { return _Age; }
            set { _Age = value; }
        }

        public string OptometristTeacherTransDate
        {
            get { return _OptometristTeacherTransDate; }
            set { _OptometristTeacherTransDate = value; }
        }

        public string OptometristName
        {
            get { return _OptometristName; }
            set { _OptometristName = value; }
        }

        public string Spherical_Right_Eye
        {
            get { return _Spherical_Right_Eye; }
            set { _Spherical_Right_Eye = value; }
        }

        public string Cyclinderical_Right_Eye
        {
            get { return _Cyclinderical_Right_Eye; }
            set { _Cyclinderical_Right_Eye = value; }
        }

        public string Axix_Right_Eye
        {
            get { return _Axix_Right_Eye; }
            set { _Axix_Right_Eye = value; }
        }

        public string NearAdd_Right_Eye
        {
            get { return _NearAdd_Right_Eye; }
            set { _NearAdd_Right_Eye = value; }
        }

        public string Spherical_Left_Eye
        {
            get { return _Spherical_Left_Eye; }
            set { _Spherical_Left_Eye = value; }
        }

        public string Cyclinderical_Left_Eye
        {
            get { return _Cyclinderical_Left_Eye; }
            set { _Cyclinderical_Left_Eye = value; }
        }

        public string Axix_Left_Eye
        {
            get { return _Axix_Left_Eye; }
            set { _Axix_Left_Eye = value; }
        }

        public string NearAdd_Left_Eye
        {
            get { return _NearAdd_Left_Eye; }
            set { _NearAdd_Left_Eye = value; }
        }
    }

    public class ProcedureParametersCollection : CollectionBase
    {
        public void Add(ProcedureParameters app)
        {
            List.Add(app);
        }
        public void Delete(ProcedureParameters app)
        {
            List.Remove(app);
        }
        public void Add(int index)
        {
            if (index < 0 || index > List.Count)
            {
                throw new Exception("");
            }
            List.RemoveAt(index);
        }
    }
    #endregion Properties_Collection
}

namespace ComprehensiveSummaryReport_StudentProperties
{
    #region Properties_Collection
    public class ProcedureParameters
    {
        int _SchoolAutoId;
        int _ClassAutoId;
        int _SectionAutoId;
        int _StudentAutoId;
        string _StudentCode;
        string _StudentName;
        string _SchoolName;
        string _ClassCode;
        string _ClassSection;
        int _Age;
        int _Normal;
        int _WearingGlasses;
        int _Refractive_Error;
        int _Needs_cyclopegic_refration;
        int _Squint_Strabismus;
        int _LazyEye_Amblyopia;
        int _Colorblindness_Achromatopsia;
        int _Cataract;
        int _Traumatic_Cataract;
        int _Keratoconus;
        int _Anisometropia;
        int _Ptosis;
        int _Nystagmus;
        int _Presbyopia;
        int _Other;

        public int SchoolAutoId
        {
            get { return _SchoolAutoId; }
            set { _SchoolAutoId = value; }
        }

        public int ClassAutoId
        {
            get { return _ClassAutoId; }
            set { _ClassAutoId = value; }
        }

        public int SectionAutoId
        {
            get { return _SectionAutoId; }
            set { _SectionAutoId = value; }
        }

        public int StudentAutoId
        {
            get { return _StudentAutoId; }
            set { _StudentAutoId = value; }
        }

        public string StudentCode
        {
            get { return _StudentCode; }
            set { _StudentCode = value; }
        }

        public string StudentName
        {
            get { return _StudentName; }
            set { _StudentName = value; }
        }

        public string SchoolName
        {
            get { return _SchoolName; }
            set { _SchoolName = value; }
        }

        public string ClassCode
        {
            get { return _ClassCode; }
            set { _ClassCode = value; }
        }

        public string ClassSection
        {
            get { return _ClassSection; }
            set { _ClassSection = value; }
        }

        public int Age
        {
            get { return _Age; }
            set { _Age = value; }
        }

        public int Normal
        {
            get { return _Normal; }
            set { _Normal = value; }
        }

        public int WearingGlasses
        {
            get { return _WearingGlasses; }
            set { _WearingGlasses = value; }
        }

        public int Refractive_Error
        {
            get { return _Refractive_Error; }
            set { _Refractive_Error = value; }
        }

        public int Needs_cyclopegic_refration
        {
            get { return _Needs_cyclopegic_refration; }
            set { _Needs_cyclopegic_refration = value; }
        }

        public int Squint_Strabismus
        {
            get { return _Squint_Strabismus; }
            set { _Squint_Strabismus = value; }
        }

        public int LazyEye_Amblyopia
        {
            get { return _LazyEye_Amblyopia; }
            set { _LazyEye_Amblyopia = value; }
        }

        public int Colorblindness_Achromatopsia
        {
            get { return _Colorblindness_Achromatopsia; }
            set { _Colorblindness_Achromatopsia = value; }
        }
        public int Cataract
        {
            get { return _Cataract; }
            set { _Cataract = value; }
        }

        public int Traumatic_Cataract
        {
            get { return _Traumatic_Cataract; }
            set { _Traumatic_Cataract = value; }
        }

        public int Keratoconus
        {
            get { return _Keratoconus; }
            set { _Keratoconus = value; }
        }

        public int Anisometropia
        {
            get { return _Anisometropia; }
            set { _Anisometropia = value; }
        }

        public int Ptosis
        {
            get { return _Ptosis; }
            set { _Ptosis = value; }
        }

        public int Nystagmus
        {
            get { return _Nystagmus; }
            set { _Nystagmus = value; }
        }

        public int Presbyopia
        {
            get { return _Presbyopia; }
            set { _Presbyopia = value; }
        }

        public int Other
        {
            get { return _Other; }
            set { _Other = value; }
        }

    }

    public class ProcedureParametersCollection : CollectionBase
    {
        public void Add(ProcedureParameters app)
        {
            List.Add(app);
        }
        public void Delete(ProcedureParameters app)
        {
            List.Remove(app);
        }
        public void Add(int index)
        {
            if (index < 0 || index > List.Count)
            {
                throw new Exception("");
            }
            List.RemoveAt(index);
        }
    }
    #endregion Properties_Collection
}


