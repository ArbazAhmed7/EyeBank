 
/****** Object:  Table [dbo].[tblCompany]    Script Date: 10/9/2023 3:30:18 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCompany]') AND type in (N'U'))
DROP TABLE [dbo].[tblCompany]
GO

/****** Object:  Table [dbo].[tblCompany]    Script Date: 10/9/2023 3:30:18 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblCompany](
	[CompanyAutoId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyCode] [nvarchar](10) NOT NULL,
	[CompanyName] [nvarchar](250) NOT NULL,
	[Website] [nvarchar](250) NULL,
	[Address1] [nvarchar](1000) NULL,
	[Address2] [nvarchar](1000) NULL,
	[Address3] [nvarchar](1000) NULL,
	[Town] [varchar](100) NULL,
	[District] [nvarchar](1000) NULL,
	[City] [nvarchar](100) NULL,
	[WorkForce] [int] NULL,
	[OwnerName] [varchar](100) NULL,
	[OwnerMobile] [varchar](50) NULL,
	[OwnerEmail] [varchar](100) NULL,
	[AdminHeadName] [varchar](100) NULL,
	[AdminHeadMobile] [varchar](50) NULL,
	[AdminHeadEmail] [varchar](100) NULL,
	[HRHeadName] [varchar](100) NULL,
	[HRHeadMobile] [varchar](50) NULL,
	[HRHeadEmail] [varchar](100) NULL,
	[TitleAutoId] [int] NULL,
	[UserId] [nvarchar](250) NULL,
	[EntDate] [datetime] NULL,
	[EntOperation] [nvarchar](100) NULL,
	[EntTerminal] [nvarchar](200) NULL,
	[EntTerminalIP] [nvarchar](200) NULL,
	[FormId] [nvarchar](250) NULL,
	[UserEmpId] [int] NULL,
	[UserEmpName] [nvarchar](250) NULL,
	[UserEmpCode] [nvarchar](10) NULL,
	[EnrollmentDate] [datetime] NULL,
 CONSTRAINT [PK_tblCompany] PRIMARY KEY CLUSTERED 
(
	[CompanyAutoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_CompanyCode] UNIQUE NONCLUSTERED 
(
	[CompanyCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


 
/****** Object:  Table [dbo].[tblCompanyImage]    Script Date: 10/9/2023 3:30:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCompanyImage]') AND type in (N'U'))
DROP TABLE [dbo].[tblCompanyImage]
GO

/****** Object:  Table [dbo].[tblCompanyImage]    Script Date: 10/9/2023 3:30:34 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblCompanyImage](
	[CompanyImageAutoId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyAutoId] [int] NOT NULL,
	[CompanyPic] [varchar](max) NULL,
	[FileType] [nvarchar](10) NULL,
	[FileSize] [int] NULL,
	[CaptureDate] [datetime] NULL,
	[CaptureRemarks] [nvarchar](200) NULL,
	[UserId] [nvarchar](500) NULL,
	[EntDate] [datetime] NULL,
	[EntOperation] [nvarchar](200) NULL,
	[EntTerminal] [nvarchar](400) NULL,
	[EntTerminalIP] [nvarchar](400) NULL,
 CONSTRAINT [PK_tblCompanyImage] PRIMARY KEY CLUSTERED 
(
	[CompanyImageAutoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


 
/****** Object:  Table [dbo].[tblCompanyWorker]    Script Date: 10/9/2023 3:30:48 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCompanyWorker]') AND type in (N'U'))
DROP TABLE [dbo].[tblCompanyWorker]
GO

/****** Object:  Table [dbo].[tblCompanyWorker]    Script Date: 10/9/2023 3:30:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblCompanyWorker](
	[WorkerAutoId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyAutoId] [int] NULL,
	[WorkerCode] [nvarchar](15) NOT NULL,
	[WorkerName] [nvarchar](500) NOT NULL,
	[RelationType] [nvarchar](100) NULL,
	[RelationName] [nvarchar](500) NULL,
	[Age] [int] NULL,
	[GenderAutoId] [int] NULL,
	[CNIC] [varchar](30) NULL,
	[UserId] [nvarchar](250) NULL,
	[EntDate] [datetime] NULL,
	[EntOperation] [nvarchar](100) NULL,
	[EntTerminal] [nvarchar](200) NULL,
	[EntTerminalIP] [nvarchar](200) NULL,
	[WearGlasses] [bit] NULL,
	[Distance] [bit] NULL,
	[Near] [bit] NULL,
	[DecreasedVision] [bit] NULL,
	[HasOccularHistory] [bit] NULL,
	[OccularHistoryRemarks] [nvarchar](500) NULL,
	[HasMedicalHistory] [bit] NULL,
	[MedicalHistoryRemarks] [nvarchar](500) NULL,
	[HasChiefComplain] [bit] NULL,
	[ChiefComplainRemarks] [nvarchar](500) NULL,
	[WorkerTestDate] [datetime] NULL,
	[WorkerRegNo] [varchar](20) NULL,
	[SectionAutoId] [int] NULL,
	[ApplicationID] [nvarchar](20) NULL,
	[FormId] [nvarchar](250) NULL,
	[UserEmpId] [int] NULL,
	[UserEmpName] [nvarchar](250) NULL,
	[UserEmpCode] [nvarchar](10) NULL,
	[Religion] [bit] NULL,
	[MobileNo] [varchar](30) NULL,
	[CompanyCode] [varchar](20) NULL,
 CONSTRAINT [PK_tblWorker] PRIMARY KEY CLUSTERED 
(
	[WorkerAutoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_WorkerCode] UNIQUE NONCLUSTERED 
(
	[WorkerCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


 
/****** Object:  Table [dbo].[tblCompanyWorkerImage]    Script Date: 10/9/2023 3:31:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCompanyWorkerImage]') AND type in (N'U'))
DROP TABLE [dbo].[tblCompanyWorkerImage]
GO

/****** Object:  Table [dbo].[tblCompanyWorkerImage]    Script Date: 10/9/2023 3:31:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblCompanyWorkerImage](
	[WorkerImageAutoId] [int] IDENTITY(1,1) NOT NULL,
	[WorkerAutoId] [int] NOT NULL,
	[CompanyAutoId] [int] NOT NULL,
	[WorkerPic] [varchar](max) NULL,
	[FileType] [nvarchar](10) NULL,
	[FileSize] [int] NULL,
	[CaptureDate] [datetime] NULL,
	[CaptureRemarks] [nvarchar](200) NULL,
	[UserId] [nvarchar](500) NULL,
	[EntDate] [datetime] NULL,
	[EntOperation] [nvarchar](200) NULL,
	[EntTerminal] [nvarchar](400) NULL,
	[EntTerminalIP] [nvarchar](400) NULL,
 CONSTRAINT [PK_tblCompanyWorkerImage] PRIMARY KEY CLUSTERED 
(
	[WorkerImageAutoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


 
/****** Object:  Table [dbo].[tblAutoRefTestWorker]    Script Date: 10/9/2023 3:31:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblAutoRefTestWorker]') AND type in (N'U'))
DROP TABLE [dbo].[tblAutoRefTestWorker]
GO

/****** Object:  Table [dbo].[tblAutoRefTestWorker]    Script Date: 10/9/2023 3:31:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblAutoRefTestWorker](
	[AutoRefWorkerId] [int] IDENTITY(1,1) NOT NULL,
	[AutoRefWorkerTransId] [varchar](15) NULL,
	[AutoRefWorkerTransDate] [datetime] NULL,
	[WorkerAutoId] [int] NULL,
	[Right_Spherical_Status] [char](1) NULL,
	[Right_Spherical_Points] [decimal](9, 2) NULL,
	[Right_Cyclinderical_Status] [char](1) NULL,
	[Right_Cyclinderical_Points] [decimal](9, 2) NULL,
	[Right_Axix_From] [int] NULL,
	[Right_Axix_To] [int] NULL,
	[Left_Spherical_Status] [char](1) NULL,
	[Left_Spherical_Points] [decimal](9, 2) NULL,
	[Left_Cyclinderical_Status] [char](1) NULL,
	[Left_Cyclinderical_Points] [decimal](9, 2) NULL,
	[Left_Axix_From] [int] NULL,
	[Left_Axix_To] [int] NULL,
	[UserId] [nvarchar](250) NULL,
	[EntDate] [datetime] NULL,
	[EntOperation] [nvarchar](100) NULL,
	[EntTerminal] [nvarchar](200) NULL,
	[EntTerminalIP] [nvarchar](200) NULL,
	[ApplicationID] [nvarchar](20) NULL,
	[FormId] [nvarchar](250) NULL,
	[UserEmpId] [int] NULL,
	[UserEmpName] [nvarchar](250) NULL,
	[UserEmpCode] [nvarchar](10) NULL,
	[IPD] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[AutoRefWorkerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UNIQUE_tblAutoRefTestWorker] UNIQUE NONCLUSTERED 
(
	[AutoRefWorkerId] ASC,
	[AutoRefWorkerTransDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


 
/****** Object:  Table [dbo].[tblOptometristWorker]    Script Date: 10/9/2023 3:32:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblOptometristWorker]') AND type in (N'U'))
DROP TABLE [dbo].[tblOptometristWorker]
GO

/****** Object:  Table [dbo].[tblOptometristWorker]    Script Date: 10/9/2023 3:32:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblOptometristWorker](
	[OptometristWorkerId] [int] IDENTITY(1,1) NOT NULL,
	[OptometristWorkerTransDate] [datetime] NULL,
	[WorkerAutoId] [int] NULL,
	[CompanyAutoId] [int] NULL,
	[HasChiefComplain] [int] NULL,
	[ChiefComplainRemarks] [nvarchar](200) NULL,
	[HasOccularHistory] [int] NULL,
	[OccularHistoryRemarks] [nvarchar](200) NULL,
	[HasMedicalHistory] [int] NULL,
	[MedicalHistoryRemarks] [nvarchar](200) NULL,
	[DistanceVision_RightEye_Unaided] [int] NULL,
	[DistanceVision_RightEye_WithGlasses] [int] NULL,
	[DistanceVision_RightEye_PinHole] [int] NULL,
	[NearVision_RightEye] [int] NULL,
	[NeedCycloRefraction_RightEye] [int] NULL,
	[NeedCycloRefractionRemarks_RightEye] [nvarchar](200) NULL,
	[DistanceVision_LeftEye_Unaided] [int] NULL,
	[DistanceVision_LeftEye_WithGlasses] [int] NULL,
	[DistanceVision_LeftEye_PinHole] [int] NULL,
	[NearVision_LeftEye] [int] NULL,
	[NeedCycloRefraction_LeftEye] [int] NULL,
	[NeedCycloRefractionRemarks_LeftEye] [nvarchar](200) NULL,
	[Right_Spherical_Status] [char](1) NULL,
	[Right_Spherical_Points] [decimal](9, 2) NULL,
	[Right_Cyclinderical_Status] [char](1) NULL,
	[Right_Cyclinderical_Points] [decimal](9, 2) NULL,
	[Right_Axix_From] [int] NULL,
	[Right_Axix_To] [int] NULL,
	[Right_Near_Status] [char](1) NULL,
	[Right_Near_Points] [decimal](9, 2) NULL,
	[Left_Spherical_Status] [char](1) NULL,
	[Left_Spherical_Points] [decimal](9, 2) NULL,
	[Left_Cyclinderical_Status] [char](1) NULL,
	[Left_Cyclinderical_Points] [decimal](9, 2) NULL,
	[Left_Axix_From] [int] NULL,
	[Left_Axix_To] [int] NULL,
	[Left_Near_Status] [char](1) NULL,
	[Left_Near_Points] [decimal](9, 2) NULL,
	[VisualAcuity_RightEye] [int] NULL,
	[VisualAcuity_LeftEye] [int] NULL,
	[LeftSquint_VA] [bit] NULL,
	[RightSquint_VA] [bit] NULL,
	[LeftAmblyopic_VA] [bit] NULL,
	[RightAmblyopic_VA] [bit] NULL,
	[AutoRefWorkerId] [int] NULL,
	[Hirchberg_Distance] [int] NULL,
	[Hirchberg_Near] [int] NULL,
	[Ophthalmoscope_RightEye] [int] NULL,
	[Ophthalmoscope_LeftEye] [int] NULL,
	[PupillaryReactions_RightEye] [int] NULL,
	[CoverUncovertTest_RightEye] [int] NULL,
	[CoverUncovertTestRemarks_RightEye] [nvarchar](200) NULL,
	[ExtraOccularMuscleRemarks_RightEye] [nvarchar](200) NULL,
	[PupillaryReactions_LeftEye] [int] NULL,
	[CoverUncovertTest_LeftEye] [int] NULL,
	[CoverUncovertTestRemarks_LeftEye] [nvarchar](200) NULL,
	[CycloplegicRefraction_RightEye] [bit] NULL,
	[CycloplegicRefraction_LeftEye] [bit] NULL,
	[Conjunctivitis_RightEye] [bit] NULL,
	[Conjunctivitis_LeftEye] [bit] NULL,
	[Scleritis_RightEye] [bit] NULL,
	[Scleritis_LeftEye] [bit] NULL,
	[Nystagmus_RightEye] [bit] NULL,
	[Nystagmus_LeftEye] [bit] NULL,
	[CornealDefect_RightEye] [bit] NULL,
	[CornealDefect_LeftEye] [bit] NULL,
	[Cataract_RightEye] [bit] NULL,
	[Cataract_LeftEye] [bit] NULL,
	[Keratoconus_RightEye] [bit] NULL,
	[Keratoconus_LeftEye] [bit] NULL,
	[Ptosis_RightEye] [bit] NULL,
	[Ptosis_LeftEye] [bit] NULL,
	[LowVision_RightEye] [bit] NULL,
	[LowVision_LeftEye] [bit] NULL,
	[Pterygium_RightEye] [bit] NULL,
	[Pterygium_LeftEye] [bit] NULL,
	[ColorBlindness_RightEye] [bit] NULL,
	[ColorBlindness_LeftEye] [bit] NULL,
	[Others_RightEye] [bit] NULL,
	[Others_LeftEye] [bit] NULL,
	[Fundoscopy_RightEye] [bit] NULL,
	[Fundoscopy_LeftEye] [bit] NULL,
	[Surgery_RightEye] [bit] NULL,
	[Surgery_LeftEye] [bit] NULL,
	[CataractSurgery_RightEye] [bit] NULL,
	[CataractSurgery_LeftEye] [bit] NULL,
	[SurgeryPterygium_RightEye] [bit] NULL,
	[SurgeryPterygium_LeftEye] [bit] NULL,
	[SurgeryCornealDefect_RightEye] [bit] NULL,
	[SurgeryCornealDefect_LeftEye] [bit] NULL,
	[SurgeryPtosis_RightEye] [bit] NULL,
	[SurgeryPtosis_LeftEye] [bit] NULL,
	[SurgeryKeratoconus_RightEye] [bit] NULL,
	[SurgeryKeratoconus_LeftEye] [bit] NULL,
	[Chalazion_RightEye] [bit] NULL,
	[Chalazion_LeftEye] [bit] NULL,
	[Hordeolum_RightEye] [bit] NULL,
	[Hordeolum_LeftEye] [bit] NULL,
	[SurgeryOthers_RightEye] [bit] NULL,
	[SurgeryOthers_LeftEye] [bit] NULL,
	[Douchrome] [int] NULL,
	[Achromatopsia] [varchar](20) NULL,
	[RetinoScopy_RightEye] [int] NULL,
	[Condition_RightEye] [int] NULL,
	[Meridian1_RightEye] [varchar](200) NULL,
	[Meridian2_RightEye] [varchar](200) NULL,
	[FinalPrescription_RightEye] [varchar](200) NULL,
	[RetinoScopy_LeftEye] [int] NULL,
	[Condition_LeftEye] [varchar](200) NULL,
	[Meridian1_LeftEye] [varchar](200) NULL,
	[Meridian2_LeftEye] [varchar](200) NULL,
	[FinalPrescription_LeftEye] [varchar](200) NULL,
	[UserId] [nvarchar](250) NULL,
	[EntDate] [datetime] NULL,
	[EntOperation] [nvarchar](100) NULL,
	[EntTerminal] [nvarchar](200) NULL,
	[EntTerminalIP] [nvarchar](200) NULL,
	[ExtraOccularMuscleRemarks_LeftEye] [nvarchar](200) NULL,
	[ApplicationID] [nvarchar](20) NULL,
	[FormId] [nvarchar](250) NULL,
	[UserEmpId] [int] NULL,
	[UserEmpName] [nvarchar](250) NULL,
	[UserEmpCode] [nvarchar](10) NULL,
	[RightPupilDefects] [bit] NULL,
	[LeftPupilDefects] [bit] NULL,
	[RightAmblyopia] [bit] NULL,
	[LeftAmblyopia] [bit] NULL,
	[RightSquint_Surgery] [bit] NULL,
	[LeftSquint_Surgery] [bit] NULL,
	[IPD] [int] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [CycloplegicRefraction_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [CycloplegicRefraction_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [Conjunctivitis_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [Conjunctivitis_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [Scleritis_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [Scleritis_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [Nystagmus_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [Nystagmus_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [CornealDefect_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [CornealDefect_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [Cataract_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [Cataract_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [Keratoconus_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [Keratoconus_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [Ptosis_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [Ptosis_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [LowVision_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [LowVision_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [Pterygium_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [Pterygium_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [ColorBlindness_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [ColorBlindness_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [Others_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [Others_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [Fundoscopy_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [Fundoscopy_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [Surgery_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [Surgery_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [CataractSurgery_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [CataractSurgery_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [SurgeryPterygium_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [SurgeryPterygium_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [SurgeryCornealDefect_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [SurgeryCornealDefect_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [SurgeryPtosis_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [SurgeryPtosis_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [SurgeryKeratoconus_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [SurgeryKeratoconus_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [Chalazion_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [Chalazion_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [Hordeolum_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [Hordeolum_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [SurgeryOthers_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [SurgeryOthers_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [RightPupilDefects]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [LeftPupilDefects]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [RightAmblyopia]
GO

ALTER TABLE [dbo].[tblOptometristWorker] ADD  DEFAULT ((0)) FOR [LeftAmblyopia]
GO


 
/****** Object:  Table [dbo].[tblGlassDespenseWorker]    Script Date: 10/9/2023 3:33:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblGlassDespenseWorker]') AND type in (N'U'))
DROP TABLE [dbo].[tblGlassDespenseWorker]
GO

/****** Object:  Table [dbo].[tblGlassDespenseWorker]    Script Date: 10/9/2023 3:33:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblGlassDespenseWorker](
	[GlassDespenseWorkerId] [int] IDENTITY(1,1) NOT NULL,
	[OptometristWorkerId] [int] NOT NULL,
	[GlassDespenseWorkerTransDate] [datetime] NULL,
	[WorkerAutoId] [int] NULL,
	[VisionwithGlasses_RightEye] [int] NULL,
	[VisionwithGlasses_LeftEye] [int] NULL,
	[NearVA_RightEye] [int] NULL,
	[NearVA_LeftEye] [int] NULL,
	[WorkerSatisficaion] [int] NULL,
	[Unsatisfied] [int] NULL,
	[Unsatisfied_Remarks] [nvarchar](250) NULL,
	[Unsatisfied_Reason] [int] NULL,
	[UserId] [nvarchar](250) NULL,
	[EntDate] [datetime] NULL,
	[EntOperation] [nvarchar](100) NULL,
	[EntTerminal] [nvarchar](200) NULL,
	[EntTerminalIP] [nvarchar](200) NULL,
 CONSTRAINT [PK_tblGlassDespenseWorker] PRIMARY KEY CLUSTERED 
(
	[GlassDespenseWorkerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


 
/****** Object:  Table [dbo].[tblGoths]    Script Date: 10/9/2023 3:33:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblGoths]') AND type in (N'U'))
DROP TABLE [dbo].[tblGoths]
GO

/****** Object:  Table [dbo].[tblGoths]    Script Date: 10/9/2023 3:33:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblGoths](
	[GothAutoId] [int] IDENTITY(1,1) NOT NULL,
	[GothCode] [nvarchar](10) NOT NULL,
	[GothName] [nvarchar](250) NOT NULL,
	[Website] [nvarchar](250) NULL,
	[Address1] [nvarchar](1000) NULL,
	[Address2] [nvarchar](1000) NULL,
	[Address3] [nvarchar](1000) NULL,
	[Town] [varchar](100) NULL,
	[District] [nvarchar](1000) NULL,
	[City] [nvarchar](100) NULL,
	[NameofPerson] [varchar](100) NULL,
	[PersonMobile] [varchar](50) NULL,
	[PersonRole] [varchar](100) NULL,
	[TitleAutoId] [int] NULL,
	[UserId] [nvarchar](250) NULL,
	[EntDate] [datetime] NULL,
	[EntOperation] [nvarchar](100) NULL,
	[EntTerminal] [nvarchar](200) NULL,
	[EntTerminalIP] [nvarchar](200) NULL,
	[FormId] [nvarchar](250) NULL,
	[UserEmpId] [int] NULL,
	[UserEmpName] [nvarchar](250) NULL,
	[UserEmpCode] [nvarchar](10) NULL,
	[EnrollmentDate] [datetime] NULL
) ON [PRIMARY]
GO

 
/****** Object:  Table [dbo].[tblGothsResident]    Script Date: 10/9/2023 3:34:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblGothsResident]') AND type in (N'U'))
DROP TABLE [dbo].[tblGothsResident]
GO

/****** Object:  Table [dbo].[tblGothsResident]    Script Date: 10/9/2023 3:34:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblGothsResident](
	[ResidentAutoId] [int] IDENTITY(1,1) NOT NULL,
	[GothAutoId] [int] NULL,
	[ResidentCode] [nvarchar](15) NOT NULL,
	[ResidentName] [nvarchar](500) NOT NULL,
	[RelationType] [nvarchar](100) NULL,
	[RelationName] [nvarchar](500) NULL,
	[Age] [int] NULL,
	[GenderAutoId] [int] NULL,
	[CNIC] [varchar](30) NULL,
	[UserId] [nvarchar](250) NULL,
	[EntDate] [datetime] NULL,
	[EntOperation] [nvarchar](100) NULL,
	[EntTerminal] [nvarchar](200) NULL,
	[EntTerminalIP] [nvarchar](200) NULL,
	[WearGlasses] [bit] NULL,
	[Distance] [bit] NULL,
	[Near] [bit] NULL,
	[DecreasedVision] [bit] NULL,
	[HasOccularHistory] [bit] NULL,
	[OccularHistoryRemarks] [nvarchar](500) NULL,
	[HasMedicalHistory] [bit] NULL,
	[MedicalHistoryRemarks] [nvarchar](500) NULL,
	[HasChiefComplain] [bit] NULL,
	[ChiefComplainRemarks] [nvarchar](500) NULL,
	[ResidentTestDate] [datetime] NULL,
	[ResidentRegNo] [varchar](20) NULL,
	[SectionAutoId] [int] NULL,
	[ApplicationID] [nvarchar](20) NULL,
	[FormId] [nvarchar](250) NULL,
	[UserEmpId] [int] NULL,
	[UserEmpName] [nvarchar](250) NULL,
	[UserEmpCode] [nvarchar](10) NULL,
	[Religion] [bit] NULL,
	[MobileNo] [varchar](30) NULL,
	[GothCode] [varchar](20) NULL
) ON [PRIMARY]
GO



 
/****** Object:  Table [dbo].[tblGothsResidentImage]    Script Date: 10/9/2023 3:34:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblGothsResidentImage]') AND type in (N'U'))
DROP TABLE [dbo].[tblGothsResidentImage]
GO

/****** Object:  Table [dbo].[tblGothsResidentImage]    Script Date: 10/9/2023 3:34:33 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblGothsResidentImage](
	[ResidentImageAutoId] [int] IDENTITY(1,1) NOT NULL,
	[ResidentAutoId] [int] NOT NULL,
	[GothAutoId] [int] NOT NULL,
	[ResidentPic] [varchar](max) NULL,
	[FileType] [nvarchar](10) NULL,
	[FileSize] [int] NULL,
	[CaptureDate] [datetime] NULL,
	[CaptureRemarks] [nvarchar](200) NULL,
	[UserId] [nvarchar](500) NULL,
	[EntDate] [datetime] NULL,
	[EntOperation] [nvarchar](200) NULL,
	[EntTerminal] [nvarchar](400) NULL,
	[EntTerminalIP] [nvarchar](400) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


 
/****** Object:  Table [dbo].[tblAutoRefTestResident]    Script Date: 10/9/2023 3:35:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblAutoRefTestResident]') AND type in (N'U'))
DROP TABLE [dbo].[tblAutoRefTestResident]
GO

/****** Object:  Table [dbo].[tblAutoRefTestResident]    Script Date: 10/9/2023 3:35:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblAutoRefTestResident](
	[AutoRefResidentId] [int] IDENTITY(1,1) NOT NULL,
	[AutoRefResidentTransId] [varchar](15) NULL,
	[AutoRefResidentTransDate] [datetime] NULL,
	[ResidentAutoId] [int] NULL,
	[Right_Spherical_Status] [char](1) NULL,
	[Right_Spherical_Points] [decimal](9, 2) NULL,
	[Right_Cyclinderical_Status] [char](1) NULL,
	[Right_Cyclinderical_Points] [decimal](9, 2) NULL,
	[Right_Axix_From] [int] NULL,
	[Right_Axix_To] [int] NULL,
	[Left_Spherical_Status] [char](1) NULL,
	[Left_Spherical_Points] [decimal](9, 2) NULL,
	[Left_Cyclinderical_Status] [char](1) NULL,
	[Left_Cyclinderical_Points] [decimal](9, 2) NULL,
	[Left_Axix_From] [int] NULL,
	[Left_Axix_To] [int] NULL,
	[UserId] [nvarchar](250) NULL,
	[EntDate] [datetime] NULL,
	[EntOperation] [nvarchar](100) NULL,
	[EntTerminal] [nvarchar](200) NULL,
	[EntTerminalIP] [nvarchar](200) NULL,
	[ApplicationID] [nvarchar](20) NULL,
	[FormId] [nvarchar](250) NULL,
	[UserEmpId] [int] NULL,
	[UserEmpName] [nvarchar](250) NULL,
	[UserEmpCode] [nvarchar](10) NULL,
	[IPD] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[AutoRefResidentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UNIQUE_tblAutoRefTestResident] UNIQUE NONCLUSTERED 
(
	[AutoRefResidentId] ASC,
	[AutoRefResidentTransDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



/****** Object:  Table [dbo].[tblOptometristGothResident]    Script Date: 10/9/2023 3:35:44 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblOptometristGothResident]') AND type in (N'U'))
DROP TABLE [dbo].[tblOptometristGothResident]
GO

/****** Object:  Table [dbo].[tblOptometristGothResident]    Script Date: 10/9/2023 3:35:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblOptometristGothResident](
	[OptometristGothResidentId] [int] IDENTITY(1,1) NOT NULL,
	[OptometristGothResidentTransDate] [datetime] NULL,
	[ResidentAutoId] [int] NULL,
	[GothAutoId] [int] NULL,
	[HasChiefComplain] [int] NULL,
	[ChiefComplainRemarks] [nvarchar](200) NULL,
	[HasOccularHistory] [int] NULL,
	[OccularHistoryRemarks] [nvarchar](200) NULL,
	[HasMedicalHistory] [int] NULL,
	[MedicalHistoryRemarks] [nvarchar](200) NULL,
	[DistanceVision_RightEye_Unaided] [int] NULL,
	[DistanceVision_RightEye_WithGlasses] [int] NULL,
	[DistanceVision_RightEye_PinHole] [int] NULL,
	[NearVision_RightEye] [int] NULL,
	[NeedCycloRefraction_RightEye] [int] NULL,
	[NeedCycloRefractionRemarks_RightEye] [nvarchar](200) NULL,
	[DistanceVision_LeftEye_Unaided] [int] NULL,
	[DistanceVision_LeftEye_WithGlasses] [int] NULL,
	[DistanceVision_LeftEye_PinHole] [int] NULL,
	[NearVision_LeftEye] [int] NULL,
	[NeedCycloRefraction_LeftEye] [int] NULL,
	[NeedCycloRefractionRemarks_LeftEye] [nvarchar](200) NULL,
	[Right_Spherical_Status] [char](1) NULL,
	[Right_Spherical_Points] [decimal](9, 2) NULL,
	[Right_Cyclinderical_Status] [char](1) NULL,
	[Right_Cyclinderical_Points] [decimal](9, 2) NULL,
	[Right_Axix_From] [int] NULL,
	[Right_Axix_To] [int] NULL,
	[Right_Near_Status] [char](1) NULL,
	[Right_Near_Points] [decimal](9, 2) NULL,
	[Left_Spherical_Status] [char](1) NULL,
	[Left_Spherical_Points] [decimal](9, 2) NULL,
	[Left_Cyclinderical_Status] [char](1) NULL,
	[Left_Cyclinderical_Points] [decimal](9, 2) NULL,
	[Left_Axix_From] [int] NULL,
	[Left_Axix_To] [int] NULL,
	[Left_Near_Status] [char](1) NULL,
	[Left_Near_Points] [decimal](9, 2) NULL,
	[VisualAcuity_RightEye] [int] NULL,
	[VisualAcuity_LeftEye] [int] NULL,
	[LeftSquint_VA] [bit] NULL,
	[RightSquint_VA] [bit] NULL,
	[LeftAmblyopic_VA] [bit] NULL,
	[RightAmblyopic_VA] [bit] NULL,
	[AutoRefResidentId] [int] NULL,
	[Hirchberg_Distance] [int] NULL,
	[Hirchberg_Near] [int] NULL,
	[Ophthalmoscope_RightEye] [int] NULL,
	[Ophthalmoscope_LeftEye] [int] NULL,
	[PupillaryReactions_RightEye] [int] NULL,
	[CoverUncovertTest_RightEye] [int] NULL,
	[CoverUncovertTestRemarks_RightEye] [nvarchar](200) NULL,
	[ExtraOccularMuscleRemarks_RightEye] [nvarchar](200) NULL,
	[PupillaryReactions_LeftEye] [int] NULL,
	[CoverUncovertTest_LeftEye] [int] NULL,
	[CoverUncovertTestRemarks_LeftEye] [nvarchar](200) NULL,
	[CycloplegicRefraction_RightEye] [bit] NULL,
	[CycloplegicRefraction_LeftEye] [bit] NULL,
	[Conjunctivitis_RightEye] [bit] NULL,
	[Conjunctivitis_LeftEye] [bit] NULL,
	[Scleritis_RightEye] [bit] NULL,
	[Scleritis_LeftEye] [bit] NULL,
	[Nystagmus_RightEye] [bit] NULL,
	[Nystagmus_LeftEye] [bit] NULL,
	[CornealDefect_RightEye] [bit] NULL,
	[CornealDefect_LeftEye] [bit] NULL,
	[Cataract_RightEye] [bit] NULL,
	[Cataract_LeftEye] [bit] NULL,
	[Keratoconus_RightEye] [bit] NULL,
	[Keratoconus_LeftEye] [bit] NULL,
	[Ptosis_RightEye] [bit] NULL,
	[Ptosis_LeftEye] [bit] NULL,
	[LowVision_RightEye] [bit] NULL,
	[LowVision_LeftEye] [bit] NULL,
	[Pterygium_RightEye] [bit] NULL,
	[Pterygium_LeftEye] [bit] NULL,
	[ColorBlindness_RightEye] [bit] NULL,
	[ColorBlindness_LeftEye] [bit] NULL,
	[Others_RightEye] [bit] NULL,
	[Others_LeftEye] [bit] NULL,
	[Fundoscopy_RightEye] [bit] NULL,
	[Fundoscopy_LeftEye] [bit] NULL,
	[Surgery_RightEye] [bit] NULL,
	[Surgery_LeftEye] [bit] NULL,
	[CataractSurgery_RightEye] [bit] NULL,
	[CataractSurgery_LeftEye] [bit] NULL,
	[SurgeryPterygium_RightEye] [bit] NULL,
	[SurgeryPterygium_LeftEye] [bit] NULL,
	[SurgeryCornealDefect_RightEye] [bit] NULL,
	[SurgeryCornealDefect_LeftEye] [bit] NULL,
	[SurgeryPtosis_RightEye] [bit] NULL,
	[SurgeryPtosis_LeftEye] [bit] NULL,
	[SurgeryKeratoconus_RightEye] [bit] NULL,
	[SurgeryKeratoconus_LeftEye] [bit] NULL,
	[Chalazion_RightEye] [bit] NULL,
	[Chalazion_LeftEye] [bit] NULL,
	[Hordeolum_RightEye] [bit] NULL,
	[Hordeolum_LeftEye] [bit] NULL,
	[SurgeryOthers_RightEye] [bit] NULL,
	[SurgeryOthers_LeftEye] [bit] NULL,
	[Douchrome] [int] NULL,
	[Achromatopsia] [varchar](20) NULL,
	[RetinoScopy_RightEye] [int] NULL,
	[Condition_RightEye] [int] NULL,
	[Meridian1_RightEye] [varchar](200) NULL,
	[Meridian2_RightEye] [varchar](200) NULL,
	[FinalPrescription_RightEye] [varchar](200) NULL,
	[RetinoScopy_LeftEye] [int] NULL,
	[Condition_LeftEye] [varchar](200) NULL,
	[Meridian1_LeftEye] [varchar](200) NULL,
	[Meridian2_LeftEye] [varchar](200) NULL,
	[FinalPrescription_LeftEye] [varchar](200) NULL,
	[UserId] [nvarchar](250) NULL,
	[EntDate] [datetime] NULL,
	[EntOperation] [nvarchar](100) NULL,
	[EntTerminal] [nvarchar](200) NULL,
	[EntTerminalIP] [nvarchar](200) NULL,
	[ExtraOccularMuscleRemarks_LeftEye] [nvarchar](200) NULL,
	[ApplicationID] [nvarchar](20) NULL,
	[FormId] [nvarchar](250) NULL,
	[UserEmpId] [int] NULL,
	[UserEmpName] [nvarchar](250) NULL,
	[UserEmpCode] [nvarchar](10) NULL,
	[RightPupilDefects] [bit] NULL,
	[LeftPupilDefects] [bit] NULL,
	[RightAmblyopia] [bit] NULL,
	[LeftAmblyopia] [bit] NULL,
	[RightSquint_Surgery] [bit] NULL,
	[LeftSquint_Surgery] [bit] NULL,
	[IPD] [int] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [CycloplegicRefraction_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [CycloplegicRefraction_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [Conjunctivitis_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [Conjunctivitis_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [Scleritis_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [Scleritis_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [Nystagmus_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [Nystagmus_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [CornealDefect_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [CornealDefect_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [Cataract_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [Cataract_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [Keratoconus_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [Keratoconus_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [Ptosis_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [Ptosis_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [LowVision_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [LowVision_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [Pterygium_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [Pterygium_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [ColorBlindness_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [ColorBlindness_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [Others_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [Others_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [Fundoscopy_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [Fundoscopy_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [Surgery_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [Surgery_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [CataractSurgery_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [CataractSurgery_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [SurgeryPterygium_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [SurgeryPterygium_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [SurgeryCornealDefect_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [SurgeryCornealDefect_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [SurgeryPtosis_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [SurgeryPtosis_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [SurgeryKeratoconus_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [SurgeryKeratoconus_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [Chalazion_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [Chalazion_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [Hordeolum_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [Hordeolum_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [SurgeryOthers_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [SurgeryOthers_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [RightPupilDefects]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [LeftPupilDefects]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [RightAmblyopia]
GO

ALTER TABLE [dbo].[tblOptometristGothResident] ADD  DEFAULT ((0)) FOR [LeftAmblyopia]
GO


 
/****** Object:  Table [dbo].[tblGlassDispenseResident]    Script Date: 10/9/2023 3:36:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblGlassDispenseResident]') AND type in (N'U'))
DROP TABLE [dbo].[tblGlassDispenseResident]
GO

/****** Object:  Table [dbo].[tblGlassDispenseResident]    Script Date: 10/9/2023 3:36:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblGlassDispenseResident](
	[GlassDispenseResidentId] [int] IDENTITY(1,1) NOT NULL,
	[OptometristResidentId] [int] NOT NULL,
	[GlassDispenseResidentTransDate] [datetime] NULL,
	[ResidentAutoId] [int] NULL,
	[VisionwithGlasses_RightEye] [int] NULL,
	[VisionwithGlasses_LeftEye] [int] NULL,
	[NearVA_RightEye] [int] NULL,
	[NearVA_LeftEye] [int] NULL,
	[ResidentSatisficaion] [int] NULL,
	[Unsatisfied] [int] NULL,
	[Unsatisfied_Remarks] [nvarchar](250) NULL,
	[Unsatisfied_Reason] [int] NULL,
	[UserId] [nvarchar](250) NULL,
	[EntDate] [datetime] NULL,
	[EntOperation] [nvarchar](100) NULL,
	[EntTerminal] [nvarchar](200) NULL,
	[EntTerminalIP] [nvarchar](200) NULL,
 CONSTRAINT [PK_tblGlassDispenseResident] PRIMARY KEY CLUSTERED 
(
	[GlassDispenseResidentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


 
/****** Object:  Table [dbo].[tblLocalities]    Script Date: 10/9/2023 3:37:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblLocalities]') AND type in (N'U'))
DROP TABLE [dbo].[tblLocalities]
GO

/****** Object:  Table [dbo].[tblLocalities]    Script Date: 10/9/2023 3:37:11 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblLocalities](
	[LocalityAutoId] [int] IDENTITY(1,1) NOT NULL,
	[LocalityCode] [nvarchar](10) NOT NULL,
	[LocalityName] [nvarchar](250) NOT NULL,
	[Website] [nvarchar](250) NULL,
	[Address1] [nvarchar](1000) NULL,
	[Address2] [nvarchar](1000) NULL,
	[Address3] [nvarchar](1000) NULL,
	[Town] [varchar](100) NULL,
	[District] [nvarchar](1000) NULL,
	[City] [nvarchar](100) NULL,
	[NameofPerson] [varchar](100) NULL,
	[PersonMobile] [varchar](50) NULL,
	[PersonRole] [varchar](100) NULL,
	[TitleAutoId] [int] NULL,
	[UserId] [nvarchar](250) NULL,
	[EntDate] [datetime] NULL,
	[EntOperation] [nvarchar](100) NULL,
	[EntTerminal] [nvarchar](200) NULL,
	[EntTerminalIP] [nvarchar](200) NULL,
	[FormId] [nvarchar](250) NULL,
	[UserEmpId] [int] NULL,
	[UserEmpName] [nvarchar](250) NULL,
	[UserEmpCode] [nvarchar](10) NULL,
	[EnrollmentDate] [datetime] NULL,
 CONSTRAINT [PK_tblLocality] PRIMARY KEY CLUSTERED 
(
	[LocalityAutoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_LocalityCode] UNIQUE NONCLUSTERED 
(
	[LocalityCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


 

/****** Object:  Table [dbo].[tblLocalityImage]    Script Date: 10/9/2023 3:37:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblLocalityImage]') AND type in (N'U'))
DROP TABLE [dbo].[tblLocalityImage]
GO

/****** Object:  Table [dbo].[tblLocalityImage]    Script Date: 10/9/2023 3:37:30 PM ******/

CREATE TABLE [dbo].[tblLocalityImage](
	[LocalityImageAutoId] [int] IDENTITY(1,1) NOT NULL,
	[LocalityAutoId] [int] NOT NULL,
	[LocalityPic] [varchar](max) NULL,
	[FileType] [nvarchar](10) NULL,
	[FileSize] [int] NULL,
	[CaptureDate] [datetime] NULL,
	[CaptureRemarks] [nvarchar](200) NULL,
	[UserId] [nvarchar](500) NULL,
	[EntDate] [datetime] NULL,
	[EntOperation] [nvarchar](200) NULL,
	[EntTerminal] [nvarchar](400) NULL,
	[EntTerminalIP] [nvarchar](400) NULL,
PRIMARY KEY CLUSTERED 
(
	[LocalityImageAutoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

 
/****** Object:  Table [dbo].[tblLocalityResident]    Script Date: 10/9/2023 3:41:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblLocalityResident]') AND type in (N'U'))
DROP TABLE [dbo].[tblLocalityResident]
GO

/****** Object:  Table [dbo].[tblLocalityResident]    Script Date: 10/9/2023 3:41:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblLocalityResident](
	[ResidentAutoId] [int] IDENTITY(1,1) NOT NULL,
	[LocalityAutoId] [int] NULL,
	[ResidentCode] [nvarchar](15) NOT NULL,
	[ResidentName] [nvarchar](500) NOT NULL,
	[RelationType] [nvarchar](100) NULL,
	[RelationName] [nvarchar](500) NULL,
	[Age] [int] NULL,
	[GenderAutoId] [int] NULL,
	[CNIC] [varchar](30) NULL,
	[UserId] [nvarchar](250) NULL,
	[EntDate] [datetime] NULL,
	[EntOperation] [nvarchar](100) NULL,
	[EntTerminal] [nvarchar](200) NULL,
	[EntTerminalIP] [nvarchar](200) NULL,
	[WearGlasses] [bit] NULL,
	[Distance] [bit] NULL,
	[Near] [bit] NULL,
	[DecreasedVision] [bit] NULL,
	[HasOccularHistory] [bit] NULL,
	[OccularHistoryRemarks] [nvarchar](500) NULL,
	[HasMedicalHistory] [bit] NULL,
	[MedicalHistoryRemarks] [nvarchar](500) NULL,
	[HasChiefComplain] [bit] NULL,
	[ChiefComplainRemarks] [nvarchar](500) NULL,
	[ResidentTestDate] [datetime] NULL,
	[ResidentRegNo] [varchar](20) NULL,
	[SectionAutoId] [int] NULL,
	[ApplicationID] [nvarchar](20) NULL,
	[FormId] [nvarchar](250) NULL,
	[UserEmpId] [int] NULL,
	[UserEmpName] [nvarchar](250) NULL,
	[UserEmpCode] [nvarchar](10) NULL,
	[Religion] [bit] NULL,
	[MobileNo] [varchar](30) NULL,
	[LocalityCode] [varchar](20) NULL,
 CONSTRAINT [PK_tblResident] PRIMARY KEY CLUSTERED 
(
	[ResidentAutoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_ResidentCode] UNIQUE NONCLUSTERED 
(
	[ResidentCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



/****** Object:  Table [dbo].[tblLocalityResidentImage]    Script Date: 10/9/2023 3:41:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblLocalityResidentImage]') AND type in (N'U'))
DROP TABLE [dbo].[tblLocalityResidentImage]
GO

/****** Object:  Table [dbo].[tblLocalityResidentImage]    Script Date: 10/9/2023 3:41:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblLocalityResidentImage](
	[ResidentImageAutoId] [int] IDENTITY(1,1) NOT NULL,
	[ResidentAutoId] [int] NOT NULL,
	[LocalityAutoId] [int] NOT NULL,
	[ResidentPic] [varchar](max) NULL,
	[FileType] [nvarchar](10) NULL,
	[FileSize] [int] NULL,
	[CaptureDate] [datetime] NULL,
	[CaptureRemarks] [nvarchar](200) NULL,
	[UserId] [nvarchar](500) NULL,
	[EntDate] [datetime] NULL,
	[EntOperation] [nvarchar](200) NULL,
	[EntTerminal] [nvarchar](400) NULL,
	[EntTerminalIP] [nvarchar](400) NULL,
 CONSTRAINT [PK_tblLocalityResidentImage] PRIMARY KEY CLUSTERED 
(
	[ResidentImageAutoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


 
/****** Object:  Table [dbo].[tblAutoRefTestResident]    Script Date: 10/9/2023 3:43:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblAutoRefTestResident]') AND type in (N'U'))
DROP TABLE [dbo].[tblAutoRefTestResident]
GO

/****** Object:  Table [dbo].[tblAutoRefTestResident]    Script Date: 10/9/2023 3:43:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblAutoRefTestResident](
	[AutoRefResidentId] [int] IDENTITY(1,1) NOT NULL,
	[AutoRefResidentTransId] [varchar](15) NULL,
	[AutoRefResidentTransDate] [datetime] NULL,
	[ResidentAutoId] [int] NULL,
	[Right_Spherical_Status] [char](1) NULL,
	[Right_Spherical_Points] [decimal](9, 2) NULL,
	[Right_Cyclinderical_Status] [char](1) NULL,
	[Right_Cyclinderical_Points] [decimal](9, 2) NULL,
	[Right_Axix_From] [int] NULL,
	[Right_Axix_To] [int] NULL,
	[Left_Spherical_Status] [char](1) NULL,
	[Left_Spherical_Points] [decimal](9, 2) NULL,
	[Left_Cyclinderical_Status] [char](1) NULL,
	[Left_Cyclinderical_Points] [decimal](9, 2) NULL,
	[Left_Axix_From] [int] NULL,
	[Left_Axix_To] [int] NULL,
	[UserId] [nvarchar](250) NULL,
	[EntDate] [datetime] NULL,
	[EntOperation] [nvarchar](100) NULL,
	[EntTerminal] [nvarchar](200) NULL,
	[EntTerminalIP] [nvarchar](200) NULL,
	[ApplicationID] [nvarchar](20) NULL,
	[FormId] [nvarchar](250) NULL,
	[UserEmpId] [int] NULL,
	[UserEmpName] [nvarchar](250) NULL,
	[UserEmpCode] [nvarchar](10) NULL,
	[IPD] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[AutoRefResidentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UNIQUE_tblAutoRefTestResident] UNIQUE NONCLUSTERED 
(
	[AutoRefResidentId] ASC,
	[AutoRefResidentTransDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO





/****** Object:  Table [dbo].[tblOptometristResident]    Script Date: 10/9/2023 3:45:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblOptometristResident]') AND type in (N'U'))
DROP TABLE [dbo].[tblOptometristResident]
GO

/****** Object:  Table [dbo].[tblOptometristResident]    Script Date: 10/9/2023 3:45:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblOptometristResident](
	[OptometristResidentId] [int] IDENTITY(1,1) NOT NULL,
	[OptometristResidentTransDate] [datetime] NULL,
	[ResidentAutoId] [int] NULL,
	[LocalityAutoId] [int] NULL,
	[HasChiefComplain] [int] NULL,
	[ChiefComplainRemarks] [nvarchar](200) NULL,
	[HasOccularHistory] [int] NULL,
	[OccularHistoryRemarks] [nvarchar](200) NULL,
	[HasMedicalHistory] [int] NULL,
	[MedicalHistoryRemarks] [nvarchar](200) NULL,
	[DistanceVision_RightEye_Unaided] [int] NULL,
	[DistanceVision_RightEye_WithGlasses] [int] NULL,
	[DistanceVision_RightEye_PinHole] [int] NULL,
	[NearVision_RightEye] [int] NULL,
	[NeedCycloRefraction_RightEye] [int] NULL,
	[NeedCycloRefractionRemarks_RightEye] [nvarchar](200) NULL,
	[DistanceVision_LeftEye_Unaided] [int] NULL,
	[DistanceVision_LeftEye_WithGlasses] [int] NULL,
	[DistanceVision_LeftEye_PinHole] [int] NULL,
	[NearVision_LeftEye] [int] NULL,
	[NeedCycloRefraction_LeftEye] [int] NULL,
	[NeedCycloRefractionRemarks_LeftEye] [nvarchar](200) NULL,
	[Right_Spherical_Status] [char](1) NULL,
	[Right_Spherical_Points] [decimal](9, 2) NULL,
	[Right_Cyclinderical_Status] [char](1) NULL,
	[Right_Cyclinderical_Points] [decimal](9, 2) NULL,
	[Right_Axix_From] [int] NULL,
	[Right_Axix_To] [int] NULL,
	[Right_Near_Status] [char](1) NULL,
	[Right_Near_Points] [decimal](9, 2) NULL,
	[Left_Spherical_Status] [char](1) NULL,
	[Left_Spherical_Points] [decimal](9, 2) NULL,
	[Left_Cyclinderical_Status] [char](1) NULL,
	[Left_Cyclinderical_Points] [decimal](9, 2) NULL,
	[Left_Axix_From] [int] NULL,
	[Left_Axix_To] [int] NULL,
	[Left_Near_Status] [char](1) NULL,
	[Left_Near_Points] [decimal](9, 2) NULL,
	[VisualAcuity_RightEye] [int] NULL,
	[VisualAcuity_LeftEye] [int] NULL,
	[LeftSquint_VA] [bit] NULL,
	[RightSquint_VA] [bit] NULL,
	[LeftAmblyopic_VA] [bit] NULL,
	[RightAmblyopic_VA] [bit] NULL,
	[AutoRefResidentId] [int] NULL,
	[Hirchberg_Distance] [int] NULL,
	[Hirchberg_Near] [int] NULL,
	[Ophthalmoscope_RightEye] [int] NULL,
	[Ophthalmoscope_LeftEye] [int] NULL,
	[PupillaryReactions_RightEye] [int] NULL,
	[CoverUncovertTest_RightEye] [int] NULL,
	[CoverUncovertTestRemarks_RightEye] [nvarchar](200) NULL,
	[ExtraOccularMuscleRemarks_RightEye] [nvarchar](200) NULL,
	[PupillaryReactions_LeftEye] [int] NULL,
	[CoverUncovertTest_LeftEye] [int] NULL,
	[CoverUncovertTestRemarks_LeftEye] [nvarchar](200) NULL,
	[CycloplegicRefraction_RightEye] [bit] NULL,
	[CycloplegicRefraction_LeftEye] [bit] NULL,
	[Conjunctivitis_RightEye] [bit] NULL,
	[Conjunctivitis_LeftEye] [bit] NULL,
	[Scleritis_RightEye] [bit] NULL,
	[Scleritis_LeftEye] [bit] NULL,
	[Nystagmus_RightEye] [bit] NULL,
	[Nystagmus_LeftEye] [bit] NULL,
	[CornealDefect_RightEye] [bit] NULL,
	[CornealDefect_LeftEye] [bit] NULL,
	[Cataract_RightEye] [bit] NULL,
	[Cataract_LeftEye] [bit] NULL,
	[Keratoconus_RightEye] [bit] NULL,
	[Keratoconus_LeftEye] [bit] NULL,
	[Ptosis_RightEye] [bit] NULL,
	[Ptosis_LeftEye] [bit] NULL,
	[LowVision_RightEye] [bit] NULL,
	[LowVision_LeftEye] [bit] NULL,
	[Pterygium_RightEye] [bit] NULL,
	[Pterygium_LeftEye] [bit] NULL,
	[ColorBlindness_RightEye] [bit] NULL,
	[ColorBlindness_LeftEye] [bit] NULL,
	[Others_RightEye] [bit] NULL,
	[Others_LeftEye] [bit] NULL,
	[Fundoscopy_RightEye] [bit] NULL,
	[Fundoscopy_LeftEye] [bit] NULL,
	[Surgery_RightEye] [bit] NULL,
	[Surgery_LeftEye] [bit] NULL,
	[CataractSurgery_RightEye] [bit] NULL,
	[CataractSurgery_LeftEye] [bit] NULL,
	[SurgeryPterygium_RightEye] [bit] NULL,
	[SurgeryPterygium_LeftEye] [bit] NULL,
	[SurgeryCornealDefect_RightEye] [bit] NULL,
	[SurgeryCornealDefect_LeftEye] [bit] NULL,
	[SurgeryPtosis_RightEye] [bit] NULL,
	[SurgeryPtosis_LeftEye] [bit] NULL,
	[SurgeryKeratoconus_RightEye] [bit] NULL,
	[SurgeryKeratoconus_LeftEye] [bit] NULL,
	[Chalazion_RightEye] [bit] NULL,
	[Chalazion_LeftEye] [bit] NULL,
	[Hordeolum_RightEye] [bit] NULL,
	[Hordeolum_LeftEye] [bit] NULL,
	[SurgeryOthers_RightEye] [bit] NULL,
	[SurgeryOthers_LeftEye] [bit] NULL,
	[Douchrome] [int] NULL,
	[Achromatopsia] [varchar](20) NULL,
	[RetinoScopy_RightEye] [int] NULL,
	[Condition_RightEye] [int] NULL,
	[Meridian1_RightEye] [varchar](200) NULL,
	[Meridian2_RightEye] [varchar](200) NULL,
	[FinalPrescription_RightEye] [varchar](200) NULL,
	[RetinoScopy_LeftEye] [int] NULL,
	[Condition_LeftEye] [varchar](200) NULL,
	[Meridian1_LeftEye] [varchar](200) NULL,
	[Meridian2_LeftEye] [varchar](200) NULL,
	[FinalPrescription_LeftEye] [varchar](200) NULL,
	[UserId] [nvarchar](250) NULL,
	[EntDate] [datetime] NULL,
	[EntOperation] [nvarchar](100) NULL,
	[EntTerminal] [nvarchar](200) NULL,
	[EntTerminalIP] [nvarchar](200) NULL,
	[ExtraOccularMuscleRemarks_LeftEye] [nvarchar](200) NULL,
	[ApplicationID] [nvarchar](20) NULL,
	[FormId] [nvarchar](250) NULL,
	[UserEmpId] [int] NULL,
	[UserEmpName] [nvarchar](250) NULL,
	[UserEmpCode] [nvarchar](10) NULL,
	[RightPupilDefects] [bit] NULL,
	[LeftPupilDefects] [bit] NULL,
	[RightAmblyopia] [bit] NULL,
	[LeftAmblyopia] [bit] NULL,
	[RightSquint_Surgery] [bit] NULL,
	[LeftSquint_Surgery] [bit] NULL,
	[IPD] [int] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [CycloplegicRefraction_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [CycloplegicRefraction_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [Conjunctivitis_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [Conjunctivitis_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [Scleritis_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [Scleritis_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [Nystagmus_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [Nystagmus_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [CornealDefect_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [CornealDefect_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [Cataract_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [Cataract_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [Keratoconus_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [Keratoconus_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [Ptosis_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [Ptosis_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [LowVision_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [LowVision_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [Pterygium_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [Pterygium_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [ColorBlindness_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [ColorBlindness_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [Others_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [Others_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [Fundoscopy_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [Fundoscopy_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [Surgery_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [Surgery_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [CataractSurgery_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [CataractSurgery_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [SurgeryPterygium_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [SurgeryPterygium_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [SurgeryCornealDefect_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [SurgeryCornealDefect_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [SurgeryPtosis_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [SurgeryPtosis_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [SurgeryKeratoconus_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [SurgeryKeratoconus_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [Chalazion_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [Chalazion_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [Hordeolum_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [Hordeolum_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [SurgeryOthers_RightEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [SurgeryOthers_LeftEye]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [RightPupilDefects]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [LeftPupilDefects]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [RightAmblyopia]
GO

ALTER TABLE [dbo].[tblOptometristResident] ADD  DEFAULT ((0)) FOR [LeftAmblyopia]
GO




 
/****** Object:  Table [dbo].[tblGlassDispenseResident]    Script Date: 10/9/2023 3:56:23 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblGlassDispenseResident]') AND type in (N'U'))
DROP TABLE [dbo].[tblGlassDispenseResident]
GO

/****** Object:  Table [dbo].[tblGlassDispenseResident]    Script Date: 10/9/2023 3:56:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblGlassDispenseResident](
	[GlassDispenseResidentId] [int] IDENTITY(1,1) NOT NULL,
	[OptometristResidentId] [int] NOT NULL,
	[GlassDispenseResidentTransDate] [datetime] NULL,
	[ResidentAutoId] [int] NULL,
	[VisionwithGlasses_RightEye] [int] NULL,
	[VisionwithGlasses_LeftEye] [int] NULL,
	[NearVA_RightEye] [int] NULL,
	[NearVA_LeftEye] [int] NULL,
	[ResidentSatisficaion] [int] NULL,
	[Unsatisfied] [int] NULL,
	[Unsatisfied_Remarks] [nvarchar](250) NULL,
	[Unsatisfied_Reason] [int] NULL,
	[UserId] [nvarchar](250) NULL,
	[EntDate] [datetime] NULL,
	[EntOperation] [nvarchar](100) NULL,
	[EntTerminal] [nvarchar](200) NULL,
	[EntTerminalIP] [nvarchar](200) NULL,
 CONSTRAINT [PK_tblGlassDispenseResident] PRIMARY KEY CLUSTERED 
(
	[GlassDispenseResidentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


 
/****** Object:  Table [dbo].[tblPublicSpaces]    Script Date: 10/9/2023 3:58:15 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPublicSpaces]') AND type in (N'U'))
DROP TABLE [dbo].[tblPublicSpaces]
GO

/****** Object:  Table [dbo].[tblPublicSpaces]    Script Date: 10/9/2023 3:58:15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblPublicSpaces](
	[PublicSpacesAutoId] [int] IDENTITY(1,1) NOT NULL,
	[PublicSpacesCode] [nvarchar](10) NOT NULL,
	[PublicSpacesName] [nvarchar](250) NOT NULL,
	[Website] [nvarchar](250) NULL,
	[Address1] [nvarchar](1000) NULL,
	[Address2] [nvarchar](1000) NULL,
	[Address3] [nvarchar](1000) NULL,
	[Town] [varchar](100) NULL,
	[District] [nvarchar](1000) NULL,
	[City] [nvarchar](100) NULL,
	[NameofPerson] [varchar](100) NULL,
	[PersonMobile] [varchar](50) NULL,
	[PersonRole] [varchar](100) NULL,
	[TitleAutoId] [int] NULL,
	[UserId] [nvarchar](250) NULL,
	[EntDate] [datetime] NULL,
	[EntOperation] [nvarchar](100) NULL,
	[EntTerminal] [nvarchar](200) NULL,
	[EntTerminalIP] [nvarchar](200) NULL,
	[FormId] [nvarchar](250) NULL,
	[UserEmpId] [int] NULL,
	[UserEmpName] [nvarchar](250) NULL,
	[UserEmpCode] [nvarchar](10) NULL,
	[EnrollmentDate] [datetime] NULL
) ON [PRIMARY]
GO


 
/****** Object:  Table [dbo].[tblPublicSpacesImage]    Script Date: 10/9/2023 3:58:33 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPublicSpacesImage]') AND type in (N'U'))
DROP TABLE [dbo].[tblPublicSpacesImage]
GO

/****** Object:  Table [dbo].[tblPublicSpacesImage]    Script Date: 10/9/2023 3:58:33 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblPublicSpacesImage](
	[PublicSpacesImageAutoId] [int] IDENTITY(1,1) NOT NULL,
	[PublicSpacesAutoId] [int] NOT NULL,
	[PublicSpacesPic] [varchar](max) NULL,
	[FileType] [nvarchar](10) NULL,
	[FileSize] [int] NULL,
	[CaptureDate] [datetime] NULL,
	[CaptureRemarks] [nvarchar](200) NULL,
	[UserId] [nvarchar](500) NULL,
	[EntDate] [datetime] NULL,
	[EntOperation] [nvarchar](200) NULL,
	[EntTerminal] [nvarchar](400) NULL,
	[EntTerminalIP] [nvarchar](400) NULL,
PRIMARY KEY CLUSTERED 
(
	[PublicSpacesImageAutoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


 
/****** Object:  Table [dbo].[tblPublicSpacesResident]    Script Date: 10/9/2023 3:58:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPublicSpacesResident]') AND type in (N'U'))
DROP TABLE [dbo].[tblPublicSpacesResident]
GO

/****** Object:  Table [dbo].[tblPublicSpacesResident]    Script Date: 10/9/2023 3:58:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblPublicSpacesResident](
	[ResidentAutoId] [int] IDENTITY(1,1) NOT NULL,
	[PublicSpacesAutoId] [int] NULL,
	[ResidentCode] [nvarchar](15) NOT NULL,
	[ResidentName] [nvarchar](500) NOT NULL,
	[RelationType] [nvarchar](100) NULL,
	[RelationName] [nvarchar](500) NULL,
	[Age] [int] NULL,
	[GenderAutoId] [int] NULL,
	[CNIC] [varchar](30) NULL,
	[UserId] [nvarchar](250) NULL,
	[EntDate] [datetime] NULL,
	[EntOperation] [nvarchar](100) NULL,
	[EntTerminal] [nvarchar](200) NULL,
	[EntTerminalIP] [nvarchar](200) NULL,
	[WearGlasses] [bit] NULL,
	[Distance] [bit] NULL,
	[Near] [bit] NULL,
	[DecreasedVision] [bit] NULL,
	[HasOccularHistory] [bit] NULL,
	[OccularHistoryRemarks] [nvarchar](500) NULL,
	[HasMedicalHistory] [bit] NULL,
	[MedicalHistoryRemarks] [nvarchar](500) NULL,
	[HasChiefComplain] [bit] NULL,
	[ChiefComplainRemarks] [nvarchar](500) NULL,
	[ResidentTestDate] [datetime] NULL,
	[ResidentRegNo] [varchar](20) NULL,
	[SectionAutoId] [int] NULL,
	[ApplicationID] [nvarchar](20) NULL,
	[FormId] [nvarchar](250) NULL,
	[UserEmpId] [int] NULL,
	[UserEmpName] [nvarchar](250) NULL,
	[UserEmpCode] [nvarchar](10) NULL,
	[Religion] [bit] NULL,
	[MobileNo] [varchar](30) NULL,
	[PublicSpacesCode] [varchar](20) NULL
) ON [PRIMARY]
GO


 
/****** Object:  Table [dbo].[tblPublicSpacesResidentImage]    Script Date: 10/9/2023 3:59:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPublicSpacesResidentImage]') AND type in (N'U'))
DROP TABLE [dbo].[tblPublicSpacesResidentImage]
GO

/****** Object:  Table [dbo].[tblPublicSpacesResidentImage]    Script Date: 10/9/2023 3:59:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblPublicSpacesResidentImage](
	[ResidentImageAutoId] [int] IDENTITY(1,1) NOT NULL,
	[ResidentAutoId] [int] NOT NULL,
	[PublicSpacesAutoId] [int] NOT NULL,
	[ResidentPic] [varchar](max) NULL,
	[FileType] [nvarchar](10) NULL,
	[FileSize] [int] NULL,
	[CaptureDate] [datetime] NULL,
	[CaptureRemarks] [nvarchar](200) NULL,
	[UserId] [nvarchar](500) NULL,
	[EntDate] [datetime] NULL,
	[EntOperation] [nvarchar](200) NULL,
	[EntTerminal] [nvarchar](400) NULL,
	[EntTerminalIP] [nvarchar](400) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


 
/****** Object:  Table [dbo].[tblPublicSpacesAutoRefTestResident]    Script Date: 10/9/2023 3:59:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPublicSpacesAutoRefTestResident]') AND type in (N'U'))
DROP TABLE [dbo].[tblPublicSpacesAutoRefTestResident]
GO

/****** Object:  Table [dbo].[tblPublicSpacesAutoRefTestResident]    Script Date: 10/9/2023 3:59:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblPublicSpacesAutoRefTestResident](
	[AutoRefResidentId] [int] IDENTITY(1,1) NOT NULL,
	[AutoRefResidentTransId] [varchar](15) NULL,
	[AutoRefResidentTransDate] [datetime] NULL,
	[ResidentAutoId] [int] NULL,
	[Right_Spherical_Status] [char](1) NULL,
	[Right_Spherical_Points] [decimal](9, 2) NULL,
	[Right_Cyclinderical_Status] [char](1) NULL,
	[Right_Cyclinderical_Points] [decimal](9, 2) NULL,
	[Right_Axix_From] [int] NULL,
	[Right_Axix_To] [int] NULL,
	[Left_Spherical_Status] [char](1) NULL,
	[Left_Spherical_Points] [decimal](9, 2) NULL,
	[Left_Cyclinderical_Status] [char](1) NULL,
	[Left_Cyclinderical_Points] [decimal](9, 2) NULL,
	[Left_Axix_From] [int] NULL,
	[Left_Axix_To] [int] NULL,
	[UserId] [nvarchar](250) NULL,
	[EntDate] [datetime] NULL,
	[EntOperation] [nvarchar](100) NULL,
	[EntTerminal] [nvarchar](200) NULL,
	[EntTerminalIP] [nvarchar](200) NULL,
	[ApplicationID] [nvarchar](20) NULL,
	[FormId] [nvarchar](250) NULL,
	[UserEmpId] [int] NULL,
	[UserEmpName] [nvarchar](250) NULL,
	[UserEmpCode] [nvarchar](10) NULL,
	[IPD] [int] NULL
) ON [PRIMARY]
GO


  
/****** Object:  Table [dbo].[tblPublicSpacesOptometristResident]    Script Date: 10/9/2023 3:59:56 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPublicSpacesOptometristResident]') AND type in (N'U'))
DROP TABLE [dbo].[tblPublicSpacesOptometristResident]
GO

/****** Object:  Table [dbo].[tblPublicSpacesOptometristResident]    Script Date: 10/9/2023 3:59:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblPublicSpacesOptometristResident](
	[OptometristPublicSpacesResidentId] [int] IDENTITY(1,1) NOT NULL,
	[OptometristResidentTransDate] [datetime] NULL,
	[ResidentAutoId] [int] NULL,
	[PublicSpacesAutoId] [int] NULL,
	[HasChiefComplain] [int] NULL,
	[ChiefComplainRemarks] [nvarchar](200) NULL,
	[HasOccularHistory] [int] NULL,
	[OccularHistoryRemarks] [nvarchar](200) NULL,
	[HasMedicalHistory] [int] NULL,
	[MedicalHistoryRemarks] [nvarchar](200) NULL,
	[DistanceVision_RightEye_Unaided] [int] NULL,
	[DistanceVision_RightEye_WithGlasses] [int] NULL,
	[DistanceVision_RightEye_PinHole] [int] NULL,
	[NearVision_RightEye] [int] NULL,
	[NeedCycloRefraction_RightEye] [int] NULL,
	[NeedCycloRefractionRemarks_RightEye] [nvarchar](200) NULL,
	[DistanceVision_LeftEye_Unaided] [int] NULL,
	[DistanceVision_LeftEye_WithGlasses] [int] NULL,
	[DistanceVision_LeftEye_PinHole] [int] NULL,
	[NearVision_LeftEye] [int] NULL,
	[NeedCycloRefraction_LeftEye] [int] NULL,
	[NeedCycloRefractionRemarks_LeftEye] [nvarchar](200) NULL,
	[Right_Spherical_Status] [char](1) NULL,
	[Right_Spherical_Points] [decimal](9, 2) NULL,
	[Right_Cyclinderical_Status] [char](1) NULL,
	[Right_Cyclinderical_Points] [decimal](9, 2) NULL,
	[Right_Axix_From] [int] NULL,
	[Right_Axix_To] [int] NULL,
	[Right_Near_Status] [char](1) NULL,
	[Right_Near_Points] [decimal](9, 2) NULL,
	[Left_Spherical_Status] [char](1) NULL,
	[Left_Spherical_Points] [decimal](9, 2) NULL,
	[Left_Cyclinderical_Status] [char](1) NULL,
	[Left_Cyclinderical_Points] [decimal](9, 2) NULL,
	[Left_Axix_From] [int] NULL,
	[Left_Axix_To] [int] NULL,
	[Left_Near_Status] [char](1) NULL,
	[Left_Near_Points] [decimal](9, 2) NULL,
	[VisualAcuity_RightEye] [int] NULL,
	[VisualAcuity_LeftEye] [int] NULL,
	[LeftSquint_VA] [bit] NULL,
	[RightSquint_VA] [bit] NULL,
	[LeftAmblyopic_VA] [bit] NULL,
	[RightAmblyopic_VA] [bit] NULL,
	[AutoRefResidentId] [int] NULL,
	[Hirchberg_Distance] [int] NULL,
	[Hirchberg_Near] [int] NULL,
	[Ophthalmoscope_RightEye] [int] NULL,
	[Ophthalmoscope_LeftEye] [int] NULL,
	[PupillaryReactions_RightEye] [int] NULL,
	[CoverUncovertTest_RightEye] [int] NULL,
	[CoverUncovertTestRemarks_RightEye] [nvarchar](200) NULL,
	[ExtraOccularMuscleRemarks_RightEye] [nvarchar](200) NULL,
	[PupillaryReactions_LeftEye] [int] NULL,
	[CoverUncovertTest_LeftEye] [int] NULL,
	[CoverUncovertTestRemarks_LeftEye] [nvarchar](200) NULL,
	[CycloplegicRefraction_RightEye] [bit] NULL,
	[CycloplegicRefraction_LeftEye] [bit] NULL,
	[Conjunctivitis_RightEye] [bit] NULL,
	[Conjunctivitis_LeftEye] [bit] NULL,
	[Scleritis_RightEye] [bit] NULL,
	[Scleritis_LeftEye] [bit] NULL,
	[Nystagmus_RightEye] [bit] NULL,
	[Nystagmus_LeftEye] [bit] NULL,
	[CornealDefect_RightEye] [bit] NULL,
	[CornealDefect_LeftEye] [bit] NULL,
	[Cataract_RightEye] [bit] NULL,
	[Cataract_LeftEye] [bit] NULL,
	[Keratoconus_RightEye] [bit] NULL,
	[Keratoconus_LeftEye] [bit] NULL,
	[Ptosis_RightEye] [bit] NULL,
	[Ptosis_LeftEye] [bit] NULL,
	[LowVision_RightEye] [bit] NULL,
	[LowVision_LeftEye] [bit] NULL,
	[Pterygium_RightEye] [bit] NULL,
	[Pterygium_LeftEye] [bit] NULL,
	[ColorBlindness_RightEye] [bit] NULL,
	[ColorBlindness_LeftEye] [bit] NULL,
	[Others_RightEye] [bit] NULL,
	[Others_LeftEye] [bit] NULL,
	[Fundoscopy_RightEye] [bit] NULL,
	[Fundoscopy_LeftEye] [bit] NULL,
	[Surgery_RightEye] [bit] NULL,
	[Surgery_LeftEye] [bit] NULL,
	[CataractSurgery_RightEye] [bit] NULL,
	[CataractSurgery_LeftEye] [bit] NULL,
	[SurgeryPterygium_RightEye] [bit] NULL,
	[SurgeryPterygium_LeftEye] [bit] NULL,
	[SurgeryCornealDefect_RightEye] [bit] NULL,
	[SurgeryCornealDefect_LeftEye] [bit] NULL,
	[SurgeryPtosis_RightEye] [bit] NULL,
	[SurgeryPtosis_LeftEye] [bit] NULL,
	[SurgeryKeratoconus_RightEye] [bit] NULL,
	[SurgeryKeratoconus_LeftEye] [bit] NULL,
	[Chalazion_RightEye] [bit] NULL,
	[Chalazion_LeftEye] [bit] NULL,
	[Hordeolum_RightEye] [bit] NULL,
	[Hordeolum_LeftEye] [bit] NULL,
	[SurgeryOthers_RightEye] [bit] NULL,
	[SurgeryOthers_LeftEye] [bit] NULL,
	[Douchrome] [int] NULL,
	[Achromatopsia] [varchar](20) NULL,
	[RetinoScopy_RightEye] [int] NULL,
	[Condition_RightEye] [int] NULL,
	[Meridian1_RightEye] [varchar](200) NULL,
	[Meridian2_RightEye] [varchar](200) NULL,
	[FinalPrescription_RightEye] [varchar](200) NULL,
	[RetinoScopy_LeftEye] [int] NULL,
	[Condition_LeftEye] [varchar](200) NULL,
	[Meridian1_LeftEye] [varchar](200) NULL,
	[Meridian2_LeftEye] [varchar](200) NULL,
	[FinalPrescription_LeftEye] [varchar](200) NULL,
	[UserId] [nvarchar](250) NULL,
	[EntDate] [datetime] NULL,
	[EntOperation] [nvarchar](100) NULL,
	[EntTerminal] [nvarchar](200) NULL,
	[EntTerminalIP] [nvarchar](200) NULL,
	[ExtraOccularMuscleRemarks_LeftEye] [nvarchar](200) NULL,
	[ApplicationID] [nvarchar](20) NULL,
	[FormId] [nvarchar](250) NULL,
	[UserEmpId] [int] NULL,
	[UserEmpName] [nvarchar](250) NULL,
	[UserEmpCode] [nvarchar](10) NULL,
	[RightPupilDefects] [bit] NULL,
	[LeftPupilDefects] [bit] NULL,
	[RightAmblyopia] [bit] NULL,
	[LeftAmblyopia] [bit] NULL,
	[RightSquint_Surgery] [bit] NULL,
	[LeftSquint_Surgery] [bit] NULL,
	[IPD] [int] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [CycloplegicRefraction_RightEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [CycloplegicRefraction_LeftEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [Conjunctivitis_RightEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [Conjunctivitis_LeftEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [Scleritis_RightEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [Scleritis_LeftEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [Nystagmus_RightEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [Nystagmus_LeftEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [CornealDefect_RightEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [CornealDefect_LeftEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [Cataract_RightEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [Cataract_LeftEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [Keratoconus_RightEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [Keratoconus_LeftEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [Ptosis_RightEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [Ptosis_LeftEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [LowVision_RightEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [LowVision_LeftEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [Pterygium_RightEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [Pterygium_LeftEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [ColorBlindness_RightEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [ColorBlindness_LeftEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [Others_RightEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [Others_LeftEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [Fundoscopy_RightEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [Fundoscopy_LeftEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [Surgery_RightEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [Surgery_LeftEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [CataractSurgery_RightEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [CataractSurgery_LeftEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [SurgeryPterygium_RightEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [SurgeryPterygium_LeftEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [SurgeryCornealDefect_RightEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [SurgeryCornealDefect_LeftEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [SurgeryPtosis_RightEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [SurgeryPtosis_LeftEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [SurgeryKeratoconus_RightEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [SurgeryKeratoconus_LeftEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [Chalazion_RightEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [Chalazion_LeftEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [Hordeolum_RightEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [Hordeolum_LeftEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [SurgeryOthers_RightEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [SurgeryOthers_LeftEye]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [RightPupilDefects]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [LeftPupilDefects]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [RightAmblyopia]
GO

ALTER TABLE [dbo].[tblPublicSpacesOptometristResident] ADD  DEFAULT ((0)) FOR [LeftAmblyopia]
GO


 
/****** Object:  Table [dbo].[tblPublicSpacesGlassDispenseResident]    Script Date: 10/9/2023 4:00:23 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPublicSpacesGlassDispenseResident]') AND type in (N'U'))
DROP TABLE [dbo].[tblPublicSpacesGlassDispenseResident]
GO

/****** Object:  Table [dbo].[tblPublicSpacesGlassDispenseResident]    Script Date: 10/9/2023 4:00:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblPublicSpacesGlassDispenseResident](
	[PublicSpacesGlassDispenseResidentId] [int] IDENTITY(1,1) NOT NULL,
	[OptometristPublicSpacesResidentId] [int] NOT NULL,
	[GlassDispenseResidentTransDate] [datetime] NULL,
	[ResidentAutoId] [int] NULL,
	[VisionwithGlasses_RightEye] [int] NULL,
	[VisionwithGlasses_LeftEye] [int] NULL,
	[NearVA_RightEye] [int] NULL,
	[NearVA_LeftEye] [int] NULL,
	[ResidentSatisficaion] [int] NULL,
	[Unsatisfied] [int] NULL,
	[Unsatisfied_Remarks] [nvarchar](250) NULL,
	[Unsatisfied_Reason] [int] NULL,
	[UserId] [nvarchar](250) NULL,
	[EntDate] [datetime] NULL,
	[EntOperation] [nvarchar](100) NULL,
	[EntTerminal] [nvarchar](200) NULL,
	[EntTerminalIP] [nvarchar](200) NULL,
 CONSTRAINT [PK_tblPublicSpacesGlassDispenseResident] PRIMARY KEY CLUSTERED 
(
	[PublicSpacesGlassDispenseResidentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


 
/****** Object:  Table [dbo].[tbl_Codes]    Script Date: 10/9/2023 4:00:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_Codes]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_Codes]
GO

/****** Object:  Table [dbo].[tbl_Codes]    Script Date: 10/9/2023 4:00:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbl_Codes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CodeType] [varchar](100) NULL,
	[PreFix] [varchar](10) NULL,
	[CodeLength] [int] NULL,
	[LastUsedCode] [int] NULL,
	[CompanyId] [int] NULL,
	[LocalityId] [int] NULL,
	[GothId] [int] NULL,
	[PublicSpacesId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


