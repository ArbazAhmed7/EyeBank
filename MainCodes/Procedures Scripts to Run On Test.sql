 

CREATE OR ALTER PROC [dbo].[Sp_SetupCompany]     
    @CompanyAutoId INT       =NULL ,    
    @CompanyCode nvarchar(10) =NULL,    
    @CompanyName nvarchar(250) =NULL,    
 @Website nvarchar(250) =null,    
    @Address1 nvarchar(1000) =NULL,    
    @Address2 nvarchar(1000) =NULL,    
 @Address3 nvarchar(1000) = NULL,    
    @District nvarchar(1000) =NULL,    
 @Town nvarchar(200) =NULL,    
    @City nvarchar(100) =NULL,    
 @WorkForce INT = null,    
    @OwnerName varchar(100) =NULL,    
    @OwnerMobile varchar(50) =NULL,    
    @OwnerEmail varchar(100) =NULL,    
    @AdminHeadName varchar(100)=NULL,    
    @AdminHeadMobile varchar(50)=NULL,    
    @AdminHeadEmail varchar(100)=NULL,    
    @HRHeadName varchar(100)=NULL,    
    @HRHeadMobile varchar(50)=NULL,    
    @HRHeadEmail varchar(100)=NULL,    
    @TitleAutoId INT =NULL,    
    @UserId nvarchar(250) =NULL,    
    @EntDate DATETIME =NULL,    
    @EntOperation nvarchar(100) =NULL,    
    @FormId nvarchar(250) =NULL,    
    @UserEmpId INT =NULL,    
    @UserEmpName nvarchar(250) =NULL,    
    @UserEmpCode nvarchar(10) =NULL,    
    @EnrollmentDate DATETIME =NULL,    
 @operation VARCHAR(50)= NULL,    
 @EntryTerminal VARCHAR(200)=NULL,    
 @EntryTerminalIP VARCHAR(200)=NULL,    
 @SearchText VARCHAR(MAX)=NULL    
AS     
    IF @operation = 'Save'    
 Begin    
 IF NOT EXISTS(SELECT 1 FROM tblCompany c WHERE c.CompanyName=@CompanyName AND c.City=@City AND c.Address1=@Address1 )    
 BEGIN    
        
    INSERT INTO dbo.tblCompany (CompanyCode,CompanyName, Website, Address1, Address2,     
                                Address3, Town, District, City, WorkForce, OwnerName, OwnerMobile, OwnerEmail,     
                                AdminHeadName, AdminHeadMobile, AdminHeadEmail, HRHeadName, HRHeadMobile,     
                                HRHeadEmail, TitleAutoId, UserId, EntDate, EntOperation, EntTerminal,    
         EntTerminalIP, FormId, UserEmpId, UserEmpName, UserEmpCode, EnrollmentDate)    
            
    
    SELECT '',UPPER(@CompanyName),@Website, @Address1, @Address2,@Address3,@Town, @District, @City, @WorkForce,     
 @OwnerName,@OwnerMobile,@OwnerEmail,@AdminHeadName,@AdminHeadMobile,@AdminHeadEmail,@HRHeadName,@HRHeadMobile    
 ,@HRHeadEmail, @TitleAutoId, @UserId, GETDATE(), 'INSERT', @EntryTerminal, @EntryTerminalIP,     
           @FormId, @UserEmpId, @UserEmpName, @UserEmpCode, ISNULL(@EnrollmentDate,GETDATE())    
         
     SET @CompanyAutoId = SCOPE_IDENTITY()    
     SELECT @CompanyCode =MAX(CAST(cd.CompanyCode AS INT))+1 FROM tblCompany cd    
    
   UPDATE tblCompany SET CompanyCode=CASE WHEN LEN(@CompanyCode)=1 THEN '00'+@CompanyCode    
   WHEN LEN(@CompanyCode )=2 THEN '0'+@CompanyCode    
   ELSE @CompanyCode END    
   WHERE CompanyAutoId=@CompanyAutoId    
    
   SELECT @CompanyAutoId AS CompanyAutoId,'Successfully Saved' AS RESULT    
  END    
  ELSE    
  SELECT @CompanyAutoId AS CompanyAutoId,'Company with same detail Already Exists.' AS RESULT    
 END    
    
 ELSE IF @operation = 'Update'    
 BEGIN    
 UPDATE dbo.tblCompany    
    SET    CompanyName = @CompanyName,Website=@Website, Address1 = @Address1, Address2 = @Address2,     
            Address3 = @Address3, Town = @Town, District = @District, City = @City,     
           WorkForce = @WorkForce, OwnerName = @OwnerName, OwnerMobile = @OwnerMobile, OwnerEmail = @OwnerEmail,     
           AdminHeadName = @AdminHeadName, AdminHeadMobile = @AdminHeadMobile, AdminHeadEmail = @AdminHeadEmail,     
           HRHeadName = @HRHeadName, HRHeadMobile = @HRHeadMobile, HRHeadEmail = @HRHeadEmail, TitleAutoId = @TitleAutoId,     
           UserId = @UserId, EntDate = @EntDate, EntOperation = @EntOperation, EntTerminal = @EntryTerminal,     
           EntTerminalIP = @EntryTerminalIP, FormId = @FormId, UserEmpId = @UserEmpId, UserEmpName = @UserEmpName,     
           UserEmpCode = @UserEmpCode    
    WHERE  CompanyAutoId = @CompanyAutoId    
 SELECT @CompanyAutoId AS CompanyAutoId,'Successfully Updated' AS RESULT    
 END    
     
 ELSE IF @operation='GetCompanyByiD'    
 BEGIN    
 SELECT c.[CompanyAutoId] companyAutoId ,c.[CompanyCode],c.[CompanyName],c.[Website] ,c.[Address1],c.[Address2],c.[Address3] ,c.[Town],c.[District] ,c.[City],c.[WorkForce]      ,    
   c.[OwnerName]  ,c.[OwnerMobile], c.[OwnerEmail],c.[AdminHeadName]      ,c.[AdminHeadMobile]   ,c.[AdminHeadEmail]   ,c.[HRHeadName]  ,c.[HRHeadMobile]    
      ,c.[HRHeadEmail],c.[TitleAutoId] ,c.[UserId]      ,c.EnrollmentDate EnrollmentDate ,c.[EntOperation] ,c.[EntTerminal],c.[EntTerminalIP] ,c.[FormId]  ,c.[UserEmpId]    
      ,c.[UserEmpName],c.[UserEmpCode] ,CAST(ci.EntDate AS DATE) EntDate ,ci.CompanyAutoId DetailCompanyImageAutoId    
   ,ci.CompanyImageAutoId CompanyImageAutoId,ci.FileType,ci.FileSize,ci.CompanyPic,ISNULL(ci.CaptureRemarks,'')CaptureRemarks    
   FROM [dbo].[tblCompany] AS c   LEFT JOIN tblCompanyImage ci ON c.CompanyAutoId = ci.CompanyAutoId    
   WHERE     
   c.CompanyAutoId=@CompanyAutoId    
       
 END    
    
 ELSE IF @operation='GetAllCompany'    
 BEGIN    
 SELECT  DISTINCT CompanyAutoId, CompanyCode 'Factory Code', CompanyName 'Factory Name', ISNULL(ISNULL(Address1,c.Address2),c.Address3) Address    
     FROM tblCompany c    
     ORDER BY c.CompanyAutoId desc    
 END    
    
 ELSE IF @operation = 'Search'    
 BEGIN    
 SELECT CompanyAutoId, CompanyCode, CompanyName, ISNULL(ISNULL(Address1,a.Address2),a.Address3) Address    
     FROM tblCompany A    
   WHERE    
   (    
   CAST(A.CompanyCode AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR    
   CAST(A.CompanyName AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR    
   CAST(A.Address1 AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR    
   CAST(A.Address2 AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR    
   CAST(A.Address3 AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR    
   CAST(A.District AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR    
   CAST(A.City AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'        
   )    
   ORDER BY A.CompanyAutoId desc    
 END    
    
 ELSE IF @operation='DropDownCompany'    
 BEGIN    
 SELECT  DISTINCT CompanyAutoId Id, CompanyCode 'Code', CompanyName 'Text'    
     FROM tblCompany c    
     ORDER BY c.CompanyAutoId desc    
 END    
    
 ELSE IF @operation='GetCompanies'    
 BEGIN    
 SELECT DISTINCT TOP 5 CompanyAutoId Id, CompanyCode 'Code', CompanyName 'Text'    
     FROM tblCompany c    
      WHERE     
     (c.CompanyCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR    
     c.CompanyName LIKE '%'+ISNULL(@SearchText,'')+ '%' )    
     ORDER BY c.CompanyAutoId desc    
 END    
 ELSE IF @operation='GetNewCompaniesForAutoRef'    
 BEGIN    
 SELECT  DISTINCT TOP 5 c.CompanyAutoId Id, c.CompanyCode 'Code', c.CompanyName 'Text'    
     FROM tblCompany c INNER JOIN tblCompanyWorker cw ON c.CompanyAutoId = cw.CompanyAutoId    
     Left JOIN tblAutoRefTestWorker artw ON cw.WorkerAutoId=artw.WorkerAutoId    
     WHERE     
     (c.CompanyCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR    
     c.CompanyName LIKE '%'+ISNULL(@SearchText,'')+ '%' )    
     ORDER BY c.CompanyName    
 END    
    
 ELSE IF @operation='GetEditCompaniesForAutoRef'    
 BEGIN    
 SELECT DISTINCT TOP 5  c.CompanyAutoId Id, c.CompanyCode 'Code', c.CompanyName 'Text'    
     FROM tblCompany c INNER JOIN tblCompanyWorker cw ON c.CompanyAutoId = cw.CompanyAutoId    
     INNER JOIN tblAutoRefTestWorker artw ON cw.WorkerAutoId=artw.WorkerAutoId    
     WHERE     
     (c.CompanyCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR    
     c.CompanyName LIKE '%'+ISNULL(@SearchText,'')+ '%' )    
     ORDER BY c.CompanyName    
 END    
    
 ELSE IF @operation='GetNewCompaniesForOptometristWorker'    
 BEGIN    
 SELECT DISTINCT  TOP 5  c.CompanyAutoId Id, c.CompanyCode 'Code', c.CompanyName 'Text'    
     FROM tblCompany c INNER JOIN tblCompanyWorker cw ON c.CompanyAutoId = cw.CompanyAutoId    
     JOIN tblAutoRefTestWorker artw ON cw.WorkerAutoId=artw.WorkerAutoId    
     WHERE    
     (c.CompanyCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR    
     c.CompanyName LIKE '%'+ISNULL(@SearchText,'')+ '%' )    
     ORDER BY c.CompanyName    
 END    
    
 ELSE IF @operation='GetEditCompaniesForOptometristWorker'    
 BEGIN    
 SELECT  DISTINCT TOP 5 c.CompanyAutoId Id, c.CompanyCode 'Code', c.CompanyName 'Text'    
     FROM tblCompany c INNER JOIN tblCompanyWorker cw ON c.CompanyAutoId = cw.CompanyAutoId    
     INNER JOIN tblAutoRefTestWorker artw ON cw.WorkerAutoId=artw.WorkerAutoId    
     INNER JOIN tblOptometristWorker ow ON artw.AutoRefWorkerId=ow.AutoRefWorkerId    
     WHERE    
     (c.CompanyCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR    
     c.CompanyName LIKE '%'+ISNULL(@SearchText,'')+ '%' )    
     ORDER BY c.CompanyName    
 END    
  
 ELSE IF @operation='GetNewCompaniesForGlassDispense'    
 BEGIN    
 SELECT  DISTINCT TOP 5 c.CompanyAutoId Id, c.CompanyCode 'Code', c.CompanyName 'Text'    
     FROM tblCompany c INNER JOIN tblCompanyWorker cw ON c.CompanyAutoId = cw.CompanyAutoId    
	 INNER JOIN tblOptometristWorker ow ON cw.WorkerAutoId = ow.WorkerAutoId
     --Left JOIN tblGlassDespenseWorker  artw ON cw.WorkerAutoId=artw.WorkerAutoId    

     WHERE     
     (c.CompanyCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR    
     c.CompanyName LIKE '%'+ISNULL(@SearchText,'')+ '%' )    
     ORDER BY c.CompanyName    
 END    
    
 ELSE IF @operation='GetEditCompaniesForGlassDispense'    
 BEGIN    
 SELECT DISTINCT TOP 5  c.CompanyAutoId Id, c.CompanyCode 'Code', c.CompanyName 'Text'    
     FROM tblCompany c INNER JOIN tblCompanyWorker cw ON c.CompanyAutoId = cw.CompanyAutoId    
     INNER JOIN tblGlassDespenseWorker artw ON cw.WorkerAutoId=artw.WorkerAutoId    
     WHERE     
     (c.CompanyCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR    
     c.CompanyName LIKE '%'+ISNULL(@SearchText,'')+ '%' )    
     ORDER BY c.CompanyName    
 END    
  
  
 ELSE IF @operation = 'Delete'    
 BEGIN    
  DELETE    
  FROM   dbo.tblCompany    
  WHERE  CompanyAutoId = @CompanyAutoId    
  IF EXISTS(SELECT 1 FROM tblCompanyImage ci WHERE ci.CompanyAutoId=@CompanyAutoId)    
  BEGIN    
   DELETE FROM tblCompanyImage WHERE CompanyAutoId=@CompanyAutoId    
  END    
  SELECT @CompanyAutoId AS CompanyAutoId,'Successfully Deleted' AS RESULT    
 END
GO


 
CREATE OR ALTER PROCEDURE [dbo].[sp_SetupCompanyImage](
	@CompanyImageAutoId INT=NULL,
	@CompanyAutoId	INT = NULL,  
	@CompanyPic		VARCHAR(MAX) = NULL,
	@FileType		NVARCHAR(20) = NULL,
	@FileSize		INT = NULL,
	@CaptureRemarks  VARCHAR(500)=NULL,
	@UserId nvarchar(500) = NULL,  
	@EntDate DATETIME = NULL,  
	@EntOperation nvarchar(200) = NULL,  
	@EntryTerminal nvarchar(400) = NULL,  
	@EntryTerminalIP  nvarchar(400)  = NULL,
	@UserEmpName NVARCHAR(100)=NULL,
	@operation VARCHAR(50)= NULL
)
AS
IF @operation = 'Save'
BEGIN
    	INSERT INTO tblCompanyImage (CompanyAutoId, CompanyPic, FileType, FileSize, CaptureDate, CaptureRemarks, UserId, EntDate, EntOperation, EntTerminal, EntTerminalIP)
		SELECT   @CompanyAutoId, @CompanyPic, SUBSTRING(@FileType,1,10), @FileSize,GETDATE(),@CaptureRemarks, @UserId, @EntDate, 'INSERT', @EntryTerminal, @EntryTerminalIP  
		SET @CompanyImageAutoId=SCOPE_IDENTITY()
		SELECT  @CompanyImageAutoId CompanyImageAutoId ,'Successfully Saved' AS Result
END

ELSE IF @operation='Update'
BEGIN

		UPDATE tblCompanyImage SET CompanyPic = @CompanyPic, FileType = @FileType, FileSize = @FileSize, 
		UserId = @UserId, EntDate = @EntDate, EntOperation = 'UPDATE', EntTerminal = @EntryTerminal, EntTerminalIP = @EntryTerminalIP    
		WHERE @CompanyAutoId = CompanyAutoId
		SELECT @CompanyAutoId AS CompanyAutoId,'Successfully Updated' AS RESULT
END


ELSE IF @operation='Delete'
BEGIN

		DELETE FROM tblCompanyImage WHERE CompanyImageAutoId=@CompanyImageAutoId
		SELECT @CompanyImageAutoId AS CompanyAutoId,'Successfully Deleted' AS RESULT
END
GO


     
CREATE OR Alter PROCEDURE [dbo].[Sp_GetCode](        
@CodeType VARCHAR(100) =null,        
@CodeLength INT =null,        
@PreFix VARCHAR(10)=NULL,        
@LastUsedCode INT =null,        
@CompanyCode VARCHAR(10)=NULL,        
@CompanyId INT =NULL,        
@LocalityId INT =NULL,        
@LocalityCode VARCHAR(10) =NULL,        
@GothId INT =NULL,        
@GothCode VARCHAR(10) =NULL,      
@PublicSpacesId INT =NULL,        
@PublicSpacesCode VARCHAR(10) =NULL,      
@operation VARCHAR(100)=NULL,        
@Code VARCHAR(20) =NULL OUTPUT        
)        
AS         
IF ISNULL(@CompanyCode,'0') !='0' AND @CodeType='CompanyWorker'        
 SELECT @CompanyId= c.CompanyAutoId FROM tblCompany c WHERE @CompanyCode=c.CompanyCode        
        
ELSE IF ISNULL(@LocalityCode,'0')!='0' AND @CodeType='LocalityResident'        
 SELECT @LocalityId= c.LocalityAutoId FROM tblLocalities  c WHERE @LocalityCode=c.LocalityCode        
      
ELSE IF ISNULL(@GothCode,'0')!='0' AND @CodeType='GothsResident'        
 SELECT @GothId= c.GothAutoId FROM tblGoths c WHERE @GothCode=c.GothCode      
    
ELSE IF ISNULL(@PublicSpacesCode,'0')!='0' AND @CodeType='PublicSpacesResident'        
 SELECT @PublicSpacesId= c.PublicSpacesAutoId FROM tblPublicSpaces c WHERE @PublicSpacesCode=c.PublicSpacesCode      
    
        
  ---- Company Worker-----      
IF @operation  ='GetCompanyCode'        
BEGIN        
 IF NOT EXISTS(SELECT 1 FROM tbl_Codes tc WHERE tc.CodeType=@CodeType AND tc.CompanyId=@CompanyId)        
 INSERT INTO tbl_Codes (CodeType, PreFix, CodeLength, LastUsedCode, CompanyId)        
   SELECT @CodeType,@PreFix,@CodeLength,0,@CompanyId        
        
 SELECT @Code=ISNULL(LastUsedCode,0)+1 FROM tbl_Codes WITH (NOLOCK) WHERE CodeType=@CodeType AND CompanyId=@CompanyId        
        
 WHILE(LEN(@Code )< ISNULL(@CodeLength,1))        
 BEGIN        
  SET @code = '0'+@Code        
 END        
 SET @Code = @Code+@PreFix+@CompanyCode        
END        
--ELSE IF @operation  ='Code'        
--BEGIN        
-- IF NOT EXISTS(SELECT 1 FROM tbl_Codes tc WHERE tc.CodeType=@CodeType AND tc.CompanyId=@CompanyId)        
-- INSERT INTO tbl_Codes (CodeType, PreFix, CodeLength, LastUsedCode, CompanyId)        
--   SELECT @CodeType,@PreFix,@CodeLength,0,@CompanyId        
        
-- SELECT @Code=ISNULL(LastUsedCode,0)+1 FROM tbl_Codes WITH (NOLOCK) WHERE CodeType=@CodeType AND CompanyId=@CompanyId        
        
-- WHILE(LEN(@Code )< ISNULL(@CodeLength,1))        
-- BEGIN        
--  SET @code = '0'+@Code        
-- END        
-- SET @Code = @Code+@PreFix+@CompanyCode        
-- SELECT @Code AS result        
--END        
ELSE IF @operation = 'UpdateCompanyCode'        
BEGIN        
 UPDATE tbl_Codes        
 SET         
 LastUsedCode=ISNULL(LastUsedCode,0)+1        
 WHERE        
 CompanyId=@CompanyId AND        
 CodeType=@CodeType         
END        
 ---- END -----      
      
      
  ---- Locality Resident -----      
      
ELSE IF @operation  ='GetLocalityCode'        
BEGIN        
 IF NOT EXISTS(SELECT 1 FROM tbl_Codes tc WHERE tc.CodeType=@CodeType AND tc.LocalityId=@LocalityId)        
 INSERT INTO tbl_Codes (CodeType, PreFix, CodeLength, LastUsedCode, LocalityId)        
   SELECT @CodeType,@PreFix,@CodeLength,0,@LocalityId        
        
 SELECT @Code=ISNULL(LastUsedCode,0)+1 FROM tbl_Codes WITH (NOLOCK) WHERE CodeType=@CodeType AND LocalityId=@LocalityId        
        
 WHILE(LEN(@Code )< ISNULL(@CodeLength,1))        
 BEGIN        
  SET @code = '0'+@Code        
 END        
 SET @Code = @Code+@PreFix+@LocalityCode        
END        
        
        
ELSE IF @operation = 'UpdateLocalityCode'        
BEGIN        
 UPDATE tbl_Codes        
 SET         
 LastUsedCode=ISNULL(LastUsedCode,0)+1        
 WHERE        
 LocalityId=@LocalityId AND        
 CodeType=@CodeType        
END        
      
---- END -----      
      
      
---- Goth Resident ----      
      
ELSE IF @operation  ='GetGothCode'        
BEGIN        
 IF NOT EXISTS(SELECT 1 FROM tbl_Codes tc WHERE tc.CodeType=@CodeType AND tc.GothId=@GothId)        
 INSERT INTO tbl_Codes (CodeType, PreFix, CodeLength, LastUsedCode, GothId)        
   SELECT @CodeType,@PreFix,@CodeLength,0,@GothId        
        
 SELECT @Code=ISNULL(LastUsedCode,0)+1 FROM tbl_Codes WITH (NOLOCK) WHERE CodeType=@CodeType AND GothId=@GothId        
        
 WHILE(LEN(@Code )< ISNULL(@CodeLength,1))        
 BEGIN        
  SET @code = '0'+@Code        
 END        
 SET @Code = @Code+@PreFix+@GothCode        
END        
        
ELSE IF @operation = 'UpdateGothCode'        
BEGIN        
 UPDATE tbl_Codes        
 SET         
 LastUsedCode=ISNULL(LastUsedCode,0)+1        
 WHERE        
 GothId=@GothId AND        
 CodeType=@CodeType        
END        
      
---- END -----      
    
    
---- Public Spaces Resident ----      
      
ELSE IF @operation  ='GetPublicSpacesCode'        
BEGIN        
 IF NOT EXISTS(SELECT 1 FROM tbl_Codes tc WHERE tc.CodeType=@CodeType AND tc.PublicSpacesId=@PublicSpacesId)        
 INSERT INTO tbl_Codes (CodeType, PreFix, CodeLength, LastUsedCode, PublicSpacesId)        
   SELECT @CodeType,@PreFix,@CodeLength,0,@PublicSpacesId        
        
 SELECT @Code=ISNULL(LastUsedCode,0)+1 FROM tbl_Codes WITH (NOLOCK) WHERE CodeType=@CodeType AND PublicSpacesId=@PublicSpacesId    
        
 WHILE(LEN(@Code )< ISNULL(@CodeLength,1))        
 BEGIN        
  SET @code = '0'+@Code        
 END        
 SET @Code = @Code+@PreFix+@PublicSpacesCode        
END        
        
ELSE IF @operation = 'UpdatePublicSpacesCode'        
BEGIN        
 UPDATE tbl_Codes        
 SET         
 LastUsedCode=ISNULL(LastUsedCode,0)+1        
 WHERE        
 PublicSpacesId=@PublicSpacesId AND        
 CodeType=@CodeType        
END        
      
---- END ----- 

GO


 
CREATE OR ALTER PROCEDURE [dbo].[Sp_CompanyWorker](
@WorkerAutoId int=NULL,
@CompanyAutoId int=NULL,
@CompanyCode VARCHAR(20)=NULL,
@WorkerCode nvarchar(15)=NULL,
@WorkerName nvarchar(500)=NULL,
@RelationType NVARCHAR(100)=NULL,
@RelationName nvarchar(500)=NULL,
@Age int=NULL,
@GenderAutoId int=NULL,
@DecreasedVision bit=NULL,
@Distance BIT= NULL,
@Near BIT= NULL,
@WearGlasses bit=NULL,
@CNIC VARCHAR(30) =NULL,
@UserId nvarchar(250)=NULL,
@EnrollmentDate DATETIME =NULL,
@EntryTerminal VARCHAR(200)=NULL,
@EntryTerminalIP VARCHAR(200)=NULL,
@HasOccularHistory bit=NULL,
@OccularHistoryRemarks nvarchar(500)=NULL,
@HasMedicalHistory bit=NULL,
@MedicalHistoryRemarks nvarchar(500)=NULL,
@HasChiefComplain bit=NULL,
@ChiefComplainRemarks nvarchar(500)=NULL,
@WorkerTestDate datetime=NULL,
@WorkerRegNo varchar(20)=NULL,
@MobileNo VARCHAR(15)=NULL,
@SectionAutoId int=NULL,
@ApplicationID nvarchar(20)=NULL,
@FormId nvarchar(250)=NULL,
@UserEmpId int=NULL,
@UserEmpName nvarchar(250)=NULL,
@UserEmpCode nvarchar(10)=NULL,
@Religion bit=NULL,
@operation VARCHAR(50)= NULL,
@SearchText VARCHAR(MAX)=NULL
)
AS 
DECLARE @WorkerNewCode VARCHAR(10)=null
IF @operation='Save'
BEGIN
	IF NOT EXISTS(SELECT 1 FROM tblCompanyWorker c WITH (NOLOCK) WHERE c.WorkerName=@WorkerName AND c.CNIC=@CNIC )
	BEGIN
	BEGIN TRAN
	BEGIN TRy
	SELECT @CompanyAutoId=c.CompanyAutoId FROM tblCompany c WHERE c.CompanyCode=@CompanyCode 
	EXEC Sp_GetCode @CodeType = 'CompanyWorker',@CodeLength = 4	   ,@PreFix = '04'	   ,@CompanyCode = @CompanyCode	  
					,@CompanyId = @CompanyAutoId   ,@operation = 'GetCompanyCode',@Code = @WorkerNewCode OUTPUT

			INSERT INTO dbo.tblCompanyWorker    (CompanyCode,CompanyAutoId, WorkerCode, WorkerName, RelationType,RelationName, 
                                      Age, GenderAutoId,CNIC,MobileNo, DecreasedVision,Near,Distance,WearGlasses, UserId, EntDate, EntOperation, 
                                      EntTerminal, EntTerminalIP, HasOccularHistory, OccularHistoryRemarks,
                                      HasMedicalHistory, MedicalHistoryRemarks, HasChiefComplain, ChiefComplainRemarks, 
                                      WorkerTestDate, WorkerRegNo, SectionAutoId, ApplicationID, FormId, 
                                      UserEmpId, UserEmpName, UserEmpCode, Religion)				

			SELECT @CompanyCode,@CompanyAutoId,@WorkerNewCode, @WorkerName, @RelationType,@RelationName, 
                                      @Age, @GenderAutoId,@CNIC,@MobileNo, @DecreasedVision,@Near,@Distance,@WearGlasses, @UserId, @EnrollmentDate, 'INSERT', 
                                      @EntryTerminal, @EntryTerminalIP, @HasOccularHistory, @OccularHistoryRemarks, 
                                      @HasMedicalHistory, @MedicalHistoryRemarks, @HasChiefComplain, @ChiefComplainRemarks, 
                                      Getdate(), @WorkerRegNo, @SectionAutoId, @ApplicationID, @FormId, 
                                      @UserEmpId, @UserEmpName, @UserEmpCode, @Religion	

			  SET @WorkerAutoId = SCOPE_IDENTITY()

			EXEC Sp_GetCode @CodeType = 'CompanyWorker',@CodeLength = 4	   ,@PreFix = '04'	   ,@CompanyCode = @CompanyCode	   ,
							@CompanyId = @CompanyAutoId   ,@operation = 'UpdateCompanyCode'
			SELECT @WorkerAutoId AS WorkerAutoId,'Successfully Saved: <br> Woker Code: '+@WorkerNewCode AS RESULT
			Commit
END TRY
BEGIN CATCH
		SELECT @WorkerAutoId AS WorkerAutoId,'Error'+ERROR_MESSAGE() AS RESULT
ROLLBACK TRAN
END Catch
		END
		ELSE
		SELECT @CompanyAutoId AS CompanyAutoId,'Worker with same detail Already Exists.' AS RESULT
END
ELSE IF @operation = 'Update'
BEGIN
BEGIN TRAN
BEGIN TRy

	IF(@CompanyCode != (SELECT top 1 cw.CompanyCode FROM tblCompanyWorker cw WITH (NOLOCK) WHERE cw.WorkerAutoId=@WorkerAutoId))
	BEGIN
		SELECT @CompanyAutoId=c.CompanyAutoId FROM tblCompany c WHERE c.CompanyCode=@CompanyCode 
		EXEC Sp_GetCode @CodeType = 'CompanyWorker',@CodeLength = 4	   ,@PreFix = '04'	   ,@CompanyCode = @CompanyCode	   ,@CompanyId = @CompanyAutoId   
		,@operation = 'GetCompanyCode',@Code = @WorkerNewCode OUTPUT
		EXEC Sp_GetCode @CodeType = 'CompanyWorker',@CodeLength = 4	   ,@PreFix = '04'	   ,@CompanyCode = @CompanyCode	   ,@CompanyId = @CompanyAutoId  
		,@operation = 'UpdateCompanyCode'
		SET @WorkerCode=@WorkerNewCode
	END
	ELSE
	BEGIN
		SELECT @WorkerCode=cw.WorkerCode FROM tblCompanyWorker cw WITH (NOLOCK) WHERE cw.WorkerAutoId=@WorkerAutoId
	END
		UPDATE tblCompanyWorker 
		SET CompanyAutoId = @CompanyAutoId
		   ,WorkerName = @WorkerName 
		   ,CompanyCode=@CompanyCode
		   ,RelationType=@RelationType
		   ,RelationName = @RelationName 
		   ,Age = @Age 
		   ,GenderAutoId = @GenderAutoId
		   ,CNIC = @CNIC
		   ,DecreasedVision = @DecreasedVision 
		   ,Near=@Near
		   ,Distance=@Distance
		   ,EntOperation = 'Update'
		   ,WearGlasses = @WearGlasses 
		   ,HasOccularHistory = @HasOccularHistory 
		   ,OccularHistoryRemarks = @OccularHistoryRemarks 
		   ,HasMedicalHistory = @HasMedicalHistory 
		   ,MedicalHistoryRemarks = @MedicalHistoryRemarks 
		   ,HasChiefComplain = @HasChiefComplain 
		   ,ChiefComplainRemarks = @ChiefComplainRemarks 
		   ,WorkerTestDate = @WorkerTestDate 
		   ,WorkerCode=@WorkerCode
		   ,MobileNo = @MobileNo
		   ,Religion = @Religion 
			WHERE 
			WorkerAutoId = @WorkerAutoId	

				SELECT @WorkerAutoId AS WorkerAutoId,'Successfully Updated: <br> '+@WorkerCode AS RESULT
			Commit
END TRY
BEGIN CATCH
		SELECT @WorkerAutoId AS WorkerAutoId,'Error Updated' AS RESULT
ROLLBACK TRAN
END Catch
END

ELSE IF @operation = 'Delete'
	BEGIN
		DELETE
		FROM   dbo.tblCompanyWorker
		WHERE  WorkerAutoId = @WorkerAutoId
		IF EXISTS(SELECT 1 FROM tblCompanyWorkerImage ci WHERE ci.WorkerAutoId=@WorkerAutoId)
		BEGIN
			DELETE FROM tblCompanyWorkerImage WHERE WorkerAutoId=@WorkerAutoId
		END
		SELECT @WorkerAutoId AS WorkerAutoId,'Successfully Deleted' AS RESULT
	END
ELSE IF @operation='GetWorkerByiD'
	BEGIN
	SELECT	c.WorkerAutoId,c.CompanyAutoId,c.companyCode, c.WorkerCode, c.WorkerName,c.RelationType, c.RelationName, c.Age, ISNULL(c.GenderAutoId,0)GenderAutoId
	        , c.CNIC, c.DecreasedVision,c.UserId, c.EntDate, c.Near,c.Distance,	c.EntOperation, c.EntTerminal, c.EntTerminalIP, c.WearGlasses,
			c.HasOccularHistory, c.OccularHistoryRemarks, c.HasMedicalHistory, c.MedicalHistoryRemarks,c.EntDate EnrollmentDate,c.MobileNo,
			c.HasChiefComplain, c.ChiefComplainRemarks, c.WorkerTestDate, c.WorkerRegNo, c.SectionAutoId, c.ApplicationID, c.FormId, c.UserEmpId,c1.CompanyName,
			c.UserEmpName, c.UserEmpCode, c.Religion, c.EntDate,ci.WorkerImageAutoId,ci.WorkerAutoId,ci.WorkerImageAutoId,ISNULL(ci.CaptureRemarks,'')CaptureRemarks
			,ci.CompanyAutoId DetailCompanyAutoId,ci.FileType,ci.FileSize,ci.WorkerPic
		   FROM tblCompanyWorker c	 INNER JOIN tblCompany c1 ON c1.CompanyAutoId=c.CompanyAutoId
		   LEFT JOIN tblCompanyWorkerImage ci ON c.WorkerAutoId = ci.WorkerAutoId
		WHERE 
		c.WorkerAutoId=@WorkerAutoId
	END

	ELSE IF @operation='GetAllWorker'
	BEGIN
	SELECT TOP 10 c.WorkerAutoId,c.WorkerCode,c.WorkerName,c1.CompanyName,c.CNIC,c.MobileNo
		   FROM tblCompanyWorker c INNER JOIN tblCompany c1 ON c.CompanyAutoId = c1.CompanyAutoId
		   ORDER BY c.WorkerAutoId desc
	END

	
	ELSE IF @operation = 'Search'
	BEGIN
	SELECT TOP 10 c.WorkerAutoId,c.WorkerCode,c.WorkerName,A.CompanyName,c.CNIC,c.MobileNo
		   FROM tblCompany A INNER JOIN tblCompanyWorker c ON A.CompanyAutoId = c.CompanyAutoId
		 WHERE
		 (
			 CAST(c.WorkerCode AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR
			 CAST(A.CompanyName AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR
			 CAST(c.WorkerName AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR
			 CAST(c.CNIC AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR
			 CAST(c.MobileNo AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'    
		 )
		 ORDER BY c.WorkerAutoId desc
	END

	ELSE IF @operation= 'GetWorkers'
	BEGIN
	SELECT DISTINCT  TOP 5  cw.WorkerAutoId 'Id', cw.WorkerCode 'Code', cw.WorkerName +' | '+cw.WorkerCode 'Text'
		   FROM tblCompanyWorker cw WITH(NOLOCK) 
		   WHERE
		   cw.CompanyAutoId=@CompanyAutoId AND 
		   (cw.WorkerCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR
		   cw.WorkerName LIKE '%'+ISNULL(@SearchText,'')+ '%' )
		   ORDER BY  cw.WorkerName +' | '+cw.WorkerCode 
	END
	ELSE IF @operation= 'DeleteWorkerById'
	BEGIN
		DELETE FROM tblCompanyWorker WHERE WorkerAutoId=@WorkerAutoId
		DELETE FROM tblCompanyWorkerImage WHERE WorkerAutoId=@WorkerAutoId

		SELECT @WorkerAutoId AS WorkerAutoId,'Successfully Deleted' AS RESULT

		
	END
GO


 


CREATE OR ALTER PROCEDURE [dbo].[Sp_AutoRefTestWorker](
@AutoRefWorkerId INT = NULL,
    @AutoRefWorkerTransId varchar(15) = NULL,
    @AutoRefWorkerTransDate datetime = NULL,
    @WorkerAutoId int= NULL,
    @Right_Spherical_Status char(1)= NULL,
    @Right_Spherical_Points decimal(9, 2)= NULL,
    @Right_Cyclinderical_Status char(1)= NULL,
    @Right_Cyclinderical_Points decimal(9, 2)= NULL,
    @Right_Axix_From int= NULL,
    @Right_Axix_To int= NULL,
    @Left_Spherical_Status char(1)= NULL,
    @Left_Spherical_Points decimal(9, 2)= NULL,
    @Left_Cyclinderical_Status char(1)= NULL,
    @Left_Cyclinderical_Points decimal(9, 2)= NULL,
    @Left_Axix_From int= NULL,
    @Left_Axix_To int= NULL,
	@IPD INT = NULL,
	@CompanyAutoId INT =NULL,
    @UserId nvarchar(250)= NULL,
    @EntDate datetime= NULL,
    @EntOperation nvarchar(100)= NULL,
    @EntryTerminal nvarchar(200)= NULL,
    @EntryTerminalIP nvarchar(200)= NULL,
    @ApplicationID nvarchar(20)= NULL,
    @FormId nvarchar(250)= NULL,
    @UserEmpId int= NULL,
    @UserEmpName nvarchar(250)= NULL,
    @UserEmpCode nvarchar(10)= NULL,
	@operation VARCHAR(50)= NULL,
	@SearchText VARCHAR(MAX)=NULL
	)
	AS
IF @operation='Save'
	BEGIN
	IF NOT EXISTS(SELECT 1 FROM tblAutoRefTestWorker artw WHERE artw.WorkerAutoId=@WorkerAutoId AND CAST(artw.AutoRefWorkerTransDate AS DATE)=CAST(@AutoRefWorkerTransDate AS date))
	BEGIN
	 INSERT INTO dbo.tblAutoRefTestWorker (AutoRefWorkerTransId, AutoRefWorkerTransDate, 
                                          WorkerAutoId, Right_Spherical_Status, Right_Spherical_Points, 
                                          Right_Cyclinderical_Status, Right_Cyclinderical_Points, Right_Axix_From, 
                                          Right_Axix_To, Left_Spherical_Status, Left_Spherical_Points, 
                                          Left_Cyclinderical_Status, Left_Cyclinderical_Points, Left_Axix_From, 
                                          Left_Axix_To,IPD, UserId, EntDate, EntOperation, EntTerminal, 
                                          EntTerminalIP, ApplicationID, FormId, UserEmpId, UserEmpName, 
                                          UserEmpCode)
    SELECT @AutoRefWorkerTransId, @AutoRefWorkerTransDate, @WorkerAutoId, @Right_Spherical_Status, 
           @Right_Spherical_Points, @Right_Cyclinderical_Status, @Right_Cyclinderical_Points, @Right_Axix_From, 
           @Right_Axix_To, @Left_Spherical_Status, @Left_Spherical_Points, @Left_Cyclinderical_Status, 
           @Left_Cyclinderical_Points, @Left_Axix_From, @Left_Axix_To,@IPD, @UserId, GETDATE(), 'INSERT', 
           @EntryTerminal, @EntryTerminalIP, @ApplicationID, @FormId, @UserEmpId, @UserEmpName, @UserEmpCode
		   SET @AutoRefWorkerId = SCOPE_IDENTITY()
		   SELECT @AutoRefWorkerId  AS AutoRefWorkerId,'Successfully Saved' AS RESULT
	END
	ELSE
	   SELECT @AutoRefWorkerId  AS AutoRefWorkerId,'Worker Auto Refraction Already Exists in selected Date' AS RESULT
	END
ELSE IF @operation='Update'
	BEGIN
		UPDATE dbo.tblAutoRefTestWorker
		SET    AutoRefWorkerTransId = @AutoRefWorkerTransId, WorkerAutoId = @WorkerAutoId, Right_Spherical_Status = @Right_Spherical_Status, 
				Right_Spherical_Points = @Right_Spherical_Points, Right_Cyclinderical_Status = @Right_Cyclinderical_Status, 
				Right_Cyclinderical_Points = @Right_Cyclinderical_Points, Right_Axix_From = @Right_Axix_From, 
				Right_Axix_To = @Right_Axix_To, Left_Spherical_Status = @Left_Spherical_Status, Left_Spherical_Points = @Left_Spherical_Points, 
				Left_Cyclinderical_Status = @Left_Cyclinderical_Status, Left_Cyclinderical_Points = @Left_Cyclinderical_Points, 
				Left_Axix_From = @Left_Axix_From, Left_Axix_To = @Left_Axix_To,IPD=@IPD, UserId = @UserId,
				EntOperation = 'Update', EntTerminal = @EntryTerminal, EntTerminalIP = @EntryTerminalIP, 
				UserEmpId = @UserEmpId, UserEmpName = @UserEmpName, UserEmpCode = @UserEmpCode
		WHERE  AutoRefWorkerId = @AutoRefWorkerId 

		SELECT @AutoRefWorkerId  AS AutoRefWorkerId,'Successfully Updated' AS RESULT
	END

ELSE IF @operation='GetByAutoRefWorkerId '
	BEGIN
		SELECT AutoRefWorkerId, AutoRefWorkerTransId, AutoRefWorkerTransDate, WorkerAutoId, Right_Spherical_Status, Right_Spherical_Points, 
		Right_Cyclinderical_Status, Right_Cyclinderical_Points, Right_Axix_From, Right_Axix_To, Left_Spherical_Status, Left_Spherical_Points, 
		Left_Cyclinderical_Status, Left_Cyclinderical_Points, Left_Axix_From, Left_Axix_To, ISNULL(IPD,0)IPD,UserId, EntDate, EntOperation, EntTerminal,
		EntTerminalIP, ApplicationID, FormId, UserEmpId, UserEmpName, UserEmpCode ,0 WearGlasses
		FROM   dbo.tblAutoRefTestWorker
		WHERE  AutoRefWorkerId = @AutoRefWorkerId  
END
ELSE IF @operation='GetAutoRefByWorkerId '
	BEGIN
		SELECT 
				--b.WorkerAutoId,b.CompanyAutoId,b.WorkerCode,b.WorkerName,b.RelationType,b.RelationName,
				--b.Age,CASE WHEN b.GenderAutoId = 1 THEN 'Male' ELSE 'Female' END Gender,b.GenderAutoId,b.CNIC,b.MobileNo,
				--ISNULL(b.WearGlasses,0)WearGlasses,ISNULL(b.Distance,0)Distance,ISNULL(b.Near,0)Near,ISNULL(b.DecreasedVision,0)DecreasedVision,
				--ISNULL(b.HasOccularHistory,0)HasOccularHistory,ISNULL(b.HasMedicalHistory,0)HasMedicalHistory,ISNULL(b.HasChiefComplain,0)HasChiefComplain,
			   a.AutoRefWorkerId, a.AutoRefWorkerTransId, CAST(a.AutoRefWorkerTransDate AS varchar) 'Test Date', a.WorkerAutoId AutoRefWorkerId, 
			   a.Right_Spherical_Status, CASE WHEN  ISNULL(a.Right_Spherical_Points,0) > 0 THEN '+'+CAST(a.Right_Spherical_Points AS VARCHAR)
			   ELSE CAST(a.Right_Spherical_Points AS VARCHAR) END 'Right Spherical', a.Right_Cyclinderical_Status,	   
			   CASE WHEN ISNULL(a.Left_Spherical_Points,0) > 0 THEN '+'+ CAST(a.Left_Spherical_Points AS VARCHAR)
			   ELSE CAST(a.Left_Spherical_Points AS VARCHAR) END 'Left Spherical', 
			   '', a.Right_Axix_To, a.Left_Spherical_Status, a.Right_Cyclinderical_Points 'Right Cyclinderical', 
			   a.Left_Cyclinderical_Status,a.Left_Cyclinderical_Points 'Left Cyclinderical',a.Right_Axix_From AS 'Right Axis' , a.Left_Axix_From AS 'Left Axis',
			   a.Left_Axix_To,ISNULL(IPD,0)IPD, a.UserId, a.EntDate, a.EntOperation, a.EntTerminal, a.EntTerminalIP, a.ApplicationID, a.FormId, a.UserEmpId, a.UserEmpName, a.UserEmpCode 
			   ,b.WearGlasses
			   FROM  tblCompanyWorker b INNER  JOIN dbo.tblAutoRefTestWorker a ON b.WorkerAutoId=a.WorkerAutoId
		WHERE  b.WorkerAutoId=@WorkerAutoId
	END
ELSE IF @operation='NewWorkerForRef'
BEGIN
	SELECT DISTINCT top 5 cw.WorkerAutoId Id, cw.WorkerName +' | '+cw.WorkerCode  text,cw.WorkerCode Code FROM tblCompanyWorker cw
	WHERE 
	cw.CompanyAutoId=@CompanyAutoId AND 
	cw.WorkerAutoId NOT IN (SELECT artw.WorkerAutoId FROM tblAutoRefTestWorker artw WITH (NOLOCK) WHERE CAST(artw.AutoRefWorkerTransDate AS DATE) = CAST(@AutoRefWorkerTransDate AS DATE))
	 AND 
	(cw.WorkerCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR
	 cw.WorkerName LIKE '%'+ISNULL(@SearchText,'')+ '%' )
	ORDER BY  cw.WorkerName +' | '+cw.WorkerCode 
END

ELSE IF @operation='EditWorkerForRef'
BEGIN
	SELECT DISTINCT TOP 5 cw.WorkerAutoId Id, cw.WorkerName +' | '+cw.WorkerCode  text,cw.WorkerCode Code 
	FROM tblCompanyWorker cw INNER JOIN tblAutoRefTestWorker artw ON cw.WorkerAutoId=artw.WorkerAutoId
	WHERE 
	cw.CompanyAutoId=@CompanyAutoId 
	AND (cw.WorkerCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR
	 cw.WorkerName LIKE '%'+ISNULL(@SearchText,'')+ '%' )
	ORDER BY  cw.WorkerName +' | '+cw.WorkerCode 
END
ELSE IF @operation='GetAutoRefHistoryByWorkerId '
	BEGIN
		--SELECT TOP 1 CAST(FORMAT(artw.AutoRefWorkerTransDate,'dd | MMM | yyyy') AS VARCHAR) AS Last_Visit_Date,
		-- CASE WHEN  ISNULL(artw.Right_Spherical_Points,0) > 0 THEN '+'+CAST(artw.Right_Spherical_Points AS VARCHAR)
		-- ELSE CAST(artw.Right_Spherical_Points AS VARCHAR) END 'Right Spherical' ,
		-- CASE WHEN ISNULL(artw.Left_Spherical_Points,0) > 0 THEN '+'+ CAST(artw.Left_Spherical_Points AS VARCHAR)
		-- ELSE CAST(artw.Left_Spherical_Points AS VARCHAR) END 'Left Spherical',

		-- CASE WHEN  ISNULL(artw.Right_Cyclinderical_Points,0) > 0 THEN '+'+CAST(artw.Right_Cyclinderical_Points AS VARCHAR)
		-- ELSE CAST(artw.Right_Cyclinderical_Points AS VARCHAR) END 'Right Cyclinderical' ,
		-- CASE WHEN  ISNULL(artw.Left_Cyclinderical_Points,0) > 0 THEN '+'+CAST(artw.Left_Cyclinderical_Points AS VARCHAR)
		-- ELSE CAST(artw.Left_Cyclinderical_Points AS VARCHAR) END 'Left Cyclinderical' ,

		-- CASE WHEN  ISNULL(artw.Right_Axix_From,0) > 0 THEN '+'+CAST(artw.Right_Axix_From AS VARCHAR)
		-- ELSE CAST(artw.Right_Axix_From AS VARCHAR) END 'Right Axis' ,
		-- CASE WHEN  ISNULL(artw.Left_Axix_From ,0) > 0 THEN '+'+CAST(artw.Left_Axix_From  AS VARCHAR)
		-- ELSE CAST(artw.Left_Axix_From  AS VARCHAR) END 'Left Axis' 

		--FROM tblAutoRefTestWorker artw
		--WHERE  artw.WorkerAutoId=@WorkerAutoId
		--ORDER BY artw.AutoRefWorkerTransDate desc
			SELECT TOP 1 CAST(FORMAT(artw.AutoRefWorkerTransDate,'dd | MMM | yyyy') AS VARCHAR) AS Last_Visit_Date,
		 CASE WHEN artw.Right_Spherical_Status ='P' AND artw.Right_Spherical_Points>0 THEN '+' +CAST(Right_Spherical_Points AS VARCHAR)
		 WHEN artw.Right_Spherical_Status='N'AND artw.Right_Spherical_Points>0 THEN '-' +CAST(Right_Spherical_Points AS VARCHAR)
		 WHEN artw.Right_Spherical_Status='E' THEN 'Error '+CAST(ISNULL(Right_Spherical_Points,'') AS VARCHAR)
		 WHEN artw.Right_Spherical_Status='l' THEN 'Plano '+CAST(ISNULL(Right_Spherical_Points,'') AS VARCHAR)
		 ELSE CAST(Right_Spherical_Points AS VARCHAR) END 'Right Spherical',

		 CASE WHEN artw.Left_Spherical_Status ='P' AND artw.Left_Spherical_Points>0 THEN '+' +CAST(Left_Spherical_Points AS VARCHAR)
		 WHEN artw.Left_Spherical_Status='N'AND artw.Left_Spherical_Points>0 THEN '-' +CAST(Left_Spherical_Points AS VARCHAR)
		  WHEN artw.Left_Spherical_Status='E' THEN 'Error '+CAST(ISNULL(Left_Spherical_Points,'') AS VARCHAR)
		 WHEN artw.Left_Spherical_Status='l' THEN 'Plano '+CAST(ISNULL(Left_Spherical_Points,'') AS VARCHAR)
		 ELSE CAST(Left_Spherical_Points AS VARCHAR) END 'Left Spherical',

		 CASE WHEN artw.Right_Spherical_Status ='P' AND artw.Left_Spherical_Points>0 THEN '+' +CAST(Left_Spherical_Points AS VARCHAR)
		 WHEN artw.Right_Spherical_Status='N'AND artw.Right_Spherical_Points>0 THEN '-' +CAST(Right_Spherical_Points AS VARCHAR)
		 ELSE CAST(Right_Spherical_Points AS VARCHAR) END 'Right Spherical',

		 CASE WHEN artw.Right_Cyclinderical_Status ='P' AND artw.Right_Cyclinderical_Points>0 THEN '+' +CAST(Right_Cyclinderical_Points AS VARCHAR)
		 WHEN artw.Right_Cyclinderical_Status='N'AND artw.Right_Cyclinderical_Points>0 THEN '-' +CAST(Right_Cyclinderical_Points AS VARCHAR)
		 ELSE CAST(Right_Cyclinderical_Points AS VARCHAR) END 'Right Cyclinderical', 

		 
		 CASE WHEN artw.Left_Cyclinderical_Status ='P' AND artw.Left_Cyclinderical_Points>0 THEN '+' +CAST(Left_Cyclinderical_Points AS VARCHAR)
		 WHEN artw.Left_Cyclinderical_Status='N'AND artw.Left_Cyclinderical_Points>0 THEN '-' +CAST(Left_Cyclinderical_Points AS VARCHAR)
		 ELSE CAST(artw.Left_Cyclinderical_Points AS VARCHAR) END 'Left Cyclinderical', 
		 artw.Right_Axix_From 'Right Axis' ,
		  artw.Left_Axix_From 'Left Axis' ,0 WearGlasses,
		  ISNULL(artw.IPD,0)IPD

		FROM tblAutoRefTestWorker artw
		WHERE  artw.WorkerAutoId=@WorkerAutoId
		ORDER BY artw.AutoRefWorkerTransDate desc
	END

	ELSE IF @operation= 'GetDatesofWorker'
	BEGIN
		SELECT CAST(FORMAT(artw.AutoRefWorkerTransDate,'dd | MMM | yyyy') AS VARCHAR) Text,
		artw.AutoRefWorkerId AS Id
		FROM tblAutoRefTestWorker artw
		WHERE artw.WorkerAutoId=@WorkerAutoId
		ORDER BY artw.AutoRefWorkerId desc
	END

	
ELSE IF @operation='GetByAutoRefWorkerIdForOpt '
	BEGIN
		SELECT TOP 1 a.AutoRefWorkerId, a.AutoRefWorkerTransId, a.AutoRefWorkerTransDate,a. WorkerAutoId, a.Right_Spherical_Status, 
		a.Right_Spherical_Points, a.Right_Cyclinderical_Status, a.Right_Cyclinderical_Points, a.Right_Axix_From, a.Right_Axix_To,
		a.Left_Spherical_Status, a.Left_Spherical_Points, 	a.Left_Cyclinderical_Status, a.Left_Cyclinderical_Points,
		a.Left_Axix_From, Left_Axix_To, ISNULL(IPD,0)IPD, a.UserId, a.EntDate, a.EntOperation, a.EntTerminal,
		a.EntTerminalIP, a.ApplicationID, a.FormId, a.UserEmpId, a.UserEmpName, a.UserEmpCode ,ISNULL(cw.WearGlasses,0)WearGlasses
		FROM   dbo.tblAutoRefTestWorker	a	INNER JOIN  tblCompanyWorker cw ON cw.WorkerAutoId=a.WorkerAutoId
		WHERE  a.WorkerAutoId = @WorkerAutoId
		ORDER BY AutoRefWorkerId desc
END
ELSE IF @operation= 'DeleteAutoRefById'
	BEGIN
		DELETE FROM tblAutoRefTestWorker WHERE AutoRefWorkerId=@AutoRefWorkerId
		SELECT @AutoRefWorkerId AS AutoRefWorkerId,'Successfully Deleted' AS RESULT
	END
GO


 
 
CREATE OR ALTER PROC [dbo].[Sp_OptometristWorker](
    @OptometristWorkerId INT = NULL,
    @OptometristWorkerTransDate datetime = NULL,
    @WorkerAutoId int = NULL,
	@AutoRefWorkerId INT=NULL,
    @HasChiefComplain int = NULL,
    @ChiefComplainRemarks nvarchar(200)= NULL,
    @HasOccularHistory int= NULL,
    @OccularHistoryRemarks nvarchar(200)= NULL,
    @HasMedicalHistory int= NULL,
    @MedicalHistoryRemarks nvarchar(200)= NULL,
    @DistanceVision_RightEye_Unaided int= NULL,
    @DistanceVision_RightEye_WithGlasses int= NULL,
    @DistanceVision_RightEye_PinHole int= NULL,
    @NearVision_RightEye int= NULL,
    @NeedCycloRefraction_RightEye int= NULL,
    @NeedCycloRefractionRemarks_RightEye nvarchar(200)= NULL,
    @DistanceVision_LeftEye_Unaided int= NULL,
    @DistanceVision_LeftEye_WithGlasses int= NULL,
    @DistanceVision_LeftEye_PinHole int= NULL,
    @NearVision_LeftEye int= NULL,
    @NeedCycloRefraction_LeftEye int= NULL,
    @NeedCycloRefractionRemarks_LeftEye nvarchar(200)= NULL,
    @Right_Spherical_Status char(1)= NULL,
    @Right_Spherical_Points decimal(9, 2)= NULL,
    @Right_Cyclinderical_Status char(1)= NULL,
    @Right_Cyclinderical_Points decimal(9, 2)= NULL,
    @Right_Axix_From int= NULL,
    @Right_Axix_To int= NULL,
    @Right_Near_Status char(1)= NULL,
    @Right_Near_Points decimal(9, 2)= NULL,
    @Left_Spherical_Status char(1)= NULL,
    @Left_Spherical_Points decimal(9, 2)= NULL,
    @Left_Cyclinderical_Status char(1)= NULL,
    @Left_Cyclinderical_Points decimal(9, 2)= NULL,
    @Left_Axix_From int= NULL,
    @Left_Axix_To int= NULL,
    @Left_Near_Status char(1)= NULL,
    @Left_Near_Points decimal(9, 2)= NULL,
    @Douchrome int= NULL,
    @Achromatopsia varchar(20)= NULL,
    @RetinoScopy_RightEye int= NULL, 
    @Condition_RightEye varchar(200)= NULL,
    @Meridian1_RightEye varchar(200)= NULL,
    @Meridian2_RightEye varchar(200)= NULL,
    @FinalPrescription_RightEye varchar(200)= NULL,
    @RetinoScopy_LeftEye int= NULL, 
    @Condition_LeftEye varchar(200)= NULL,
    @Meridian1_LeftEye varchar(200)= NULL,
    @Meridian2_LeftEye varchar(200)= NULL,
    @FinalPrescription_LeftEye varchar(200)= NULL,
    @Hirchberg_Distance int= NULL,
    @Hirchberg_Near int= NULL,
    @Ophthalmoscope_RightEye int= NULL,
    @PupillaryReactions_RightEye int= NULL,
    @CoverUncovertTest_RightEye int= NULL,
    @CoverUncovertTestRemarks_RightEye nvarchar(200)= NULL,
    @ExtraOccularMuscleRemarks_RightEye nvarchar(200)= NULL,
    @Ophthalmoscope_LeftEye int= NULL,
    @PupillaryReactions_LeftEye int= NULL,
    @CoverUncovertTest_LeftEye int= NULL,
    @CoverUncovertTestRemarks_LeftEye nvarchar(200)= NULL,
	@CycloplegicRefraction_RightEye BIT =NULL,
	@CycloplegicRefraction_LeftEye BIT =NULL,
	@Conjunctivitis_RightEye BIT =NULL,
	@Conjunctivitis_LeftEye BIT =NULL,
	@Scleritis_RightEye BIT =NULL,						  
	@Scleritis_LeftEye BIT =NULL,
	@Nystagmus_RightEye BIT =NULL,						  
	@Nystagmus_LeftEye BIT =NULL,
	@CornealDefect_RightEye BIT =NULL,
	@CornealDefect_LeftEye BIT =NULL,
	@Cataract_RightEye BIT =NULL,
	@Cataract_LeftEye BIT =NULL,
	@Keratoconus_RightEye BIT =NULL,
	@Keratoconus_LeftEye BIT =NULL,
	@Ptosis_RightEye BIT =NULL,
	@Ptosis_LeftEye BIT =NULL,
	@LowVision_RightEye BIT =NULL,
	@LowVision_LeftEye BIT =NULL,
	@Pterygium_RightEye BIT =NULL,
	@Pterygium_LeftEye BIT =NULL,
	@ColorBlindness_RightEye BIT =NULL,
	@ColorBlindness_LeftEye BIT =NULL,
	@Others_RightEye BIT =NULL,
	@Others_LeftEye BIT =NULL,
	@Fundoscopy_RightEye BIT =NULL,
	@Fundoscopy_LeftEye BIT =NULL,
	@Surgery_RightEye BIT =NULL,
	@Surgery_LeftEye BIT =NULL,
	@CataractSurgery_RightEye BIT =NULL,
	@CataractSurgery_LeftEye BIT =NULL,
	@SurgeryPterygium_RightEye BIT =NULL,
	@SurgeryPterygium_LeftEye BIT =NULL,
	@SurgeryCornealDefect_RightEye BIT =NULL,
	@SurgeryCornealDefect_LeftEye BIT =NULL,
	@SurgeryPtosis_RightEye BIT =NULL,
	@SurgeryPtosis_LeftEye BIT =NULL,
	@SurgeryKeratoconus_RightEye BIT =NULL,
	@SurgeryKeratoconus_LeftEye BIT =NULL,
	@Chalazion_RightEye BIT =NULL,
	@Chalazion_LeftEye BIT =NULL,
	@Hordeolum_RightEye BIT =NULL,
	@Hordeolum_LeftEye BIT =NULL,
	@SurgeryOthers_RightEye BIT =NULL,
	@SurgeryOthers_LeftEye BIT =NULL,
	@RightPupilDefects BIT =NULL,
	@LeftPupilDefects BIT =NULL,
	@RightSquint_Surgery BIT =NULL,
	@LeftSquint_Surgery BIT =NULL,

	@CompanyAutoId INT=NULL,
    @UserId nvarchar(250)= NULL,
    @EntDate datetime= NULL,
    @EntOperation nvarchar(100)= NULL,
    @EntryTerminal nvarchar(200)= NULL,
    @EntryTerminalIP nvarchar(200)= NULL,
    @ExtraOccularMuscleRemarks_LeftEye nvarchar(200)= NULL,
    @ApplicationID nvarchar(20)= NULL,
    @FormId nvarchar(250)= NULL,
    @UserEmpId int= NULL,
    @UserEmpName nvarchar(250)= NULL,
    @UserEmpCode nvarchar(10)= NULL,
    @VisualAcuity_RightEye int= NULL,
    @VisualAcuity_LeftEye int= NULL,
	@LeftSquint_VA BIT= NULL,
	@RightSquint_VA BIT= NULL,
	@LeftAmblyopic_VA BIT= NULL,
	@RightAmblyopic_VA BIT= NULL,
	@LeftAmblyopia BIT=NULL,
	@RightAmblyopia BIT=NULL,
	@Operation VARCHAR(50)=NULL,
	@SearchText VARCHAR(1000)=null
	)
AS 
    IF @operation = 'Save'
	BEGIN
		SELECT @CompanyAutoId=@CompanyAutoId FROM tblCompanyWorker cw WHERE cw.WorkerAutoId=@WorkerAutoId
		INSERT INTO dbo.tblOptometristWorker (OptometristWorkerTransDate,AutoRefWorkerId , WorkerAutoId,CompanyAutoId, HasChiefComplain, ChiefComplainRemarks, HasOccularHistory,
											  OccularHistoryRemarks, HasMedicalHistory, MedicalHistoryRemarks, DistanceVision_RightEye_Unaided,
											  DistanceVision_RightEye_WithGlasses, DistanceVision_RightEye_PinHole, NearVision_RightEye,
											  NeedCycloRefraction_RightEye, NeedCycloRefractionRemarks_RightEye, DistanceVision_LeftEye_Unaided,
											  DistanceVision_LeftEye_WithGlasses, DistanceVision_LeftEye_PinHole, NearVision_LeftEye,
											  NeedCycloRefraction_LeftEye, NeedCycloRefractionRemarks_LeftEye, Right_Spherical_Status, 
											  Right_Spherical_Points, Right_Cyclinderical_Status, Right_Cyclinderical_Points, Right_Axix_From, Right_Axix_To,
											  Right_Near_Status, Right_Near_Points, Left_Spherical_Status, Left_Spherical_Points, 
											  Left_Cyclinderical_Status, Left_Cyclinderical_Points, Left_Axix_From, Left_Axix_To, Left_Near_Status, Left_Near_Points, 
											  Douchrome, Achromatopsia, RetinoScopy_RightEye,  Condition_RightEye, Meridian1_RightEye, 
											  Meridian2_RightEye, FinalPrescription_RightEye, RetinoScopy_LeftEye,  Condition_LeftEye,
											  Meridian1_LeftEye, Meridian2_LeftEye, FinalPrescription_LeftEye, Hirchberg_Distance, Hirchberg_Near, Ophthalmoscope_RightEye, 
											  PupillaryReactions_RightEye, CoverUncovertTest_RightEye, CoverUncovertTestRemarks_RightEye, ExtraOccularMuscleRemarks_RightEye,
											  Ophthalmoscope_LeftEye, PupillaryReactions_LeftEye, CoverUncovertTest_LeftEye, CoverUncovertTestRemarks_LeftEye,
											  CycloplegicRefraction_RightEye ,	CycloplegicRefraction_LeftEye ,	Conjunctivitis_RightEye ,	Conjunctivitis_LeftEye ,
						                      Scleritis_RightEye , Scleritis_LeftEye ,	Nystagmus_RightEye ,	Nystagmus_LeftEye ,	CornealDefect_RightEye ,CornealDefect_LeftEye ,	Cataract_RightEye ,
											  Cataract_LeftEye ,	Keratoconus_RightEye ,	Keratoconus_LeftEye ,	Ptosis_RightEye ,	Ptosis_LeftEye ,	LowVision_RightEye ,	LowVision_LeftEye ,
											  Pterygium_RightEye ,	Pterygium_LeftEye ,	ColorBlindness_RightEye ,	ColorBlindness_LeftEye ,	Others_RightEye ,	Others_LeftEye ,	Fundoscopy_RightEye ,
											  Fundoscopy_LeftEye ,	Surgery_RightEye ,	Surgery_LeftEye ,	CataractSurgery_RightEye ,	CataractSurgery_LeftEye ,	SurgeryPterygium_RightEye ,
											  SurgeryPterygium_LeftEye ,	SurgeryCornealDefect_RightEye ,	SurgeryCornealDefect_LeftEye ,	SurgeryPtosis_RightEye ,	SurgeryPtosis_LeftEye ,
											  SurgeryKeratoconus_RightEye ,	SurgeryKeratoconus_LeftEye ,	Chalazion_RightEye ,	Chalazion_LeftEye ,	Hordeolum_RightEye ,	Hordeolum_LeftEye ,
											  SurgeryOthers_RightEye ,	SurgeryOthers_LeftEye,RightPupilDefects,LeftPupilDefects,UserId, EntDate,
											  EntOperation, EntTerminal, EntTerminalIP, ExtraOccularMuscleRemarks_LeftEye, ApplicationID, FormId, UserEmpId, UserEmpName, UserEmpCode,
											  VisualAcuity_RightEye, VisualAcuity_LeftEye, LeftSquint_VA ,RightSquint_VA,LeftAmblyopic_VA 	,RightAmblyopic_VA ,
											  RightAmblyopia,LeftAmblyopia,LeftSquint_Surgery,RightSquint_Surgery
											  )

		SELECT  @OptometristWorkerTransDate,@AutoRefWorkerId , @WorkerAutoId, @CompanyAutoId,@HasChiefComplain, @ChiefComplainRemarks, 
			   @HasOccularHistory, @OccularHistoryRemarks, @HasMedicalHistory, @MedicalHistoryRemarks, @DistanceVision_RightEye_Unaided, 
			   @DistanceVision_RightEye_WithGlasses, @DistanceVision_RightEye_PinHole, @NearVision_RightEye, 
			   @NeedCycloRefraction_RightEye, @NeedCycloRefractionRemarks_RightEye, @DistanceVision_LeftEye_Unaided, 
			   @DistanceVision_LeftEye_WithGlasses, @DistanceVision_LeftEye_PinHole, @NearVision_LeftEye, 
			   @NeedCycloRefraction_LeftEye, @NeedCycloRefractionRemarks_LeftEye, @Right_Spherical_Status, 
			   @Right_Spherical_Points, @Right_Cyclinderical_Status, @Right_Cyclinderical_Points, @Right_Axix_From, 
			   @Right_Axix_To, @Right_Near_Status, @Right_Near_Points, @Left_Spherical_Status, @Left_Spherical_Points, 
			   @Left_Cyclinderical_Status, @Left_Cyclinderical_Points, @Left_Axix_From, @Left_Axix_To, @Left_Near_Status, 
			   @Left_Near_Points, @Douchrome, @Achromatopsia, @RetinoScopy_RightEye,  
			   @Condition_RightEye, @Meridian1_RightEye, @Meridian2_RightEye, @FinalPrescription_RightEye, 
			   @RetinoScopy_LeftEye, @Condition_LeftEye, @Meridian1_LeftEye, 
			   @Meridian2_LeftEye, @FinalPrescription_LeftEye, @Hirchberg_Distance, @Hirchberg_Near, @Ophthalmoscope_RightEye, 
			   @PupillaryReactions_RightEye, @CoverUncovertTest_RightEye, @CoverUncovertTestRemarks_RightEye, 
			   @ExtraOccularMuscleRemarks_RightEye, @Ophthalmoscope_LeftEye, @PupillaryReactions_LeftEye, 
			   @CoverUncovertTest_LeftEye, @CoverUncovertTestRemarks_LeftEye, 
			   @CycloplegicRefraction_RightEye ,	@CycloplegicRefraction_LeftEye ,	@Conjunctivitis_RightEye ,	@Conjunctivitis_LeftEye ,
				@Scleritis_RightEye ,@Scleritis_LeftEye ,	@Nystagmus_RightEye ,	@Nystagmus_LeftEye ,	@CornealDefect_RightEye ,	@CornealDefect_LeftEye ,
				@Cataract_RightEye ,	@Cataract_LeftEye ,	@Keratoconus_RightEye ,	@Keratoconus_LeftEye ,	@Ptosis_RightEye ,	@Ptosis_LeftEye ,
				@LowVision_RightEye ,	@LowVision_LeftEye ,	@Pterygium_RightEye ,	@Pterygium_LeftEye ,	@ColorBlindness_RightEye ,	@ColorBlindness_LeftEye ,
				@Others_RightEye ,	@Others_LeftEye ,	@Fundoscopy_RightEye ,	@Fundoscopy_LeftEye ,	@Surgery_RightEye ,	@Surgery_LeftEye ,
				@CataractSurgery_RightEye ,	@CataractSurgery_LeftEye ,	@SurgeryPterygium_RightEye ,	@SurgeryPterygium_LeftEye ,	@SurgeryCornealDefect_RightEye ,
				@SurgeryCornealDefect_LeftEye ,	@SurgeryPtosis_RightEye ,	@SurgeryPtosis_LeftEye ,	@SurgeryKeratoconus_RightEye ,	@SurgeryKeratoconus_LeftEye ,
				@Chalazion_RightEye ,	@Chalazion_LeftEye ,	@Hordeolum_RightEye ,	@Hordeolum_LeftEye ,	@SurgeryOthers_RightEye ,	@SurgeryOthers_LeftEye ,
			   @RightPupilDefects,@LeftPupilDefects,@UserId, GETDATE(), 'INSERT', 
			   @EntryTerminal, @EntryTerminalIP, @ExtraOccularMuscleRemarks_LeftEye, @ApplicationID, @FormId, 
			   @UserEmpId, @UserEmpName, @UserEmpCode, @VisualAcuity_RightEye, @VisualAcuity_LeftEye, @LeftSquint_VA ,@RightSquint_VA,@LeftAmblyopic_VA 	
			   ,@RightAmblyopic_VA ,@RightAmblyopia,@LeftAmblyopia,@LeftSquint_Surgery,@RightSquint_Surgery

			   SET @OptometristWorkerId=SCOPE_IDENTITY()

		SELECT @OptometristWorkerId AS OptometristWorkerId,'Successfully Saved' AS RESULT
	END
	ELSE IF @operation='Update'
	BEGIN
	UPDATE dbo.tblOptometristWorker
	 SET    HasChiefComplain = @HasChiefComplain, ChiefComplainRemarks = @ChiefComplainRemarks, HasOccularHistory = @HasOccularHistory, 
           CompanyAutoId=@CompanyAutoId,OccularHistoryRemarks = @OccularHistoryRemarks, HasMedicalHistory = @HasMedicalHistory, MedicalHistoryRemarks = @MedicalHistoryRemarks, 
           DistanceVision_RightEye_Unaided = @DistanceVision_RightEye_Unaided, DistanceVision_RightEye_WithGlasses = @DistanceVision_RightEye_WithGlasses, 
           DistanceVision_RightEye_PinHole = @DistanceVision_RightEye_PinHole, NearVision_RightEye = @NearVision_RightEye, 
           NeedCycloRefraction_RightEye = @NeedCycloRefraction_RightEye, NeedCycloRefractionRemarks_RightEye = @NeedCycloRefractionRemarks_RightEye, 
           DistanceVision_LeftEye_Unaided = @DistanceVision_LeftEye_Unaided, DistanceVision_LeftEye_WithGlasses = @DistanceVision_LeftEye_WithGlasses, 
           DistanceVision_LeftEye_PinHole = @DistanceVision_LeftEye_PinHole, NearVision_LeftEye = @NearVision_LeftEye, 
           NeedCycloRefraction_LeftEye = @NeedCycloRefraction_LeftEye, NeedCycloRefractionRemarks_LeftEye = @NeedCycloRefractionRemarks_LeftEye, 
           Right_Spherical_Status = @Right_Spherical_Status, Right_Spherical_Points = @Right_Spherical_Points, 
           Right_Cyclinderical_Status = @Right_Cyclinderical_Status, Right_Cyclinderical_Points = @Right_Cyclinderical_Points, 
           Right_Axix_From = @Right_Axix_From, Right_Axix_To = @Right_Axix_To, Right_Near_Status = @Right_Near_Status, 
           Right_Near_Points = @Right_Near_Points, Left_Spherical_Status = @Left_Spherical_Status, Left_Spherical_Points = @Left_Spherical_Points, 
           Left_Cyclinderical_Status = @Left_Cyclinderical_Status, Left_Cyclinderical_Points = @Left_Cyclinderical_Points, 
           Left_Axix_From = @Left_Axix_From, Left_Axix_To = @Left_Axix_To, Left_Near_Status = @Left_Near_Status, 
           Left_Near_Points = @Left_Near_Points, Douchrome = @Douchrome, Achromatopsia = @Achromatopsia, 
           RetinoScopy_RightEye = @RetinoScopy_RightEye, CycloplegicRefraction_RightEye = @CycloplegicRefraction_RightEye, 
           Condition_RightEye = @Condition_RightEye, Meridian1_RightEye = @Meridian1_RightEye, Meridian2_RightEye = @Meridian2_RightEye, 
           FinalPrescription_RightEye = @FinalPrescription_RightEye, RetinoScopy_LeftEye = @RetinoScopy_LeftEye, 
            Condition_LeftEye = @Condition_LeftEye, 
           Meridian1_LeftEye = @Meridian1_LeftEye, Meridian2_LeftEye = @Meridian2_LeftEye, FinalPrescription_LeftEye = @FinalPrescription_LeftEye, 
           Hirchberg_Distance = @Hirchberg_Distance, Hirchberg_Near = @Hirchberg_Near, Ophthalmoscope_RightEye = @Ophthalmoscope_RightEye, 
           PupillaryReactions_RightEye = @PupillaryReactions_RightEye, CoverUncovertTest_RightEye = @CoverUncovertTest_RightEye, 
           CoverUncovertTestRemarks_RightEye = @CoverUncovertTestRemarks_RightEye, ExtraOccularMuscleRemarks_RightEye = @ExtraOccularMuscleRemarks_RightEye, 
           Ophthalmoscope_LeftEye = @Ophthalmoscope_LeftEye, PupillaryReactions_LeftEye = @PupillaryReactions_LeftEye, 
           CoverUncovertTest_LeftEye = @CoverUncovertTest_LeftEye, CoverUncovertTestRemarks_LeftEye = @CoverUncovertTestRemarks_LeftEye,
      CycloplegicRefraction_LeftEye = @CycloplegicRefraction_LeftEye	 
      ,Conjunctivitis_RightEye =		@Conjunctivitis_RightEye
      ,Conjunctivitis_LeftEye =		@Conjunctivitis_LeftEye
      ,Scleritis_RightEye =			@Scleritis_RightEye
      ,Scleritis_LeftEye =			@Scleritis_LeftEye
      ,Nystagmus_RightEye =			@Nystagmus_RightEye
      ,Nystagmus_LeftEye =			@Nystagmus_LeftEye
      ,CornealDefect_RightEye =		@CornealDefect_RightEye
      ,CornealDefect_LeftEye =		@CornealDefect_LeftEye
      ,Cataract_RightEye =			@Cataract_RightEye
      ,Cataract_LeftEye = 			@Cataract_LeftEye
      ,Keratoconus_RightEye =			@Keratoconus_RightEye
      ,Keratoconus_LeftEye = 			@Keratoconus_LeftEye
      ,Ptosis_RightEye =				@Ptosis_RightEye
      ,Ptosis_LeftEye =				@Ptosis_LeftEye
      ,LowVision_RightEye =			@LowVision_RightEye
      ,LowVision_LeftEye =			@LowVision_LeftEye
      ,Pterygium_RightEye =			@Pterygium_RightEye
      ,Pterygium_LeftEye =			@Pterygium_LeftEye
      ,ColorBlindness_RightEye =		@ColorBlindness_RightEye
      ,ColorBlindness_LeftEye =		@ColorBlindness_LeftEye
      ,Others_RightEye =				@Others_RightEye
	  ,RightPupilDefects =				@RightPupilDefects,
	  LeftPupilDefects  =				@LeftPupilDefects
      ,Others_LeftEye =				@Others_LeftEye
      ,Fundoscopy_RightEye =			@Fundoscopy_RightEye
      ,Fundoscopy_LeftEye = 			@Fundoscopy_LeftEye
      ,Surgery_RightEye =				@Surgery_RightEye
      ,Surgery_LeftEye =				@Surgery_LeftEye
      ,CataractSurgery_RightEye =		@CataractSurgery_RightEye
      ,CataractSurgery_LeftEye =		@CataractSurgery_LeftEye
      ,SurgeryPterygium_RightEye =	@SurgeryPterygium_RightEye
      ,SurgeryPterygium_LeftEye =		@SurgeryPterygium_LeftEye
      ,SurgeryCornealDefect_RightEye =@SurgeryCornealDefect_RightEye
      ,SurgeryCornealDefect_LeftEye = @SurgeryCornealDefect_LeftEye
      ,SurgeryPtosis_RightEye =		@SurgeryPtosis_RightEye
      ,SurgeryPtosis_LeftEye =		@SurgeryPtosis_LeftEye
      ,SurgeryKeratoconus_RightEye =	@SurgeryKeratoconus_RightEye
      ,SurgeryKeratoconus_LeftEye =	@SurgeryKeratoconus_LeftEye
      ,Chalazion_RightEye =			@Chalazion_RightEye
      ,Chalazion_LeftEye =			@Chalazion_LeftEye
      ,Hordeolum_RightEye =			@Hordeolum_RightEye
      ,Hordeolum_LeftEye =			@Hordeolum_LeftEye
	  ,LeftAmblyopia =				@LeftAmblyopia
	  ,RightAmblyopia =				@RightAmblyopia
      ,SurgeryOthers_RightEye =		@SurgeryOthers_RightEye
      ,SurgeryOthers_LeftEye =		@SurgeryOthers_LeftEye,
	  LeftSquint_Surgery	=		@LeftSquint_Surgery,
	  RightSquint_Surgery	=		@RightSquint_Surgery,
           UserId = @UserId ,ExtraOccularMuscleRemarks_LeftEye = @ExtraOccularMuscleRemarks_LeftEye, 
           ApplicationID = @ApplicationID, FormId = @FormId, UserEmpId = @UserEmpId, UserEmpName = @UserEmpName, 
           UserEmpCode = @UserEmpCode, VisualAcuity_RightEye = @VisualAcuity_RightEye, VisualAcuity_LeftEye = @VisualAcuity_LeftEye, 
           LeftSquint_VA =@LeftSquint_VA ,RightSquint_VA=@RightSquint_VA,LeftAmblyopic_VA =@LeftAmblyopic_VA,RightAmblyopic_VA =@RightAmblyopic_VA,
		   EntOperation='UPDATE'
		WHERE  OptometristWorkerId = @OptometristWorkerId 

			SELECT @OptometristWorkerId  AS OptometristWorkerId,'Successfully Updated' AS RESULT

	END

	ELSE IF @operation='GetById'
	BEGIN
	    
SELECT ISNULL(OptometristWorkerId,'0')OptometristWorkerId
      ,ISNULL(OptometristWorkerTransDate,GETDATE())OptometristWorkerTransDate
      ,ISNULL(WorkerAutoId,'0')WorkerAutoId
      ,ISNULL(CompanyAutoId,'0')CompanyAutoId
      ,ISNULL(HasChiefComplain,'0')HasChiefComplain
      ,ISNULL(ChiefComplainRemarks,'')ChiefComplainRemarks
      ,ISNULL(HasOccularHistory,'0')HasOccularHistory
      ,ISNULL(OccularHistoryRemarks,'')OccularHistoryRemarks
      ,ISNULL(HasMedicalHistory,'0')HasMedicalHistory
      ,ISNULL(MedicalHistoryRemarks,'')MedicalHistoryRemarks
      ,ISNULL(DistanceVision_RightEye_Unaided,'0')DistanceVision_RightEye_Unaided
      ,ISNULL(DistanceVision_RightEye_WithGlasses,'0')DistanceVision_RightEye_WithGlasses
      ,ISNULL(DistanceVision_RightEye_PinHole,'0')DistanceVision_RightEye_PinHole
      ,ISNULL(NearVision_RightEye,'0')NearVision_RightEye
      ,ISNULL(NeedCycloRefraction_RightEye,'0')NeedCycloRefraction_RightEye
      ,ISNULL(NeedCycloRefractionRemarks_RightEye,'')NeedCycloRefractionRemarks_RightEye
      ,ISNULL(DistanceVision_LeftEye_Unaided,'0')DistanceVision_LeftEye_Unaided
      ,ISNULL(DistanceVision_LeftEye_WithGlasses,'0')DistanceVision_LeftEye_WithGlasses
      ,ISNULL(DistanceVision_LeftEye_PinHole,'0')DistanceVision_LeftEye_PinHole
      ,ISNULL(NearVision_LeftEye,'0')NearVision_LeftEye
      ,ISNULL(NeedCycloRefraction_LeftEye,'0')NeedCycloRefraction_LeftEye
      ,ISNULL(NeedCycloRefractionRemarks_LeftEye,'')NeedCycloRefractionRemarks_LeftEye
      ,ISNULL(Right_Spherical_Status,'0')Right_Spherical_Status
      ,ISNULL(Right_Spherical_Points,'0')Right_Spherical_Points
      ,ISNULL(Right_Cyclinderical_Status,'0')Right_Cyclinderical_Status
      ,ISNULL(Right_Cyclinderical_Points,'0')Right_Cyclinderical_Points
      ,ISNULL(Right_Axix_From,'0')Right_Axix_From
      ,ISNULL(Right_Axix_To,'0')Right_Axix_To
      ,ISNULL(Right_Near_Status,'P')Right_Near_Status
      ,ISNULL(Right_Near_Points,'0')Right_Near_Points
      ,ISNULL(Left_Spherical_Status,'P')Left_Spherical_Status
      ,ISNULL(Left_Spherical_Points,'0')Left_Spherical_Points
      ,ISNULL(Left_Cyclinderical_Status,'P')Left_Cyclinderical_Status
      ,ISNULL(Left_Cyclinderical_Points,'0')Left_Cyclinderical_Points
      ,ISNULL(Left_Axix_From,'0')Left_Axix_From
      ,ISNULL(Left_Axix_To,'0')Left_Axix_To
      ,ISNULL(Left_Near_Status,'')Left_Near_Status
      ,ISNULL(Left_Near_Points,'0')Left_Near_Points
      ,ISNULL(VisualAcuity_RightEye,'0')VisualAcuity_RightEye
      ,ISNULL(VisualAcuity_LeftEye,'0')VisualAcuity_LeftEye
      ,ISNULL(LeftSquint_VA,'0')LeftSquint_VA
      ,ISNULL(RightSquint_VA,'0')RightSquint_VA
      ,ISNULL(LeftAmblyopic_VA,'0')LeftAmblyopic_VA
      ,ISNULL(RightAmblyopic_VA,'0')RightAmblyopic_VA
      ,ISNULL(AutoRefWorkerId,'0')AutoRefWorkerId
      ,ISNULL(Hirchberg_Distance,'0')Hirchberg_Distance
      ,ISNULL(Hirchberg_Near,'0')Hirchberg_Near
      ,ISNULL(Ophthalmoscope_RightEye,'0')Ophthalmoscope_RightEye
      ,ISNULL(Ophthalmoscope_LeftEye,'0')Ophthalmoscope_LeftEye
      ,ISNULL(PupillaryReactions_RightEye,'0')PupillaryReactions_RightEye
      ,ISNULL(CoverUncovertTest_RightEye,'0')CoverUncovertTest_RightEye
      ,ISNULL(CoverUncovertTestRemarks_RightEye,'')CoverUncovertTestRemarks_RightEye
      ,ISNULL(ExtraOccularMuscleRemarks_RightEye,'')ExtraOccularMuscleRemarks_RightEye
      ,ISNULL(PupillaryReactions_LeftEye,'0')PupillaryReactions_LeftEye
      ,ISNULL(CoverUncovertTest_LeftEye,'0')CoverUncovertTest_LeftEye
      ,ISNULL(CoverUncovertTestRemarks_LeftEye,'0')CoverUncovertTestRemarks_LeftEye
      ,ISNULL(CycloplegicRefraction_RightEye,'0')CycloplegicRefraction_RightEye
      ,ISNULL(CycloplegicRefraction_LeftEye,'0')CycloplegicRefraction_LeftEye
      ,ISNULL(Conjunctivitis_RightEye,'0')Conjunctivitis_RightEye
      ,ISNULL(Conjunctivitis_LeftEye,'0')Conjunctivitis_LeftEye
      ,ISNULL(Scleritis_RightEye,'0')Scleritis_RightEye
      ,ISNULL(Scleritis_LeftEye,'0')Scleritis_LeftEye
      ,ISNULL(Nystagmus_RightEye,'0')Nystagmus_RightEye
      ,ISNULL(Nystagmus_LeftEye,'0')Nystagmus_LeftEye
      ,ISNULL(CornealDefect_RightEye,'0')CornealDefect_RightEye
      ,ISNULL(CornealDefect_LeftEye,'0')CornealDefect_LeftEye
      ,ISNULL(Cataract_RightEye,'0')Cataract_RightEye
      ,ISNULL(Cataract_LeftEye,'0')Cataract_LeftEye
      ,ISNULL(Keratoconus_RightEye,'0')Keratoconus_RightEye
      ,ISNULL(Keratoconus_LeftEye,'0')Keratoconus_LeftEye
      ,ISNULL(Ptosis_RightEye,'0')Ptosis_RightEye
      ,ISNULL(Ptosis_LeftEye,'0')Ptosis_LeftEye
      ,ISNULL(LowVision_RightEye,'0')LowVision_RightEye
      ,ISNULL(LowVision_LeftEye,'0')LowVision_LeftEye
      ,ISNULL(Pterygium_RightEye,'0')Pterygium_RightEye
      ,ISNULL(Pterygium_LeftEye,'0')Pterygium_LeftEye
      ,ISNULL(ColorBlindness_RightEye,'0')ColorBlindness_RightEye
      ,ISNULL(ColorBlindness_LeftEye,'0')ColorBlindness_LeftEye
      ,ISNULL(Others_RightEye,'0')Others_RightEye
      ,ISNULL(Others_LeftEye,'0')Others_LeftEye
      ,ISNULL(Fundoscopy_RightEye,'0')Fundoscopy_RightEye
      ,ISNULL(Fundoscopy_LeftEye,'0')Fundoscopy_LeftEye
      ,ISNULL(Surgery_RightEye,'0')Surgery_RightEye
      ,ISNULL(Surgery_LeftEye,'0')Surgery_LeftEye
      ,ISNULL(CataractSurgery_RightEye,'0')CataractSurgery_RightEye
      ,ISNULL(CataractSurgery_LeftEye,'0')CataractSurgery_LeftEye
      ,ISNULL(SurgeryPterygium_RightEye,'0')SurgeryPterygium_RightEye
      ,ISNULL(SurgeryPterygium_LeftEye,'0')SurgeryPterygium_LeftEye
      ,ISNULL(SurgeryCornealDefect_RightEye,'0')SurgeryCornealDefect_RightEye
      ,ISNULL(SurgeryCornealDefect_LeftEye,'0')SurgeryCornealDefect_LeftEye
      ,ISNULL(SurgeryPtosis_RightEye,'0')SurgeryPtosis_RightEye
      ,ISNULL(SurgeryPtosis_LeftEye,'0')SurgeryPtosis_LeftEye
      ,ISNULL(SurgeryKeratoconus_RightEye,'0')SurgeryKeratoconus_RightEye
      ,ISNULL(SurgeryKeratoconus_LeftEye,'0')SurgeryKeratoconus_LeftEye
      ,ISNULL(Chalazion_RightEye,'0')Chalazion_RightEye
      ,ISNULL(Chalazion_LeftEye,'0')Chalazion_LeftEye
      ,ISNULL(Hordeolum_RightEye,'0')Hordeolum_RightEye
      ,ISNULL(Hordeolum_LeftEye,'0')Hordeolum_LeftEye
      ,ISNULL(SurgeryOthers_RightEye,'0')SurgeryOthers_RightEye
      ,ISNULL(SurgeryOthers_LeftEye,'0')SurgeryOthers_LeftEye
      ,ISNULL(Douchrome,'0')Douchrome
      ,ISNULL(Achromatopsia,'0')Achromatopsia
      ,ISNULL(RetinoScopy_RightEye,'0')RetinoScopy_RightEye
      ,ISNULL(Condition_RightEye,'0')Condition_RightEye
      ,ISNULL(Meridian1_RightEye,'')Meridian1_RightEye
      ,ISNULL(Meridian2_RightEye,'')Meridian2_RightEye
      ,ISNULL(FinalPrescription_RightEye,'')FinalPrescription_RightEye
      ,ISNULL(RetinoScopy_LeftEye,'0')RetinoScopy_LeftEye
      ,ISNULL(Condition_LeftEye,'')Condition_LeftEye
      ,ISNULL(Meridian1_LeftEye,'')Meridian1_LeftEye
      ,ISNULL(Meridian2_LeftEye,'')Meridian2_LeftEye
      ,ISNULL(FinalPrescription_LeftEye,'')FinalPrescription_LeftEye
	  ,ISNULL(RightPupilDefects,0)RightPupilDefects
	  ,ISNULL(LeftPupilDefects,0)LeftPupilDefects
	  ,ISNULL(LeftSquint_Surgery,0)LeftSquint_Surgery
	  ,ISNULL(RightSquint_Surgery,0)RightSquint_Surgery
      ,ISNULL(UserId,'0')UserId
      ,ISNULL(EntDate,Getdate())
      ,ISNULL(EntOperation,'0')
      ,ISNULL(EntTerminal,'0')
      ,ISNULL(EntTerminalIP,'0')
      ,ISNULL(ExtraOccularMuscleRemarks_LeftEye,'')ExtraOccularMuscleRemarks_LeftEye,
	  ISNULL(RightAmblyopia,0)RightAmblyopia,ISNULL(LeftAmblyopia,0)LeftAmblyopia
      ,ISNULL(ApplicationID,'0')
      ,ISNULL(FormId,'0')
      ,ISNULL(UserEmpId,'0')
      ,ISNULL(UserEmpName,'0')
      ,ISNULL(UserEmpCode,'0')
    FROM   dbo.tblOptometristWorker
    WHERE  OptometristWorkerId = @OptometristWorkerId 
	END

	ELSE IF @operation = 'GetListForWokrerOptometrist'
	BEGIN
	SELECT DISTINCT TOP 10 cw.WorkerAutoId,cw.WorkerCode,cw.WorkerName,c.CompanyName,cw.CNIC,cw.MobileNo
	FROM tblCompany c
	INNER JOIN tblCompanyWorker cw ON c.CompanyAutoId=cw.CompanyAutoId
	INNER JOIN tblAutoRefTestWorker artw ON cw.WorkerAutoId=artw.WorkerAutoId
	ORDER BY cw.WorkerAutoId desc
	END
  
  ELSE IF @operation = 'Search'
	BEGIN
	SELECT DISTINCT TOP 10 c.WorkerAutoId,c.WorkerCode,c.WorkerName,A.CompanyName,c.CNIC,c.MobileNo
		   FROM tblCompany A INNER JOIN tblCompanyWorker c ON A.CompanyAutoId = c.CompanyAutoId
		   INNER JOIN tblAutoRefTestWorker artw ON c.WorkerAutoId=artw.WorkerAutoId
		 WHERE
		 (
		 CAST(c.WorkerCode AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR
		 CAST(A.CompanyName AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR
		 CAST(c.WorkerName AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR
		 CAST(c.CNIC AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR
		 CAST(c.MobileNo AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'    
		 )
		 ORDER BY c.WorkerAutoId desc
	END

	
ELSE IF @operation='NewWorkerForOpt'
BEGIN
	SELECT DISTINCT TOP 5 cw.WorkerAutoId Id,cw.WorkerName+' | '+cw.WorkerCode text,cw.WorkerCode Code FROM tblCompanyWorker cw
	WHERE 
	cw.CompanyAutoId=@CompanyAutoId AND 
	cw.WorkerAutoId NOT IN (SELECT artw.WorkerAutoId FROM tblOptometristWorker artw WITH (NOLOCK) WHERE CAST(artw.OptometristWorkerTransDate AS DATE) = CAST(@OptometristWorkerTransDate AS DATE))
	AND  (cw.WorkerName LIKE '%'+ISNULL('','')+ '%' OR
		  cw.WorkerCode LIKE '%'+ISNULL('','')+ '%' )
	ORDER BY cw.WorkerName+' | '+cw.WorkerCode  
END

ELSE IF @operation='EditWorkerForOpt'
BEGIN
	SELECT DISTINCT TOP 5 cw.WorkerAutoId Id,cw.WorkerName+' | '+cw.WorkerCode text,cw.WorkerCode Code 
	FROM tblCompanyWorker cw INNER JOIN tblOptometristWorker artw ON cw.WorkerAutoId=artw.WorkerAutoId
	WHERE 
	cw.CompanyAutoId=@CompanyAutoId 
	AND  (cw.WorkerName LIKE '%'+ISNULL('','')+ '%' OR
		  cw.WorkerCode LIKE '%'+ISNULL('','')+ '%' )
	ORDER BY cw.WorkerName+' | '+cw.WorkerCode
END

	ELSE IF @operation= 'GetDatesofWorker'
	BEGIN
		SELECT CAST(FORMAT(artw.OptometristWorkerTransDate,'dd | MMM | yyyy') AS VARCHAR) Text,
		artw.OptometristWorkerId AS Id
		FROM tblOptometristWorker artw
		WHERE artw.WorkerAutoId=@WorkerAutoId
		ORDER BY artw.OptometristWorkerId desc
	END

	ELSE IF @operation= 'DeleteOptometristById'
	BEGIN
		DELETE FROM tblOptometristWorker WHERE OptometristWorkerId=@OptometristWorkerId
		SELECT @OptometristWorkerId AS OptometristWorkerId,'Successfully Deleted' AS RESULT
	END


	 
GO


 
     
CREATE OR ALTER PROC [dbo].[Sp_GlassDespenseWorker]    
    @GlassDespenseWorkerId INT = NULL,    
	@OptometristWorkerId INT =NULL,
    @GlassDespenseWorkerTransDate DATETIME   = NULL,    
    @WorkerAutoId INT   = NULL,    
    @VisionwithGlasses_RightEye INT= NULL,    
    @VisionwithGlasses_LeftEye int= NULL,    
 @NearVA_RightEye INT=NULL,  
 @NearVA_LeftEye INT=NULL,  
    @WorkerSatisficaion int= NULL,    
    @Unsatisfied int= NULL,    
    @Unsatisfied_Remarks nvarchar(250)= NULL,    
    @Unsatisfied_Reason int= NULL,    
	@CompanyAutoId INT =NULL,  
    @UserId nvarchar(250)= NULL,    
    @EntDate datetime= NULL,    
    @EntOperation nvarchar(100)= NULL,    
    @EntryTerminal  nvarchar(200)= NULL,    
    @EntryTerminalIP nvarchar(200) =NULL,    
 @UserEmpName NVARCHAR(200)=NULL,  
 @SearchText NVARCHAR(200)=NULL,  
 @Operation VARCHAR(50)=NULL    
AS     
 IF @operation='Save'    
BEGIN    
IF NOT EXISTS(SELECT 1 FROM tblGlassDespenseWorker artw WHERE artw.WorkerAutoId=@WorkerAutoId AND CAST(artw.GlassDespenseWorkerTransDate AS DATE)=CAST(@GlassDespenseWorkerTransDate AS date))    
 BEGIN    
    
INSERT INTO [tblGlassDespenseWorker] (
									  OptometristWorkerId,GlassDespenseWorkerTransDate, WorkerAutoId, 
									  VisionwithGlasses_RightEye, VisionwithGlasses_LeftEye,NearVA_RightEye, NearVA_LeftEye,
									  WorkerSatisficaion, Unsatisfied, Unsatisfied_Remarks, Unsatisfied_Reason,
									  UserId, EntDate, EntOperation, EntTerminal, EntTerminalIP)
	SELECT @OptometristWorkerId,@GlassDespenseWorkerTransDate, @WorkerAutoId, @VisionwithGlasses_RightEye,     
			@VisionwithGlasses_LeftEye, @NearVA_RightEye,@NearVA_LeftEye,@WorkerSatisficaion, 
			@Unsatisfied, @Unsatisfied_Remarks, @Unsatisfied_Reason,        
			@UserId, GETDATE(), @EntOperation, @EntryTerminal ,     
			@EntryTerminalIP    
    
      SET @GlassDespenseWorkerId = SCOPE_IDENTITY()    
     SELECT @GlassDespenseWorkerId  AS GlassDespenseWorkerId,'Successfully Saved' AS RESULT    
 END    
 ELSE    
    SELECT @GlassDespenseWorkerId  AS GlassDespenseWorkerId,'Worker Glass Despense Already Exists in selected Date' AS RESULT    
      
END    
ELSE IF @operation='UPDATE'    
BEGIN    
	  UPDATE dbo.tblGlassDespenseWorker    
		SET    WorkerAutoId = @WorkerAutoId,     
			   VisionwithGlasses_RightEye = @VisionwithGlasses_RightEye, VisionwithGlasses_LeftEye = @VisionwithGlasses_LeftEye,   
				NearVA_RightEye=@NearVA_RightEye,NearVA_LeftEye=@NearVA_LeftEye,  
			   WorkerSatisficaion = @WorkerSatisficaion, Unsatisfied = @Unsatisfied, Unsatisfied_Remarks = @Unsatisfied_Remarks,     
			   Unsatisfied_Reason = @Unsatisfied_Reason,
			   UserId = @UserId,  EntOperation = @EntOperation, EntTerminal = @EntryTerminal ,     
			   EntTerminalIP = @EntryTerminalIP    
    WHERE  GlassDespenseWorkerId = @GlassDespenseWorkerId    
	 SELECT @GlassDespenseWorkerId AS GlassDespenseWorkerId,'Successfully Updated' AS RESULT   
END    
    
ELSE IF @operation='GetById'    
BEGIN    
    
    SELECT a.OptometristWorkerId,
		   GlassDespenseWorkerId,GlassDespenseWorkerTransDate, a.WorkerAutoId, 
		   VisionwithGlasses_RightEye, VisionwithGlasses_LeftEye,   
			NearVA_RightEye,NearVA_LeftEye,  
           WorkerSatisficaion, Unsatisfied, Unsatisfied_Remarks, Unsatisfied_Reason,
		   a.UserId, a.EntDate, a.EntOperation, a.EntTerminal, a.EntTerminalIP,    
     cw.Distance,cw.Near,cw.WearGlasses    
    FROM   dbo.tblGlassDespenseWorker a WITH (NOLOCK)    
 INNER JOIN tblCompanyWorker cw ON a.WorkerAutoId = cw.WorkerAutoId    
    
    WHERE  GlassDespenseWorkerId = @GlassDespenseWorkerId    
END    
    
ELSE IF @operation='DeleteById'    
BEGIN    
 DELETE FROM tblGlassDespenseWorker WHERE GlassDespenseWorkerId=@GlassDespenseWorkerId    
  SELECT @GlassDespenseWorkerId  AS GlassDespenseWorkerId,'Successfully Deleted' AS RESULT    
END     
  
  
ELSE IF @Operation ='GetDatesofGlassDespenseWorker'    
BEGIN    
 SELECT CAST(FORMAT(artw.GlassDespenseWorkerTransDate,'dd | MMM | yyyy') AS VARCHAR) Text,    
   artw.GlassDespenseWorkerId AS Id    
   FROM tblGlassDespenseWorker artw    
   WHERE artw.WorkerAutoId=@WorkerAutoId    
   ORDER BY artw.GlassDespenseWorkerId desc    
END    
  
  
ELSE IF @operation='NewWorkerForGlassDispense'    
BEGIN    
 SELECT DISTINCT top 5 cw.WorkerAutoId Id, cw.WorkerName +' | '+cw.WorkerCode  text,cw.WorkerCode Code FROM tblCompanyWorker cw    
 INNER JOIN tblOptometristWorker ow ON cw.WorkerAutoId = ow.WorkerAutoId AND ow.OptometristWorkerTransDate <= CAST(@GlassDespenseWorkerTransDate AS DATE)
 WHERE     
 cw.CompanyAutoId=@CompanyAutoId AND     
 cw.WorkerAutoId NOT IN   
   (SELECT artw.WorkerAutoId FROM tblGlassDespenseWorker  artw WITH (NOLOCK) WHERE CAST(artw.GlassDespenseWorkerTransDate AS DATE) = CAST(@GlassDespenseWorkerTransDate AS DATE))    
 AND     
 (cw.WorkerCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR    
 cw.WorkerName LIKE '%'+ISNULL(@SearchText,'')+ '%' )    
 ORDER BY  cw.WorkerName +' | '+cw.WorkerCode     
END    
    
ELSE IF @operation='EditWorkerForGlassDispense'    
BEGIN    
 SELECT DISTINCT TOP 5 cw.WorkerAutoId Id, cw.WorkerName +' | '+cw.WorkerCode  text,cw.WorkerCode Code     
 FROM tblCompanyWorker cw INNER JOIN tblGlassDespenseWorker artw ON cw.WorkerAutoId=artw.WorkerAutoId    
 WHERE     
 cw.CompanyAutoId=@CompanyAutoId     
 AND (cw.WorkerCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR    
 cw.WorkerName LIKE '%'+ISNULL(@SearchText,'')+ '%' )    
 ORDER BY  cw.WorkerName +' | '+cw.WorkerCode     
END    

ELSE IF @operation='GetGlassDispenseHistoryByWorkerId '
	BEGIN
		SELECT TOP 1 artw.OptometristWorkerId,CAST(FORMAT(artw.OptometristWorkerTransDate,'dd | MMM | yyyy') AS VARCHAR) AS Last_Visit_Date,
		 CASE WHEN artw.Right_Spherical_Status ='P' AND artw.Right_Spherical_Points>0 THEN '+' +CAST(Right_Spherical_Points AS VARCHAR)
		 WHEN artw.Right_Spherical_Status='N'AND artw.Right_Spherical_Points>0 THEN '-' +CAST(Right_Spherical_Points AS VARCHAR)
		 WHEN artw.Right_Spherical_Status='E' THEN 'Error '+CAST(ISNULL(Right_Spherical_Points,'') AS VARCHAR)
		 WHEN artw.Right_Spherical_Status='l' THEN 'Plano '+CAST(ISNULL(Right_Spherical_Points,'') AS VARCHAR)
		 ELSE CAST(Right_Spherical_Points AS VARCHAR) END 'Right Spherical',

		 CASE WHEN artw.Left_Spherical_Status ='P' AND artw.Left_Spherical_Points>0 THEN '+' +CAST(Left_Spherical_Points AS VARCHAR)
		 WHEN artw.Left_Spherical_Status='N'AND artw.Left_Spherical_Points>0 THEN '-' +CAST(Left_Spherical_Points AS VARCHAR)
		  WHEN artw.Left_Spherical_Status='E' THEN 'Error '+CAST(ISNULL(Left_Spherical_Points,'') AS VARCHAR)
		 WHEN artw.Left_Spherical_Status='l' THEN 'Plano '+CAST(ISNULL(Left_Spherical_Points,'') AS VARCHAR)
		 ELSE CAST(Left_Spherical_Points AS VARCHAR) END 'Left Spherical',

		 CASE WHEN artw.Right_Spherical_Status ='P' AND artw.Left_Spherical_Points>0 THEN '+' +CAST(Left_Spherical_Points AS VARCHAR)
		 WHEN artw.Right_Spherical_Status='N'AND artw.Right_Spherical_Points>0 THEN '-' +CAST(Right_Spherical_Points AS VARCHAR)
		 ELSE CAST(Right_Spherical_Points AS VARCHAR) END 'Right Spherical',

		 CASE WHEN artw.Right_Cyclinderical_Status ='P' AND artw.Right_Cyclinderical_Points>0 THEN '+' +CAST(Right_Cyclinderical_Points AS VARCHAR)
		 WHEN artw.Right_Cyclinderical_Status='N'AND artw.Right_Cyclinderical_Points>0 THEN '-' +CAST(Right_Cyclinderical_Points AS VARCHAR)
		 ELSE CAST(Right_Cyclinderical_Points AS VARCHAR) END 'Right Cyclinderical', 

		 
		 CASE WHEN artw.Left_Cyclinderical_Status ='P' AND artw.Left_Cyclinderical_Points>0 THEN '+' +CAST(Left_Cyclinderical_Points AS VARCHAR)
		 WHEN artw.Left_Cyclinderical_Status='N'AND artw.Left_Cyclinderical_Points>0 THEN '-' +CAST(Left_Cyclinderical_Points AS VARCHAR)
		 ELSE CAST(artw.Left_Cyclinderical_Points AS VARCHAR) END 'Left Cyclinderical', 
		 artw.Right_Axix_From 'Right Axis' ,
		  artw.Left_Axix_From 'Left Axis' ,ISNULL(cw.WearGlasses,0)WearGlasses,ISNULL(cw.Near,0)Near,
		  ISNULL(cw.Distance,0) Distance,  ISNULL(artw.IPD,0)IPD,CASE WHEN cw.GenderAutoId=1 THEN 'Male' ELSE 'Female' END Gender,
		  cw.Age

		FROM tblOptometristWorker  artw
		INNER JOIN tblCompanyWorker cw ON artw.WorkerAutoId = cw.WorkerAutoId
		WHERE  artw.WorkerAutoId=@WorkerAutoId AND
		artw.OptometristWorkerTransDate <= CAST(@GlassDespenseWorkerTransDate AS DATE) 
		ORDER BY artw.OptometristWorkerId desc
	END
    
    /*    
    -- Begin Return row code block    
    
    SELECT GlassDespenseWorkerTransDate, WorkerAutoId, VisionwithGlasses_RightEye, VisionwithGlasses_LeftEye,     
           WorkerSatisficaion, Unsatisfied, Unsatisfied_Remarks, Unsatisfied_Reason, Right_Spherical_Status,     
           Right_Spherical_Points, Right_Cyclinderical_Status, Right_Cyclinderical_Points, Right_Axix_From,     
           Right_Axix_To, Right_Near_Status, Right_Near_Points, Left_Spherical_Status, Left_Spherical_Points,     
           Left_Cyclinderical_Status, Left_Cyclinderical_Points, Left_Axix_From, Left_Axix_To, Left_Near_Status,     
           Left_Near_Points, FollowupRequired, TreatmentId, Medicines, Prescription, ProvideGlasses,     
           ReferToHospital, UserId, EntDate, EntOperation, EntTerminal, EntTerminalIP    
    FROM   dbo.tblGlassDespenseWorker    
    WHERE  GlassDespenseWorkerId = @GlassDespenseWorkerId    
    
    -- End Return row code block    
    
    */    
GO

 
        
CREATE OR ALTER PROC [dbo].[Sp_SetupGoths]       
    @GothAutoId INT       =NULL ,      
    @GothCode nvarchar(10) =NULL,      
    @GothName nvarchar(250) =NULL,      
 @Website nvarchar(250) =null,      
    @Address1 nvarchar(1000) =NULL,      
    @Address2 nvarchar(1000) =NULL,      
 @Address3 nvarchar(1000) = NULL,      
    @District nvarchar(1000) =NULL,      
 @Town nvarchar(200) =NULL,      
    @City nvarchar(100) =NULL,      
 @WorkForce INT = null,      
    @NameofPerson varchar(100) =NULL,      
    @PersonMobile varchar(50) =NULL,      
    @PersonRole varchar(100) =NULL,      
 @TitleAutoId INT =NULL,      
    @UserId nvarchar(250) =NULL,      
    @EntDate DATETIME =NULL,      
    @EntOperation nvarchar(100) =NULL,      
    @FormId nvarchar(250) =NULL,      
    @UserEmpId INT =NULL,      
    @UserEmpName nvarchar(250) =NULL,      
    @UserEmpCode nvarchar(10) =NULL,      
    @EnrollmentDate DATETIME =NULL,      
 @operation VARCHAR(50)= NULL,      
 @EntryTerminal VARCHAR(200)=NULL,      
 @EntryTerminalIP VARCHAR(200)=NULL,      
 @SearchText VARCHAR(MAX)=NULL      
AS       
    IF @operation = 'Save'      
 Begin      
 IF NOT EXISTS(SELECT 1 FROM tblGoths  l WHERE l.GothName=@GothName AND l.City=@City AND l.Address1=@Address1 )      
 BEGIN      
          
    INSERT INTO tblGoths (GothCode, GothName, Website, Address1, Address2, Address3, Town, District, City,      
          NameofPerson, PersonMobile, PersonRole, TitleAutoId, UserId, EntDate,       
          EntOperation, EntTerminal, EntTerminalIP,      
          FormId, UserEmpId, UserEmpName, UserEmpCode, EnrollmentDate)      
              
      
    SELECT '',UPPER(@GothName),@Website, @Address1, @Address2,@Address3,@Town, @District, @City, @NameofPerson,       
 @PersonMobile, @PersonRole,@TitleAutoId, @UserId, GETDATE(), 'INSERT', @EntryTerminal, @EntryTerminalIP,       
           @FormId, @UserEmpId, @UserEmpName, @UserEmpCode, ISNULL(@EnrollmentDate,GETDATE())      
           
     SET @GothAutoId = SCOPE_IDENTITY()      
     SELECT @GothCode =MAX(CAST(cd.GothCode AS INT))+1 FROM tblGoths  cd      
      
   UPDATE tblGoths SET GothCode=CASE WHEN LEN(@GothCode)=1 THEN '00'+@GothCode      
   WHEN LEN(@GothCode )=2 THEN '0'+@GothCode      
   ELSE @GothCode END      
   WHERE GothAutoId=@GothAutoId      
      
   SELECT @GothAutoId AS GothAutoId,'Successfully Saved' AS RESULT      
  END      
  ELSE      
  SELECT @GothAutoId AS GothAutoId,'Goth with same detail Already Exists.' AS RESULT      
 END      
      
 ELSE IF @operation = 'Update'      
 BEGIN      
 UPDATE dbo.tblGoths      
    SET    GothName = @GothName,Website=@Website, Address1 = @Address1, Address2 = @Address2,       
            Address3 = @Address3, Town = @Town, District = @District, City = @City,       
           NameofPerson=@NameofPerson,PersonMobile=@PersonMobile, PersonRole=@PersonRole,      
           UserId = @UserId, EntDate = @EntDate, EntOperation = 'Update', EntTerminal = @EntryTerminal,       
           EntTerminalIP = @EntryTerminalIP, FormId = @FormId, UserEmpId = @UserEmpId, UserEmpName = @UserEmpName,       
           UserEmpCode = @UserEmpCode      
    WHERE  GothAutoId = @GothAutoId      
 SELECT @GothAutoId AS GothAutoId,'Successfully Updated' AS RESULT      
 END      
          
 ELSE IF @operation='GetGothByiD'      
 BEGIN       
    SELECT c.GothAutoId, GothCode, GothName, Website, Address1, Address2, Address3, Town, District, City,      
 NameofPerson, PersonMobile, PersonRole, TitleAutoId, c.UserId, c.EntDate, c.EntOperation, c.EntTerminal,       
 c.EntTerminalIP, FormId, UserEmpId, UserEmpName, UserEmpCode, EnrollmentDate ,      
 ci.GothImageAutoId,ci.GothAutoId DetailGothImageAutoId,ci.GothPic,ci.FileType,ci.FileSize,ci.CaptureDate,ci.CaptureRemarks      
    FROM   dbo.tblGoths AS c   LEFT JOIN tblGothsImage  ci ON c.GothAutoId = ci.GothAutoId      
    WHERE  c.GothAutoId = @GothAutoId       
         
 END      
      
 ELSE IF @operation='GetAllGoth'      
 BEGIN      
 SELECT  DISTINCT c.GothAutoId, c.GothCode 'Goth Code', c.GothName 'Goth Name', ISNULL(ISNULL(Address1,c.Address2),c.Address3) Address      
     FROM tblGoths  c      
     ORDER BY c.GothAutoId desc      
 END      
      
 ELSE IF @operation = 'Search'      
 BEGIN      
 SELECT A.GothAutoId, A.GothCode, A.GothName, ISNULL(ISNULL(Address1,a.Address2),a.Address3) Address      
     FROM tblGoths A      
   WHERE     (      
   CAST(A.GothCode AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR      
   CAST(A.GothName AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR      
   CAST(A.Address1 AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR      
   CAST(A.Address2 AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR      
   CAST(A.Address3 AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR      
   CAST(A.District AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR      
   CAST(A.City AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'          
   )      
   ORDER BY A.GothAutoId desc      
 END      
        
 ELSE IF @operation='GetGoths'      
 BEGIN      
 SELECT DISTINCT TOP 5 c.GothAutoId Id, c.GothCode 'Code', c.GothName 'Text'      
     FROM tblGoths  c      
      WHERE       
     (c.GothCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR      
     c.GothName LIKE '%'+ISNULL(@SearchText,'')+ '%' )      
     ORDER BY c.GothAutoId desc      
 END      
  ELSE IF @operation = 'Delete'      
 BEGIN      
  DELETE      
  FROM   dbo.tblGoths      
  WHERE  GothAutoId = @GothAutoId      
  IF EXISTS(SELECT 1 FROM tblGothsImage  ci WHERE ci.GothAutoId=@GothAutoId)      
  BEGIN      
   DELETE FROM tblGothsImage WHERE GothAutoId=@GothAutoId      
  END      
  SELECT @GothAutoId AS GothAutoId,'Successfully Deleted' AS RESULT      
 END      
      
 ELSE IF @operation='GetNewGothsForAutoRef'      
 BEGIN      
 SELECT  DISTINCT TOP 5 c.GothAutoId Id, c.GothCode 'Code', c.GothName 'Text'      
     FROM tblGoths  c INNER JOIN tblGothsResident   cw ON c.GothAutoId = cw.GothAutoId      
     Left JOIN tblGothAutoRefTestResident artw ON cw.ResidentAutoId=artw.ResidentAutoId      
     WHERE       
     (c.GothCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR      
     c.GothName LIKE '%'+ISNULL(@SearchText,'')+ '%' )      
     ORDER BY c.GothName      
 END      
      
 ELSE IF @operation='GetEditGothsForAutoRef'      
 BEGIN      
 SELECT DISTINCT TOP 5 c.GothAutoId Id, c.GothCode 'Code', c.GothName 'Text'      
      FROM tblGoths  c INNER JOIN tblGothsResident  cw ON c.GothAutoId = cw.GothAutoId      
     Left JOIN tblGothAutoRefTestResident  artw ON cw.ResidentAutoId=artw.ResidentAutoId      
     WHERE       
     (c.GothCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR      
     c.GothName LIKE '%'+ISNULL(@SearchText,'')+ '%' )      
     ORDER BY c.GothName      
 END      
      
 ELSE IF @operation='GetNewGothsForOptometristResident'      
 BEGIN      
 SELECT DISTINCT  TOP 5  c.GothAutoId Id, c.GothCode 'Code', c.GothName 'Text'      
     FROM tblGoths  c INNER JOIN tblGothsResident   cw ON c.GothAutoId = cw.GothAutoId  
     JOIN tblGothAutoRefTestResident artw ON cw.ResidentAutoId=artw.ResidentAutoId      
     WHERE      
     (c.GothCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR      
     c.GothName LIKE '%'+ISNULL(@SearchText,'')+ '%' )      
     ORDER BY c.GothName      
 END      
      
 ELSE IF @operation='GetEditGothsForOptometristResident'      
 BEGIN      
 SELECT  DISTINCT TOP 5 c.GothAutoId Id, c.GothCode 'Code', c.GothName 'Text'      
     FROM tblGoths c INNER JOIN tblGothsResident  cw ON c.GothAutoId = cw.GothAutoId      
     INNER JOIN tblGothAutoRefTestResident artw ON cw.ResidentAutoId=artw.ResidentAutoId   
     INNER JOIN tblOptometristGothResident  ow ON artw.AutoRefResidentId=ow.AutoRefResidentId      
     WHERE      
     (c.GothCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR      
     c.GothName LIKE '%'+ISNULL(@SearchText,'')+ '%' )      
     ORDER BY c.GothName      
 END      

 ELSE IF @operation='GetNewGothsForGlassDispense'        
 BEGIN        
	SELECT  DISTINCT TOP 5 c.GothAutoId Id, c.GothCode 'Code', c.GothName 'Text'        
     FROM tblGoths c INNER JOIN tblGothsResident   cw ON c.GothAutoId = cw.GothAutoId        
	  INNER JOIN tblOptometristGothResident  ow ON cw.ResidentAutoId = ow.ResidentAutoId
     --Left JOIN tblGothGlassDispenseResident  artw ON cw.ResidentAutoId=artw.ResidentAutoId        
    
     WHERE         
     (c.GothCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR        
     c.GothName LIKE '%'+ISNULL(@SearchText,'')+ '%' )        
     ORDER BY c.GothName        
 END        
        
 ELSE IF @operation='GetEditGothsForGlassDispense'        
 BEGIN        
 SELECT DISTINCT TOP 5  c.GothAutoId Id, c.GothCode 'Code', c.GothName 'Text'        
     FROM tblGoths c INNER JOIN tblGothsResident cw ON c.GothAutoId = cw.GothAutoId        
     INNER JOIN tblGothGlassDispenseResident  artw ON cw.ResidentAutoId=artw.ResidentAutoId        
     WHERE         
     (c.GothCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR        
     c.GothName LIKE '%'+ISNULL(@SearchText,'')+ '%' )        
     ORDER BY c.GothName        
 END        
      
GO


 
CREATE OR ALTER PROCEDURE [dbo].[sp_SetupGothsImage](
	@GothImageAutoId INT=NULL,
	@GothAutoId	INT = NULL,  
	@GothPic		VARCHAR(MAX) = NULL,
	@FileType		NVARCHAR(20) = NULL,
	@CaptureDate DATE=NULL,
	@FileSize		INT = NULL,
	@CaptureRemarks  VARCHAR(500)=NULL,
	@UserId nvarchar(500) = NULL,  
	@EntDate DATETIME = NULL,  
	@EntOperation nvarchar(200) = NULL,  
	@EntryTerminal nvarchar(400) = NULL,  
	@EntryTerminalIP  nvarchar(400)  = NULL,
	@UserEmpName NVARCHAR(100)=NULL,
	@operation VARCHAR(50)= NULL
)
AS
IF @operation = 'Save'
BEGIN
    	INSERT INTO tblGothsImage (GothAutoId, GothPic, FileType, FileSize, CaptureDate, CaptureRemarks, UserId, EntDate, EntOperation, EntTerminal, EntTerminalIP)
		SELECT   @GothAutoId, @GothPic, SUBSTRING(@FileType,1,10), @FileSize,@CaptureDate,@CaptureRemarks, @UserId, @EntDate, 'INSERT', @EntryTerminal, @EntryTerminalIP  
		SET @GothImageAutoId=SCOPE_IDENTITY()
		SELECT  @GothImageAutoId GothImageAutoId ,'Successfully Saved' AS Result
END

ELSE IF @operation='Update'
BEGIN

		UPDATE tblGothsImage SET GothPic = @GothPic, FileType = @FileType, FileSize = @FileSize, 
		UserId = @UserId, EntDate = @EntDate, EntOperation = 'UPDATE', EntTerminal = @EntryTerminal, EntTerminalIP = @EntryTerminalIP    
		WHERE GothAutoId = @GothAutoId
		SELECT @GothAutoId AS GothAutoId,'Successfully Updated' AS RESULT
END


ELSE IF @operation='Delete'
BEGIN

		DELETE FROM tblGothsImage WHERE GothImageAutoId=@GothImageAutoId
		SELECT @GothImageAutoId AS GothImageAutoId,'Successfully Deleted' AS RESULT
END
GO


 
 
CREATE OR ALTER PROCEDURE [dbo].[Sp_GothsResident](
@ResidentAutoId int=NULL,
@GothAutoId int=NULL,
@GothCode VARCHAR(20)=NULL,
@ResidentCode nvarchar(15)=NULL,
@ResidentName nvarchar(500)=NULL,
@RelationType NVARCHAR(100)=NULL,
@RelationName nvarchar(500)=NULL,
@Age int=NULL,
@GenderAutoId int=NULL,
@DecreasedVision bit=NULL,
@Distance BIT= NULL,
@Near BIT= NULL,
@WearGlasses bit=NULL,
@CNIC VARCHAR(30) =NULL,
@UserId nvarchar(250)=NULL,
@EnrollmentDate DATETIME =NULL,
@EntryTerminal VARCHAR(200)=NULL,
@EntryTerminalIP VARCHAR(200)=NULL,
@HasOccularHistory bit=NULL,
@OccularHistoryRemarks nvarchar(500)=NULL,
@HasMedicalHistory bit=NULL,
@MedicalHistoryRemarks nvarchar(500)=NULL,
@HasChiefComplain bit=NULL,
@ChiefComplainRemarks nvarchar(500)=NULL,
@ResidentTestDate datetime=NULL,
@ResidentRegNo varchar(20)=NULL,
@MobileNo VARCHAR(15)=NULL,
@SectionAutoId int=NULL,
@ApplicationID nvarchar(20)=NULL,
@FormId nvarchar(250)=NULL,
@UserEmpId int=NULL,
@UserEmpName nvarchar(250)=NULL,
@UserEmpCode nvarchar(10)=NULL,
@Religion bit=NULL,
@operation VARCHAR(50)= NULL,
@SearchText VARCHAR(MAX)=NULL
)
AS
DECLARE @ResidentNewCode VARCHAR(10)=NULL
IF @operation='Save'
BEGIN
	IF NOT EXISTS(SELECT 1 FROM tblGothsResident c WITH (NOLOCK) WHERE c.ResidentName=@ResidentName AND c.CNIC=@CNIC )
	BEGIN
	BEGIN TRAN
	BEGIN TRy
	SELECT @GothAutoId=c.GothAutoId FROM tblGoths   c WHERE c.GothCode=@GothCode
	EXEC Sp_GetCode @CodeType = 'GothsResident',@CodeLength = 4	   ,@PreFix = '06'	   ,@GothCode = @GothCode	  
					,@GothId = @GothAutoId   ,@operation = 'GetGothCode',@Code = @ResidentNewCode OUTPUT
				 
			INSERT INTO tblGothsResident (GothAutoId, ResidentCode, ResidentName, RelationType, RelationName,
						Age, GenderAutoId, CNIC, UserId, EntDate, EntOperation, EntTerminal, EntTerminalIP, 
						WearGlasses, Distance, Near, DecreasedVision, HasOccularHistory, OccularHistoryRemarks,
						HasMedicalHistory, MedicalHistoryRemarks, HasChiefComplain, ChiefComplainRemarks,
						ResidentTestDate, ResidentRegNo, SectionAutoId, ApplicationID, FormId, UserEmpId,
						UserEmpName, UserEmpCode, Religion, MobileNo, GothCode)			

			SELECT @GothAutoId,@ResidentNewCode, @ResidentName, @RelationType,@RelationName, 
                    @Age, @GenderAutoId,@CNIC,@UserId,@EnrollmentDate,'INSERT', @EntryTerminal, @EntryTerminalIP,
					@WearGlasses,@Distance,@Near,@DecreasedVision,@HasOccularHistory,@OccularHistoryRemarks
					,@HasMedicalHistory, @MedicalHistoryRemarks, @HasChiefComplain, @ChiefComplainRemarks, 
					@ResidentTestDate,@ResidentRegNo,@SectionAutoId,@ApplicationID,@FormId,@UserEmpId,
					@UserEmpName,@UserEmpCode,@Religion,@MobileNo,@GothCode

			  SET @ResidentAutoId = SCOPE_IDENTITY()

			EXEC Sp_GetCode @CodeType = 'GothsResident',@CodeLength = 4	   ,@PreFix = '06'	   ,@GothCode = @GothCode,
							@GothId = @GothAutoId   ,@operation = 'UpdateGothCode'
			SELECT @ResidentAutoId AS ResidentAutoId,'Successfully Saved: <br> Resident Code: '+@ResidentNewCode AS RESULT
			Commit
END TRY
BEGIN CATCH
		SELECT @ResidentAutoId AS ResidentAutoId,'Error'+ERROR_MESSAGE() AS RESULT
ROLLBACK TRAN
END Catch
		END
		ELSE
		SELECT @ResidentAutoId AS ResidentAutoId,'Resident with same detail Already Exists.' AS RESULT
END
ELSE IF @operation = 'Update'
BEGIN
BEGIN TRAN
BEGIN TRy

	IF(@GothCode != (SELECT top 1 cw.GothCode FROM tblGothsResident cw WITH (NOLOCK) WHERE cw.ResidentAutoId=@ResidentAutoId))
	BEGIN
		SELECT @GothAutoId=c.GothAutoId FROM tblGothsResident c WHERE c.GothCode=@GothCode 
		EXEC Sp_GetCode @CodeType = 'GothsResident',@CodeLength = 4	   ,@PreFix = '06'	   ,
		@GothCode = @GothCode,@GothId= @GothAutoId,@operation = 'GetGothCode',@Code = @ResidentNewCode OUTPUT

		EXEC Sp_GetCode @CodeType = 'GothResident',@CodeLength = 4	   ,@PreFix = '06'	   ,@GothCode = @GothCode,@GothId = @GothAutoId
		,@operation = 'UpdateGothCode'

		SET @ResidentCode=@ResidentNewCode
	END
	ELSE
	BEGIN
		SELECT @ResidentCode=cw.ResidentCode FROM tblGothsResident cw WITH (NOLOCK) WHERE cw.ResidentAutoId=@ResidentAutoId
	END
		UPDATE tblGothsResident 
		SET GothAutoId = @GothAutoId
		   ,ResidentName = @ResidentName 
		   ,ResidentCode=@ResidentCode
		   ,RelationType=@RelationType
		   ,RelationName = @RelationName 
		   ,Age = @Age 
		   ,GenderAutoId = @GenderAutoId
		   ,CNIC = @CNIC
		   ,DecreasedVision = @DecreasedVision 
		   ,Near=@Near
		   ,Distance=@Distance
		   ,EntOperation = 'Update'
		   ,WearGlasses = @WearGlasses 
		   ,HasOccularHistory = @HasOccularHistory 
		   ,OccularHistoryRemarks = @OccularHistoryRemarks 
		   ,HasMedicalHistory = @HasMedicalHistory 
		   ,MedicalHistoryRemarks = @MedicalHistoryRemarks 
		   ,HasChiefComplain = @HasChiefComplain 
		   ,ChiefComplainRemarks = @ChiefComplainRemarks 
		   ,ResidentTestDate = @ResidentTestDate
		   ,MobileNo = @MobileNo
		   ,Religion = @Religion 
			WHERE 
			ResidentAutoId = @ResidentAutoId

				SELECT @ResidentAutoId AS ResidentAutoId,'Successfully Updated: <br> '+@ResidentCode AS RESULT
			Commit
END TRY
BEGIN CATCH
		SELECT @ResidentAutoId AS ResidentAutoId,'Error Updated: '+ERROR_MESSAGE() AS RESULT
ROLLBACK TRAN
END Catch
END

ELSE IF @operation = 'Delete'
	BEGIN
		DELETE
		FROM   dbo.tblGothsResident
		WHERE  ResidentAutoId = @ResidentAutoId
		IF EXISTS(SELECT 1 FROM tblGothsResidentImage ci WHERE ci.GothAutoId=@GothAutoId)
		BEGIN
			DELETE FROM tblGothsResidentImage WHERE ResidentAutoId=@ResidentAutoId
		END
		SELECT @ResidentAutoId AS ResidentAutoId,'Successfully Deleted' AS RESULT
	END

ELSE IF @operation='GetResidentByiD'
	BEGIN
	SELECT	c.ResidentAutoId,c.GothAutoId,c.GothCode, c.ResidentCode, c.ResidentName,c.RelationType,
			c.RelationName, c.Age, ISNULL(c.GenderAutoId,0)GenderAutoId , c.CNIC, c.DecreasedVision,c.UserId,
			c.EntDate, c.Near,c.Distance,	c.EntOperation, c.EntTerminal, c.EntTerminalIP, c.WearGlasses,
			c.HasOccularHistory, c.OccularHistoryRemarks, c.HasMedicalHistory, c.MedicalHistoryRemarks,c.EntDate EnrollmentDate,c.MobileNo,
			c.HasChiefComplain, c.ChiefComplainRemarks, c.ResidentTestDate, c.ResidentRegNo, c.SectionAutoId, c.ApplicationID, c.FormId, c.UserEmpId,c1.GothName,
			c.UserEmpName, c.UserEmpCode, c.Religion, c.EntDate,ci.ResidentImageAutoId,ci.ResidentAutoId,
			ISNULL(ci.CaptureRemarks,'')CaptureRemarks	,ci.GothAutoId DetailGothAutoId,ci.FileType,ci.FileSize,ci.ResidentPic
		   FROM tblGothsResident c	 INNER JOIN tblGoths  c1 ON c1.GothAutoId =c.GothAutoId
		   LEFT JOIN tblGothsResidentImage ci ON c.ResidentAutoId = ci.ResidentAutoId
		WHERE 
		c.ResidentAutoId=@ResidentAutoId
	END

--	ELSE IF @operation='GetAllWorker'
--	BEGIN
--	SELECT TOP 10 c.WorkerAutoId,c.WorkerCode,c.WorkerName,c1.CompanyName,c.CNIC,c.MobileNo
--		   FROM tblGothsResident c INNER JOIN tblCompany c1 ON c.CompanyAutoId = c1.CompanyAutoId
--		   ORDER BY c.WorkerAutoId desc
--	END

	
	ELSE IF @operation = 'Search'
	BEGIN
	SELECT TOP 10 c.ResidentAutoId,c.ResidentCode,c.ResidentName,A.GothName,c.CNIC,c.MobileNo
		   FROM tblGoths   A INNER JOIN tblGothsResident c ON A.GothAutoId = c.GothAutoId
		 WHERE
		 (
			 CAST(c.ResidentCode AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR
			 CAST(A.GothName AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR
			 CAST(c.ResidentName AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR
			 CAST(c.CNIC AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR
			 CAST(c.MobileNo AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'    
		 )
		 ORDER BY c.ResidentAutoId desc
	END

	ELSE IF @operation= 'GetResidents'
	BEGIN
	SELECT DISTINCT  TOP 5  cw.ResidentAutoId 'Id', cw.ResidentCode 'Code', cw.ResidentName +' | '+cw.ResidentCode 'Text'
		   FROM tblGothsResident cw WITH(NOLOCK) 
		   WHERE
		   cw.GothAutoId=@GothAutoId AND 
		   (cw.ResidentCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR
		   cw.ResidentName LIKE '%'+ISNULL(@SearchText,'')+ '%' )
		   ORDER BY  cw.ResidentName+' | '+cw.ResidentCode 
	END
	ELSE IF @operation= 'DeleteWorkerById'
	BEGIN
		DELETE FROM tblGothsResident WHERE ResidentAutoId=@ResidentAutoId
		DELETE FROM tblGothsResidentImage WHERE ResidentAutoId=@ResidentAutoId

		SELECT @ResidentAutoId AS ResidentAutoId,'Successfully Deleted' AS RESULT

		
	END
GO



CREATE OR ALTER PROCEDURE [dbo].[sp_GothsResidentImage](  
 @ResidentAutoId INT =NULL,  
 @ResidentImageAutoId INT=NULL,  
 @GothAutoId INT = NULL,    
 @ResidentPic  VARCHAR(MAX) = NULL,  
 @FileType  NVARCHAR(20) = NULL,  
 @FileSize  INT = NULL,  
 @CaptureRemarks  VARCHAR(500)=NULL,  
 @UserId nvarchar(500) = NULL,    
 @EntDate DATETIME = NULL,    
 @EntOperation nvarchar(200) = NULL,    
 @EntryTerminal nvarchar(400) = NULL,    
 @EntryTerminalIP  nvarchar(400)  = NULL,  
 @UserEmpName NVARCHAR(100)=NULL,  
 @operation VARCHAR(50)= NULL  
)  
AS  
IF @operation = 'Save'  
BEGIN  
     INSERT INTO tblGothsResidentImage (ResidentAutoId, GothAutoId, ResidentPic, FileType, FileSize, CaptureDate, CaptureRemarks, UserId, EntDate, EntOperation, EntTerminal, EntTerminalIP)  
  SELECT  @ResidentAutoId, @GothAutoId, @ResidentPic, SUBSTRING(@FileType,1,10), @FileSize,GETDATE(),@CaptureRemarks, @UserId, @EntDate, 'INSERT', @EntryTerminal, @EntryTerminalIP    
  SET @ResidentImageAutoId=SCOPE_IDENTITY()  
  SELECT  @ResidentImageAutoId GothImageAutoId ,'Successfully Saved' AS Result  
END  
  
ELSE IF @operation='Update'  
BEGIN  
  
  UPDATE tblGothsResidentImage SET ResidentPic = @ResidentPic, FileType = @FileType, FileSize = @FileSize,   
  UserId = @UserId, EntDate = @EntDate, EntOperation = 'UPDATE', EntTerminal = @EntryTerminal, EntTerminalIP = @EntryTerminalIP      
  WHERE @ResidentAutoId = @ResidentAutoId  
  SELECT @ResidentAutoId AS ResidentAutoId,'Successfully Updated' AS RESULT  
END  
  
  
ELSE IF @operation='Delete'  
BEGIN  
  
  DELETE FROM tblGothsResidentImage WHERE ResidentImageAutoId=@ResidentImageAutoId  
  SELECT @ResidentImageAutoId AS ResidentImageAutoId,'Successfully Deleted' AS RESULT  
END  
GO
 
  
   
CREATE OR ALTER PROCEDURE [dbo].[Sp_GothAutoRefTestResident](  
 @GothAutoId INT =NULL,  
 @AutoRefResidentId INT = NULL,  
    @AutoRefResidentTransId varchar(15) = NULL,  
    @AutoRefResidentTransDate datetime = NULL,  
    @ResidentAutoId int= NULL,  
    @Right_Spherical_Status char(1)= NULL,  
    @Right_Spherical_Points decimal(9, 2)= NULL,  
    @Right_Cyclinderical_Status char(1)= NULL,  
    @Right_Cyclinderical_Points decimal(9, 2)= NULL,  
    @Right_Axix_From int= NULL,  
    @Right_Axix_To int= NULL,  
    @Left_Spherical_Status char(1)= NULL,  
    @Left_Spherical_Points decimal(9, 2)= NULL,  
    @Left_Cyclinderical_Status char(1)= NULL,  
    @Left_Cyclinderical_Points decimal(9, 2)= NULL,  
    @Left_Axix_From int= NULL,  
    @Left_Axix_To int= NULL,  
 @IPD INT = NULL,   
    @UserId nvarchar(250)= NULL,  
    @EntDate datetime= NULL,  
    @EntOperation nvarchar(100)= NULL,  
    @EntryTerminal nvarchar(200)= NULL,  
    @EntryTerminalIP nvarchar(200)= NULL,  
    @ApplicationID nvarchar(20)= NULL,  
    @FormId nvarchar(250)= NULL,  
    @UserEmpId int= NULL,  
    @UserEmpName nvarchar(250)= NULL,  
    @UserEmpCode nvarchar(10)= NULL,  
 @operation VARCHAR(50)= NULL,  
 @SearchText VARCHAR(MAX)=NULL  
 )  
 AS  
IF @operation='Save'  
 BEGIN  
 IF NOT EXISTS(SELECT 1 FROM tblGothAutoRefTestResident   artw WHERE artw.ResidentAutoId=@ResidentAutoId AND CAST(artw.AutoRefResidentTransDate AS DATE)=CAST(@AutoRefResidentTransDate AS date))  
 BEGIN  
  INSERT INTO dbo.tblGothAutoRefTestResident (AutoRefResidentTransId, AutoRefResidentTransDate,   
                                          ResidentAutoId, Right_Spherical_Status, Right_Spherical_Points,   
                                          Right_Cyclinderical_Status, Right_Cyclinderical_Points, Right_Axix_From,   
                                          Right_Axix_To, Left_Spherical_Status, Left_Spherical_Points,   
                                          Left_Cyclinderical_Status, Left_Cyclinderical_Points, Left_Axix_From,   
                                          Left_Axix_To,IPD, UserId, EntDate, EntOperation, EntTerminal,   
                                          EntTerminalIP, ApplicationID, FormId, UserEmpId, UserEmpName,   
                                          UserEmpCode)  
    SELECT @AutoRefResidentTransId, @AutoRefResidentTransDate, @ResidentAutoId, @Right_Spherical_Status,   
           @Right_Spherical_Points, @Right_Cyclinderical_Status, @Right_Cyclinderical_Points, @Right_Axix_From,   
           @Right_Axix_To, @Left_Spherical_Status, @Left_Spherical_Points, @Left_Cyclinderical_Status,   
           @Left_Cyclinderical_Points, @Left_Axix_From, @Left_Axix_To,@IPD, @UserId, GETDATE(), 'INSERT',   
           @EntryTerminal, @EntryTerminalIP, @ApplicationID, @FormId, @UserEmpId, @UserEmpName, @UserEmpCode  
     SET @AutoRefResidentId = SCOPE_IDENTITY()  
     SELECT @AutoRefResidentId  AS AutoRefResidentId,'Successfully Saved' AS RESULT  
 END  
 ELSE  
    SELECT @AutoRefResidentId  AS AutoRefResidentId,'Resident Auto Refraction Already Exists in selected Date' AS RESULT  
 END  
ELSE IF @operation='Update'  
 BEGIN  
  UPDATE dbo.tblGothAutoRefTestResident  
  SET    AutoRefResidentTransId = @AutoRefResidentTransId, ResidentAutoId = @ResidentAutoId, Right_Spherical_Status = @Right_Spherical_Status,   
    Right_Spherical_Points = @Right_Spherical_Points, Right_Cyclinderical_Status = @Right_Cyclinderical_Status,   
    Right_Cyclinderical_Points = @Right_Cyclinderical_Points, Right_Axix_From = @Right_Axix_From,   
    Right_Axix_To = @Right_Axix_To, Left_Spherical_Status = @Left_Spherical_Status, Left_Spherical_Points = @Left_Spherical_Points,   
    Left_Cyclinderical_Status = @Left_Cyclinderical_Status, Left_Cyclinderical_Points = @Left_Cyclinderical_Points,   
    Left_Axix_From = @Left_Axix_From, Left_Axix_To = @Left_Axix_To,IPD=@IPD, UserId = @UserId,  
    EntOperation = 'Update', EntTerminal = @EntryTerminal, EntTerminalIP = @EntryTerminalIP,   
    UserEmpId = @UserEmpId, UserEmpName = @UserEmpName, UserEmpCode = @UserEmpCode  
  WHERE  AutoRefResidentId = @AutoRefResidentId   
  
  SELECT @AutoRefResidentId  AS AutoRefResidentId,'Successfully Updated' AS RESULT  
 END  
  
ELSE IF @operation='GetByAutoRefResidentId '  
 BEGIN  
  SELECT AutoRefResidentId, AutoRefResidentTransId, AutoRefResidentTransDate, ResidentAutoId, Right_Spherical_Status, Right_Spherical_Points,   
  Right_Cyclinderical_Status, Right_Cyclinderical_Points, Right_Axix_From, Right_Axix_To, Left_Spherical_Status, Left_Spherical_Points,   
  Left_Cyclinderical_Status, Left_Cyclinderical_Points, Left_Axix_From, Left_Axix_To, ISNULL(IPD,0)IPD,UserId, EntDate, EntOperation, EntTerminal,  
  EntTerminalIP, ApplicationID, FormId, UserEmpId, UserEmpName, UserEmpCode ,0 WearGlasses  
  FROM   dbo.tblGothAutoRefTestResident  
  WHERE  AutoRefResidentId = @AutoRefResidentId    
END  
ELSE IF @operation='GetAutoRefByResidentId '  
 BEGIN  
  SELECT   
      a.AutoRefResidentId, a.AutoRefResidentTransId, CAST(a.AutoRefResidentTransDate AS varchar) 'Test Date', a.ResidentAutoId AutoRefResidentId,   
      a.Right_Spherical_Status, CASE WHEN  ISNULL(a.Right_Spherical_Points,0) > 0 THEN '+'+CAST(a.Right_Spherical_Points AS VARCHAR)  
      ELSE CAST(a.Right_Spherical_Points AS VARCHAR) END 'Right Spherical', a.Right_Cyclinderical_Status,      
      CASE WHEN ISNULL(a.Left_Spherical_Points,0) > 0 THEN '+'+ CAST(a.Left_Spherical_Points AS VARCHAR)  
      ELSE CAST(a.Left_Spherical_Points AS VARCHAR) END 'Left Spherical',   
      '', a.Right_Axix_To, a.Left_Spherical_Status, a.Right_Cyclinderical_Points 'Right Cyclinderical',   
      a.Left_Cyclinderical_Status,a.Left_Cyclinderical_Points 'Left Cyclinderical',a.Right_Axix_From AS 'Right Axis' , a.Left_Axix_From AS 'Left Axis',  
      a.Left_Axix_To,ISNULL(IPD,0)IPD, a.UserId, a.EntDate, a.EntOperation, a.EntTerminal, a.EntTerminalIP, a.ApplicationID, a.FormId, a.UserEmpId, a.UserEmpName, a.UserEmpCode   
      ,b.WearGlasses  
      FROM  tblGothsResident b INNER  JOIN dbo.tblGothAutoRefTestResident a ON b.ResidentAutoId=a.ResidentAutoId  
  WHERE  b.ResidentAutoId=@ResidentAutoId  
 END  
ELSE IF @operation='NewResidentForRef'  
BEGIN  
 SELECT DISTINCT top 5 cw.ResidentAutoId Id, cw.ResidentName +' | '+cw.ResidentCode  text,cw.ResidentCode Code   
 FROM tblGothsResident cw  
 WHERE   
 cw.GothAutoId=@GothAutoId AND   
 cw.ResidentAutoId NOT IN (SELECT artw.ResidentAutoId FROM tblGothAutoRefTestResident artw WITH (NOLOCK) WHERE CAST(artw.AutoRefResidentTransDate AS DATE) = CAST(@AutoRefResidentTransDate AS DATE))  
  AND   
 (cw.ResidentCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR  
  cw.ResidentName LIKE '%'+ISNULL(@SearchText,'')+ '%' )  
 ORDER BY  cw.ResidentName +' | '+cw.ResidentCode   
END  
  
ELSE IF @operation='EditResidentForRef'  
BEGIN  
 SELECT DISTINCT TOP 5 cw.ResidentAutoId Id, cw.ResidentName +' | '+cw.ResidentCode  text,cw.ResidentCode Code   
 FROM tblGothsResident cw INNER JOIN tblGothAutoRefTestResident artw ON cw.ResidentAutoId=artw.ResidentAutoId  
 WHERE   
 cw.GothAutoId=@GothAutoId  
 AND (cw.ResidentCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR  
  cw.ResidentName LIKE '%'+ISNULL(@SearchText,'')+ '%' )  
 ORDER BY  cw.ResidentName +' | '+cw.ResidentCode   
END  
ELSE IF @operation='GetAutoRefHistoryByResidentId '  
 BEGIN  
    
   SELECT TOP 1 CAST(FORMAT(artw.AutoRefResidentTransDate,'dd | MMM | yyyy') AS VARCHAR) AS Last_Visit_Date,  
   CASE WHEN artw.Right_Spherical_Status ='P' AND artw.Right_Spherical_Points>0 THEN '+' +CAST(Right_Spherical_Points AS VARCHAR)  
   WHEN artw.Right_Spherical_Status='N'AND artw.Right_Spherical_Points>0 THEN '-' +CAST(Right_Spherical_Points AS VARCHAR)  
   WHEN artw.Right_Spherical_Status='E' THEN 'Error '+CAST(ISNULL(Right_Spherical_Points,'') AS VARCHAR)  
   WHEN artw.Right_Spherical_Status='l' THEN 'Plano '+CAST(ISNULL(Right_Spherical_Points,'') AS VARCHAR)  
   ELSE CAST(Right_Spherical_Points AS VARCHAR) END 'Right Spherical',  
  
   CASE WHEN artw.Left_Spherical_Status ='P' AND artw.Left_Spherical_Points>0 THEN '+' +CAST(Left_Spherical_Points AS VARCHAR)  
   WHEN artw.Left_Spherical_Status='N'AND artw.Left_Spherical_Points>0 THEN '-' +CAST(Left_Spherical_Points AS VARCHAR)  
    WHEN artw.Left_Spherical_Status='E' THEN 'Error '+CAST(ISNULL(Left_Spherical_Points,'') AS VARCHAR)  
   WHEN artw.Left_Spherical_Status='l' THEN 'Plano '+CAST(ISNULL(Left_Spherical_Points,'') AS VARCHAR)  
   ELSE CAST(Left_Spherical_Points AS VARCHAR) END 'Left Spherical',  
  
   CASE WHEN artw.Right_Spherical_Status ='P' AND artw.Left_Spherical_Points>0 THEN '+' +CAST(Left_Spherical_Points AS VARCHAR)  
   WHEN artw.Right_Spherical_Status='N'AND artw.Right_Spherical_Points>0 THEN '-' +CAST(Right_Spherical_Points AS VARCHAR)  
   ELSE CAST(Right_Spherical_Points AS VARCHAR) END 'Right Spherical',  
  
   CASE WHEN artw.Right_Cyclinderical_Status ='P' AND artw.Right_Cyclinderical_Points>0 THEN '+' +CAST(Right_Cyclinderical_Points AS VARCHAR)  
   WHEN artw.Right_Cyclinderical_Status='N'AND artw.Right_Cyclinderical_Points>0 THEN '-' +CAST(Right_Cyclinderical_Points AS VARCHAR)  
   ELSE CAST(Right_Cyclinderical_Points AS VARCHAR) END 'Right Cyclinderical',   
  
     
   CASE WHEN artw.Left_Cyclinderical_Status ='P' AND artw.Left_Cyclinderical_Points>0 THEN '+' +CAST(Left_Cyclinderical_Points AS VARCHAR)  
   WHEN artw.Left_Cyclinderical_Status='N'AND artw.Left_Cyclinderical_Points>0 THEN '-' +CAST(Left_Cyclinderical_Points AS VARCHAR)  
   ELSE CAST(artw.Left_Cyclinderical_Points AS VARCHAR) END 'Left Cyclinderical',   
   artw.Right_Axix_From 'Right Axis' ,  
    artw.Left_Axix_From 'Left Axis' ,0 WearGlasses,  
    ISNULL(artw.IPD,0)IPD  
  
  FROM tblGothAutoRefTestResident artw  
  WHERE  artw.ResidentAutoId=@ResidentAutoId  
  ORDER BY artw.AutoRefResidentTransDate desc  
 END  
  
 ELSE IF @operation= 'GetDatesofResident'  
 BEGIN  
  SELECT CAST(FORMAT(artw.AutoRefResidentTransDate,'dd | MMM | yyyy') AS VARCHAR) Text,  
  artw.AutoRefResidentId AS Id  
  FROM tblGothAutoRefTestResident artw  
  WHERE artw.ResidentAutoId=@ResidentAutoId  
  ORDER BY artw.AutoRefResidentId desc  
 END  
  
   
ELSE IF @operation='GetByAutoRefResidentIdForOpt '  
 BEGIN  
  SELECT TOP 1 a.AutoRefResidentId, a.AutoRefResidentTransId, a.AutoRefResidentTransDate,a. ResidentAutoId,
  cw.Near,cw.Distance,cw.Age,  a.Right_Spherical_Status,   
  a.Right_Spherical_Points, a.Right_Cyclinderical_Status, a.Right_Cyclinderical_Points, a.Right_Axix_From, a.Right_Axix_To,  
  a.Left_Spherical_Status, a.Left_Spherical_Points,  a.Left_Cyclinderical_Status, a.Left_Cyclinderical_Points, 
  a.Left_Axix_From, Left_Axix_To, ISNULL(IPD,0)IPD, a.UserId, a.EntDate, a.EntOperation, a.EntTerminal,  
  a.EntTerminalIP, a.ApplicationID, a.FormId, a.UserEmpId, a.UserEmpName, a.UserEmpCode ,ISNULL(cw.WearGlasses,0)WearGlasses  
  FROM   dbo.tblGothAutoRefTestResident a INNER JOIN  tblGothsResident  cw ON cw.ResidentAutoId=a.ResidentAutoId
  WHERE  a.ResidentAutoId = @ResidentAutoId
  ORDER BY a.AutoRefResidentId desc  
END  
ELSE IF @operation= 'DeleteAutoRefById'  
 BEGIN  
  DELETE FROM tblGothAutoRefTestResident WHERE AutoRefResidentId=@AutoRefResidentId  
  SELECT @AutoRefResidentId AS AutoRefResidentId,'Successfully Deleted' AS RESULT  
 END
 GO
  

 CREATE OR ALTER PROC [dbo].[Sp_OptometristGothResident](  
    @OptometristGothResidentId INT = NULL,  
    @OptometristGothResidentTransDate datetime = NULL,  
    @ResidentAutoId int = NULL,  
 @AutoRefResidentId INT=NULL,  
    @HasChiefComplain int = NULL,  
    @ChiefComplainRemarks nvarchar(200)= NULL,  
    @HasOccularHistory int= NULL,  
    @OccularHistoryRemarks nvarchar(200)= NULL,  
    @HasMedicalHistory int= NULL,  
    @MedicalHistoryRemarks nvarchar(200)= NULL,  
    @DistanceVision_RightEye_Unaided int= NULL,  
    @DistanceVision_RightEye_WithGlasses int= NULL,  
    @DistanceVision_RightEye_PinHole int= NULL,  
    @NearVision_RightEye int= NULL,  
    @NeedCycloRefraction_RightEye int= NULL,  
    @NeedCycloRefractionRemarks_RightEye nvarchar(200)= NULL,  
    @DistanceVision_LeftEye_Unaided int= NULL,  
    @DistanceVision_LeftEye_WithGlasses int= NULL,  
    @DistanceVision_LeftEye_PinHole int= NULL,  
    @NearVision_LeftEye int= NULL,  
    @NeedCycloRefraction_LeftEye int= NULL,  
    @NeedCycloRefractionRemarks_LeftEye nvarchar(200)= NULL,  
    @Right_Spherical_Status char(1)= NULL,  
    @Right_Spherical_Points decimal(9, 2)= NULL,  
    @Right_Cyclinderical_Status char(1)= NULL,  
    @Right_Cyclinderical_Points decimal(9, 2)= NULL,  
    @Right_Axix_From int= NULL,  
    @Right_Axix_To int= NULL,  
    @Right_Near_Status char(1)= NULL,  
    @Right_Near_Points decimal(9, 2)= NULL,  
    @Left_Spherical_Status char(1)= NULL,  
    @Left_Spherical_Points decimal(9, 2)= NULL,  
    @Left_Cyclinderical_Status char(1)= NULL,  
    @Left_Cyclinderical_Points decimal(9, 2)= NULL,  
    @Left_Axix_From int= NULL,  
    @Left_Axix_To int= NULL,  
    @Left_Near_Status char(1)= NULL,  
    @Left_Near_Points decimal(9, 2)= NULL,  
    @Douchrome int= NULL,  
    @Achromatopsia varchar(20)= NULL,  
    @RetinoScopy_RightEye int= NULL,   
    @Condition_RightEye varchar(200)= NULL,  
    @Meridian1_RightEye varchar(200)= NULL,  
    @Meridian2_RightEye varchar(200)= NULL,  
    @FinalPrescription_RightEye varchar(200)= NULL,  
    @RetinoScopy_LeftEye int= NULL,   
    @Condition_LeftEye varchar(200)= NULL,  
    @Meridian1_LeftEye varchar(200)= NULL,  
    @Meridian2_LeftEye varchar(200)= NULL,  
    @FinalPrescription_LeftEye varchar(200)= NULL,  
    @Hirchberg_Distance int= NULL,  
    @Hirchberg_Near int= NULL,  
    @Ophthalmoscope_RightEye int= NULL,  
    @PupillaryReactions_RightEye int= NULL,  
    @CoverUncovertTest_RightEye int= NULL,  
    @CoverUncovertTestRemarks_RightEye nvarchar(200)= NULL,  
    @ExtraOccularMuscleRemarks_RightEye nvarchar(200)= NULL,  
    @Ophthalmoscope_LeftEye int= NULL,  
    @PupillaryReactions_LeftEye int= NULL,  
    @CoverUncovertTest_LeftEye int= NULL,  
    @CoverUncovertTestRemarks_LeftEye nvarchar(200)= NULL,  
 @CycloplegicRefraction_RightEye BIT =NULL,  
 @CycloplegicRefraction_LeftEye BIT =NULL,  
 @Conjunctivitis_RightEye BIT =NULL,  
 @Conjunctivitis_LeftEye BIT =NULL,  
 @Scleritis_RightEye BIT =NULL,          
 @Scleritis_LeftEye BIT =NULL,  
 @Nystagmus_RightEye BIT =NULL,          
 @Nystagmus_LeftEye BIT =NULL,  
 @CornealDefect_RightEye BIT =NULL,  
 @CornealDefect_LeftEye BIT =NULL,  
 @Cataract_RightEye BIT =NULL,  
 @Cataract_LeftEye BIT =NULL,  
 @Keratoconus_RightEye BIT =NULL,  
 @Keratoconus_LeftEye BIT =NULL,  
 @Ptosis_RightEye BIT =NULL,  
 @Ptosis_LeftEye BIT =NULL,  
 @LowVision_RightEye BIT =NULL,  
 @LowVision_LeftEye BIT =NULL,  
 @Pterygium_RightEye BIT =NULL,  
 @Pterygium_LeftEye BIT =NULL,  
 @ColorBlindness_RightEye BIT =NULL,  
 @ColorBlindness_LeftEye BIT =NULL,  
 @Others_RightEye BIT =NULL,  
 @Others_LeftEye BIT =NULL,  
 @Fundoscopy_RightEye BIT =NULL,  
 @Fundoscopy_LeftEye BIT =NULL,  
 @Surgery_RightEye BIT =NULL,  
 @Surgery_LeftEye BIT =NULL,  
 @CataractSurgery_RightEye BIT =NULL,  
 @CataractSurgery_LeftEye BIT =NULL,  
 @SurgeryPterygium_RightEye BIT =NULL,  
 @SurgeryPterygium_LeftEye BIT =NULL,  
 @SurgeryCornealDefect_RightEye BIT =NULL,  
 @SurgeryCornealDefect_LeftEye BIT =NULL,  
 @SurgeryPtosis_RightEye BIT =NULL,  
 @SurgeryPtosis_LeftEye BIT =NULL,  
 @SurgeryKeratoconus_RightEye BIT =NULL,  
 @SurgeryKeratoconus_LeftEye BIT =NULL,  
 @Chalazion_RightEye BIT =NULL,  
 @Chalazion_LeftEye BIT =NULL,  
 @Hordeolum_RightEye BIT =NULL,  
 @Hordeolum_LeftEye BIT =NULL,  
 @SurgeryOthers_RightEye BIT =NULL,  
 @SurgeryOthers_LeftEye BIT =NULL,  
 @RightPupilDefects BIT =NULL,  
 @LeftPupilDefects BIT =NULL,  
 @RightSquint_Surgery BIT =NULL,  
 @LeftSquint_Surgery BIT =NULL,  
  
 @GothAutoId INT=NULL,  
    @UserId nvarchar(250)= NULL,  
    @EntDate datetime= NULL,  
    @EntOperation nvarchar(100)= NULL,  
    @EntryTerminal nvarchar(200)= NULL,  
    @EntryTerminalIP nvarchar(200)= NULL,  
    @ExtraOccularMuscleRemarks_LeftEye nvarchar(200)= NULL,  
    @ApplicationID nvarchar(20)= NULL,  
    @FormId nvarchar(250)= NULL,  
    @UserEmpId int= NULL,  
    @UserEmpName nvarchar(250)= NULL,  
    @UserEmpCode nvarchar(10)= NULL,  
    @VisualAcuity_RightEye int= NULL,  
    @VisualAcuity_LeftEye int= NULL,  
 @LeftSquint_VA BIT= NULL,  
 @RightSquint_VA BIT= NULL,  
 @LeftAmblyopic_VA BIT= NULL,  
 @RightAmblyopic_VA BIT= NULL,  
 @LeftAmblyopia BIT=NULL,  
 @RightAmblyopia BIT=NULL,  
 @Operation VARCHAR(50)=NULL,  
 @SearchText VARCHAR(1000)=null  
 )  
AS   
    IF @operation = 'Save'  
 BEGIN  
  SELECT @GothAutoId=GothAutoId FROM tblGothsResident  cw WHERE cw.ResidentAutoId=@ResidentAutoId  
  INSERT INTO dbo.tblOptometristGothResident (OptometristGothResidentTransDate,AutoRefResidentId , ResidentAutoId,GothAutoId, HasChiefComplain, ChiefComplainRemarks, HasOccularHistory,  
             OccularHistoryRemarks, HasMedicalHistory, MedicalHistoryRemarks, DistanceVision_RightEye_Unaided,  
             DistanceVision_RightEye_WithGlasses, DistanceVision_RightEye_PinHole, NearVision_RightEye,  
             NeedCycloRefraction_RightEye, NeedCycloRefractionRemarks_RightEye, DistanceVision_LeftEye_Unaided,  
             DistanceVision_LeftEye_WithGlasses, DistanceVision_LeftEye_PinHole, NearVision_LeftEye,  
             NeedCycloRefraction_LeftEye, NeedCycloRefractionRemarks_LeftEye, Right_Spherical_Status,   
             Right_Spherical_Points, Right_Cyclinderical_Status, Right_Cyclinderical_Points, Right_Axix_From, Right_Axix_To,  
             Right_Near_Status, Right_Near_Points, Left_Spherical_Status, Left_Spherical_Points,   
             Left_Cyclinderical_Status, Left_Cyclinderical_Points, Left_Axix_From, Left_Axix_To, Left_Near_Status, Left_Near_Points,   
             Douchrome, Achromatopsia, RetinoScopy_RightEye,  Condition_RightEye, Meridian1_RightEye,   
             Meridian2_RightEye, FinalPrescription_RightEye, RetinoScopy_LeftEye,  Condition_LeftEye,  
             Meridian1_LeftEye, Meridian2_LeftEye, FinalPrescription_LeftEye, Hirchberg_Distance, Hirchberg_Near, Ophthalmoscope_RightEye,   
             PupillaryReactions_RightEye, CoverUncovertTest_RightEye, CoverUncovertTestRemarks_RightEye, ExtraOccularMuscleRemarks_RightEye,  
             Ophthalmoscope_LeftEye, PupillaryReactions_LeftEye, CoverUncovertTest_LeftEye, CoverUncovertTestRemarks_LeftEye,  
             CycloplegicRefraction_RightEye , CycloplegicRefraction_LeftEye , Conjunctivitis_RightEye , Conjunctivitis_LeftEye ,  
                            Scleritis_RightEye , Scleritis_LeftEye , Nystagmus_RightEye , Nystagmus_LeftEye , CornealDefect_RightEye ,CornealDefect_LeftEye , Cataract_RightEye ,  
             Cataract_LeftEye , Keratoconus_RightEye , Keratoconus_LeftEye , Ptosis_RightEye , Ptosis_LeftEye , LowVision_RightEye , LowVision_LeftEye ,  
             Pterygium_RightEye , Pterygium_LeftEye , ColorBlindness_RightEye , ColorBlindness_LeftEye , Others_RightEye , Others_LeftEye , Fundoscopy_RightEye ,  
             Fundoscopy_LeftEye , Surgery_RightEye , Surgery_LeftEye , CataractSurgery_RightEye , CataractSurgery_LeftEye , SurgeryPterygium_RightEye ,  
             SurgeryPterygium_LeftEye , SurgeryCornealDefect_RightEye , SurgeryCornealDefect_LeftEye , SurgeryPtosis_RightEye , SurgeryPtosis_LeftEye ,  
             SurgeryKeratoconus_RightEye , SurgeryKeratoconus_LeftEye , Chalazion_RightEye , Chalazion_LeftEye , Hordeolum_RightEye , Hordeolum_LeftEye ,  
             SurgeryOthers_RightEye , SurgeryOthers_LeftEye,RightPupilDefects,LeftPupilDefects,UserId, EntDate,  
             EntOperation, EntTerminal, EntTerminalIP, ExtraOccularMuscleRemarks_LeftEye, ApplicationID, FormId, UserEmpId, UserEmpName, UserEmpCode,  
             VisualAcuity_RightEye, VisualAcuity_LeftEye, LeftSquint_VA ,RightSquint_VA,LeftAmblyopic_VA  ,RightAmblyopic_VA ,  
             RightAmblyopia,LeftAmblyopia,LeftSquint_Surgery,RightSquint_Surgery  
             )  
  
  SELECT  @OptometristGothResidentTransDate,@AutoRefResidentId , @ResidentAutoId, @GothAutoId,@HasChiefComplain, @ChiefComplainRemarks,   
      @HasOccularHistory, @OccularHistoryRemarks, @HasMedicalHistory, @MedicalHistoryRemarks, @DistanceVision_RightEye_Unaided,   
      @DistanceVision_RightEye_WithGlasses, @DistanceVision_RightEye_PinHole, @NearVision_RightEye,   
      @NeedCycloRefraction_RightEye, @NeedCycloRefractionRemarks_RightEye, @DistanceVision_LeftEye_Unaided,   
      @DistanceVision_LeftEye_WithGlasses, @DistanceVision_LeftEye_PinHole, @NearVision_LeftEye,   
      @NeedCycloRefraction_LeftEye, @NeedCycloRefractionRemarks_LeftEye, @Right_Spherical_Status,   
      @Right_Spherical_Points, @Right_Cyclinderical_Status, @Right_Cyclinderical_Points, @Right_Axix_From,   
      @Right_Axix_To, @Right_Near_Status, @Right_Near_Points, @Left_Spherical_Status, @Left_Spherical_Points,   
      @Left_Cyclinderical_Status, @Left_Cyclinderical_Points, @Left_Axix_From, @Left_Axix_To, @Left_Near_Status,   
      @Left_Near_Points, @Douchrome, @Achromatopsia, @RetinoScopy_RightEye,    
      @Condition_RightEye, @Meridian1_RightEye, @Meridian2_RightEye, @FinalPrescription_RightEye,   
      @RetinoScopy_LeftEye, @Condition_LeftEye, @Meridian1_LeftEye,   
      @Meridian2_LeftEye, @FinalPrescription_LeftEye, @Hirchberg_Distance, @Hirchberg_Near, @Ophthalmoscope_RightEye,   
      @PupillaryReactions_RightEye, @CoverUncovertTest_RightEye, @CoverUncovertTestRemarks_RightEye,   
      @ExtraOccularMuscleRemarks_RightEye, @Ophthalmoscope_LeftEye, @PupillaryReactions_LeftEye,   
      @CoverUncovertTest_LeftEye, @CoverUncovertTestRemarks_LeftEye,   
      @CycloplegicRefraction_RightEye , @CycloplegicRefraction_LeftEye , @Conjunctivitis_RightEye , @Conjunctivitis_LeftEye ,  
    @Scleritis_RightEye ,@Scleritis_LeftEye , @Nystagmus_RightEye , @Nystagmus_LeftEye , @CornealDefect_RightEye , @CornealDefect_LeftEye ,  
    @Cataract_RightEye , @Cataract_LeftEye , @Keratoconus_RightEye , @Keratoconus_LeftEye , @Ptosis_RightEye , @Ptosis_LeftEye ,  
    @LowVision_RightEye , @LowVision_LeftEye , @Pterygium_RightEye , @Pterygium_LeftEye , @ColorBlindness_RightEye , @ColorBlindness_LeftEye ,  
    @Others_RightEye , @Others_LeftEye , @Fundoscopy_RightEye , @Fundoscopy_LeftEye , @Surgery_RightEye , @Surgery_LeftEye ,  
    @CataractSurgery_RightEye , @CataractSurgery_LeftEye , @SurgeryPterygium_RightEye , @SurgeryPterygium_LeftEye , @SurgeryCornealDefect_RightEye ,  
    @SurgeryCornealDefect_LeftEye , @SurgeryPtosis_RightEye , @SurgeryPtosis_LeftEye , @SurgeryKeratoconus_RightEye , @SurgeryKeratoconus_LeftEye ,  
    @Chalazion_RightEye , @Chalazion_LeftEye , @Hordeolum_RightEye , @Hordeolum_LeftEye , @SurgeryOthers_RightEye , @SurgeryOthers_LeftEye ,  
      @RightPupilDefects,@LeftPupilDefects,@UserId, GETDATE(), 'INSERT',   
      @EntryTerminal, @EntryTerminalIP, @ExtraOccularMuscleRemarks_LeftEye, @ApplicationID, @FormId,   
      @UserEmpId, @UserEmpName, @UserEmpCode, @VisualAcuity_RightEye, @VisualAcuity_LeftEye, @LeftSquint_VA ,@RightSquint_VA,@LeftAmblyopic_VA    
      ,@RightAmblyopic_VA ,@RightAmblyopia,@LeftAmblyopia,@LeftSquint_Surgery,@RightSquint_Surgery  
  
      SET @OptometristGothResidentId=SCOPE_IDENTITY()  
  
  SELECT @OptometristGothResidentId AS OptometristGothResidentId,'Successfully Saved' AS RESULT  
 END  
 ELSE IF @operation='Update'  
 BEGIN  
 UPDATE dbo.tblOptometristGothResident  
  SET    HasChiefComplain = @HasChiefComplain, ChiefComplainRemarks = @ChiefComplainRemarks, HasOccularHistory = @HasOccularHistory,   
           GothAutoId=@GothAutoId,OccularHistoryRemarks = @OccularHistoryRemarks, HasMedicalHistory = @HasMedicalHistory, MedicalHistoryRemarks = @MedicalHistoryRemarks,   
           DistanceVision_RightEye_Unaided = @DistanceVision_RightEye_Unaided, DistanceVision_RightEye_WithGlasses = @DistanceVision_RightEye_WithGlasses,   
           DistanceVision_RightEye_PinHole = @DistanceVision_RightEye_PinHole, NearVision_RightEye = @NearVision_RightEye,   
           NeedCycloRefraction_RightEye = @NeedCycloRefraction_RightEye, NeedCycloRefractionRemarks_RightEye = @NeedCycloRefractionRemarks_RightEye,   
           DistanceVision_LeftEye_Unaided = @DistanceVision_LeftEye_Unaided, DistanceVision_LeftEye_WithGlasses = @DistanceVision_LeftEye_WithGlasses,   
           DistanceVision_LeftEye_PinHole = @DistanceVision_LeftEye_PinHole, NearVision_LeftEye = @NearVision_LeftEye,   
           NeedCycloRefraction_LeftEye = @NeedCycloRefraction_LeftEye, NeedCycloRefractionRemarks_LeftEye = @NeedCycloRefractionRemarks_LeftEye,   
           Right_Spherical_Status = @Right_Spherical_Status, Right_Spherical_Points = @Right_Spherical_Points,   
           Right_Cyclinderical_Status = @Right_Cyclinderical_Status, Right_Cyclinderical_Points = @Right_Cyclinderical_Points,   
           Right_Axix_From = @Right_Axix_From, Right_Axix_To = @Right_Axix_To, Right_Near_Status = @Right_Near_Status,   
           Right_Near_Points = @Right_Near_Points, Left_Spherical_Status = @Left_Spherical_Status, Left_Spherical_Points = @Left_Spherical_Points,   
           Left_Cyclinderical_Status = @Left_Cyclinderical_Status, Left_Cyclinderical_Points = @Left_Cyclinderical_Points,   
           Left_Axix_From = @Left_Axix_From, Left_Axix_To = @Left_Axix_To, Left_Near_Status = @Left_Near_Status,   
           Left_Near_Points = @Left_Near_Points, Douchrome = @Douchrome, Achromatopsia = @Achromatopsia,   
           RetinoScopy_RightEye = @RetinoScopy_RightEye, CycloplegicRefraction_RightEye = @CycloplegicRefraction_RightEye,   
           Condition_RightEye = @Condition_RightEye, Meridian1_RightEye = @Meridian1_RightEye, Meridian2_RightEye = @Meridian2_RightEye,   
           FinalPrescription_RightEye = @FinalPrescription_RightEye, RetinoScopy_LeftEye = @RetinoScopy_LeftEye,   
            Condition_LeftEye = @Condition_LeftEye,   
           Meridian1_LeftEye = @Meridian1_LeftEye, Meridian2_LeftEye = @Meridian2_LeftEye, FinalPrescription_LeftEye = @FinalPrescription_LeftEye,   
           Hirchberg_Distance = @Hirchberg_Distance, Hirchberg_Near = @Hirchberg_Near, Ophthalmoscope_RightEye = @Ophthalmoscope_RightEye,   
           PupillaryReactions_RightEye = @PupillaryReactions_RightEye, CoverUncovertTest_RightEye = @CoverUncovertTest_RightEye,   
           CoverUncovertTestRemarks_RightEye = @CoverUncovertTestRemarks_RightEye, ExtraOccularMuscleRemarks_RightEye = @ExtraOccularMuscleRemarks_RightEye,   
           Ophthalmoscope_LeftEye = @Ophthalmoscope_LeftEye, PupillaryReactions_LeftEye = @PupillaryReactions_LeftEye,   
           CoverUncovertTest_LeftEye = @CoverUncovertTest_LeftEye, CoverUncovertTestRemarks_LeftEye = @CoverUncovertTestRemarks_LeftEye,  
      CycloplegicRefraction_LeftEye = @CycloplegicRefraction_LeftEye    
      ,Conjunctivitis_RightEye =  @Conjunctivitis_RightEye  
      ,Conjunctivitis_LeftEye =  @Conjunctivitis_LeftEye  
      ,Scleritis_RightEye =   @Scleritis_RightEye  
      ,Scleritis_LeftEye =   @Scleritis_LeftEye  
      ,Nystagmus_RightEye =   @Nystagmus_RightEye  
      ,Nystagmus_LeftEye =   @Nystagmus_LeftEye  
      ,CornealDefect_RightEye =  @CornealDefect_RightEye  
      ,CornealDefect_LeftEye =  @CornealDefect_LeftEye  
      ,Cataract_RightEye =   @Cataract_RightEye  
      ,Cataract_LeftEye =    @Cataract_LeftEye  
      ,Keratoconus_RightEye =   @Keratoconus_RightEye  
      ,Keratoconus_LeftEye =    @Keratoconus_LeftEye  
      ,Ptosis_RightEye =    @Ptosis_RightEye  
      ,Ptosis_LeftEye =    @Ptosis_LeftEye  
      ,LowVision_RightEye =   @LowVision_RightEye  
      ,LowVision_LeftEye =   @LowVision_LeftEye  
      ,Pterygium_RightEye =   @Pterygium_RightEye  
      ,Pterygium_LeftEye =   @Pterygium_LeftEye  
      ,ColorBlindness_RightEye =  @ColorBlindness_RightEye  
      ,ColorBlindness_LeftEye =  @ColorBlindness_LeftEye  
      ,Others_RightEye =    @Others_RightEye  
   ,RightPupilDefects =    @RightPupilDefects,  
   LeftPupilDefects  =    @LeftPupilDefects  
      ,Others_LeftEye =    @Others_LeftEye  
      ,Fundoscopy_RightEye =   @Fundoscopy_RightEye  
      ,Fundoscopy_LeftEye =    @Fundoscopy_LeftEye  
      ,Surgery_RightEye =    @Surgery_RightEye  
      ,Surgery_LeftEye =    @Surgery_LeftEye  
      ,CataractSurgery_RightEye =  @CataractSurgery_RightEye  
      ,CataractSurgery_LeftEye =  @CataractSurgery_LeftEye  
      ,SurgeryPterygium_RightEye = @SurgeryPterygium_RightEye  
      ,SurgeryPterygium_LeftEye =  @SurgeryPterygium_LeftEye  
      ,SurgeryCornealDefect_RightEye =@SurgeryCornealDefect_RightEye  
      ,SurgeryCornealDefect_LeftEye = @SurgeryCornealDefect_LeftEye  
      ,SurgeryPtosis_RightEye =  @SurgeryPtosis_RightEye  
      ,SurgeryPtosis_LeftEye =  @SurgeryPtosis_LeftEye  
      ,SurgeryKeratoconus_RightEye = @SurgeryKeratoconus_RightEye  
      ,SurgeryKeratoconus_LeftEye = @SurgeryKeratoconus_LeftEye  
      ,Chalazion_RightEye =   @Chalazion_RightEye  
      ,Chalazion_LeftEye =   @Chalazion_LeftEye  
      ,Hordeolum_RightEye =   @Hordeolum_RightEye  
      ,Hordeolum_LeftEye =   @Hordeolum_LeftEye  
   ,LeftAmblyopia =    @LeftAmblyopia  
   ,RightAmblyopia =    @RightAmblyopia  
      ,SurgeryOthers_RightEye =  @SurgeryOthers_RightEye  
      ,SurgeryOthers_LeftEye =  @SurgeryOthers_LeftEye,  
   LeftSquint_Surgery =  @LeftSquint_Surgery,  
   RightSquint_Surgery =  @RightSquint_Surgery,  
           UserId = @UserId ,ExtraOccularMuscleRemarks_LeftEye = @ExtraOccularMuscleRemarks_LeftEye,   
           ApplicationID = @ApplicationID, FormId = @FormId, UserEmpId = @UserEmpId, UserEmpName = @UserEmpName,   
           UserEmpCode = @UserEmpCode, VisualAcuity_RightEye = @VisualAcuity_RightEye, VisualAcuity_LeftEye = @VisualAcuity_LeftEye,   
           LeftSquint_VA =@LeftSquint_VA ,RightSquint_VA=@RightSquint_VA,LeftAmblyopic_VA =@LeftAmblyopic_VA,RightAmblyopic_VA =@RightAmblyopic_VA,  
     EntOperation='UPDATE'  
  WHERE  OptometristGothResidentId = @OptometristGothResidentId   
  
   SELECT @OptometristGothResidentId  AS OptometristGothResidentId,'Successfully Updated' AS RESULT  
  
 END  
  
 ELSE IF @operation='GetById'  
 BEGIN  
       
SELECT ISNULL(OptometristGothResidentId,'0')OptometristGothResidentId  
      ,ISNULL(OptometristGothResidentTransDate,GETDATE())OptometristGothResidentTransDate  
      ,ISNULL(ResidentAutoId,'0')ResidentAutoId  
      ,ISNULL(GothAutoId,'0')GothAutoId  
      ,ISNULL(HasChiefComplain,'0')HasChiefComplain  
      ,ISNULL(ChiefComplainRemarks,'')ChiefComplainRemarks  
      ,ISNULL(HasOccularHistory,'0')HasOccularHistory  
      ,ISNULL(OccularHistoryRemarks,'')OccularHistoryRemarks  
      ,ISNULL(HasMedicalHistory,'0')HasMedicalHistory  
      ,ISNULL(MedicalHistoryRemarks,'')MedicalHistoryRemarks  
      ,ISNULL(DistanceVision_RightEye_Unaided,'0')DistanceVision_RightEye_Unaided  
      ,ISNULL(DistanceVision_RightEye_WithGlasses,'0')DistanceVision_RightEye_WithGlasses  
      ,ISNULL(DistanceVision_RightEye_PinHole,'0')DistanceVision_RightEye_PinHole  
      ,ISNULL(NearVision_RightEye,'0')NearVision_RightEye  
      ,ISNULL(NeedCycloRefraction_RightEye,'0')NeedCycloRefraction_RightEye  
      ,ISNULL(NeedCycloRefractionRemarks_RightEye,'')NeedCycloRefractionRemarks_RightEye  
      ,ISNULL(DistanceVision_LeftEye_Unaided,'0')DistanceVision_LeftEye_Unaided  
      ,ISNULL(DistanceVision_LeftEye_WithGlasses,'0')DistanceVision_LeftEye_WithGlasses  
      ,ISNULL(DistanceVision_LeftEye_PinHole,'0')DistanceVision_LeftEye_PinHole  
      ,ISNULL(NearVision_LeftEye,'0')NearVision_LeftEye  
      ,ISNULL(NeedCycloRefraction_LeftEye,'0')NeedCycloRefraction_LeftEye  
      ,ISNULL(NeedCycloRefractionRemarks_LeftEye,'')NeedCycloRefractionRemarks_LeftEye  
      ,ISNULL(Right_Spherical_Status,'0')Right_Spherical_Status  
      ,ISNULL(Right_Spherical_Points,'0')Right_Spherical_Points  
      ,ISNULL(Right_Cyclinderical_Status,'0')Right_Cyclinderical_Status  
      ,ISNULL(Right_Cyclinderical_Points,'0')Right_Cyclinderical_Points  
      ,ISNULL(Right_Axix_From,'0')Right_Axix_From  
      ,ISNULL(Right_Axix_To,'0')Right_Axix_To  
      ,ISNULL(Right_Near_Status,'P')Right_Near_Status  
      ,ISNULL(Right_Near_Points,'0')Right_Near_Points  
      ,ISNULL(Left_Spherical_Status,'P')Left_Spherical_Status  
      ,ISNULL(Left_Spherical_Points,'0')Left_Spherical_Points  
      ,ISNULL(Left_Cyclinderical_Status,'P')Left_Cyclinderical_Status  
      ,ISNULL(Left_Cyclinderical_Points,'0')Left_Cyclinderical_Points  
      ,ISNULL(Left_Axix_From,'0')Left_Axix_From  
      ,ISNULL(Left_Axix_To,'0')Left_Axix_To  
      ,ISNULL(Left_Near_Status,'')Left_Near_Status  
      ,ISNULL(Left_Near_Points,'0')Left_Near_Points  
      ,ISNULL(VisualAcuity_RightEye,'0')VisualAcuity_RightEye  
      ,ISNULL(VisualAcuity_LeftEye,'0')VisualAcuity_LeftEye  
      ,ISNULL(LeftSquint_VA,'0')LeftSquint_VA  
      ,ISNULL(RightSquint_VA,'0')RightSquint_VA  
      ,ISNULL(LeftAmblyopic_VA,'0')LeftAmblyopic_VA  
      ,ISNULL(RightAmblyopic_VA,'0')RightAmblyopic_VA  
      ,ISNULL(AutoRefResidentId,'0')AutoRefResidentId  
      ,ISNULL(Hirchberg_Distance,'0')Hirchberg_Distance  
      ,ISNULL(Hirchberg_Near,'0')Hirchberg_Near  
      ,ISNULL(Ophthalmoscope_RightEye,'0')Ophthalmoscope_RightEye  
      ,ISNULL(Ophthalmoscope_LeftEye,'0')Ophthalmoscope_LeftEye  
      ,ISNULL(PupillaryReactions_RightEye,'0')PupillaryReactions_RightEye  
      ,ISNULL(CoverUncovertTest_RightEye,'0')CoverUncovertTest_RightEye  
      ,ISNULL(CoverUncovertTestRemarks_RightEye,'')CoverUncovertTestRemarks_RightEye  
      ,ISNULL(ExtraOccularMuscleRemarks_RightEye,'')ExtraOccularMuscleRemarks_RightEye  
      ,ISNULL(PupillaryReactions_LeftEye,'0')PupillaryReactions_LeftEye  
      ,ISNULL(CoverUncovertTest_LeftEye,'0')CoverUncovertTest_LeftEye  
      ,ISNULL(CoverUncovertTestRemarks_LeftEye,'0')CoverUncovertTestRemarks_LeftEye  
      ,ISNULL(CycloplegicRefraction_RightEye,'0')CycloplegicRefraction_RightEye  
      ,ISNULL(CycloplegicRefraction_LeftEye,'0')CycloplegicRefraction_LeftEye  
      ,ISNULL(Conjunctivitis_RightEye,'0')Conjunctivitis_RightEye  
      ,ISNULL(Conjunctivitis_LeftEye,'0')Conjunctivitis_LeftEye  
      ,ISNULL(Scleritis_RightEye,'0')Scleritis_RightEye  
      ,ISNULL(Scleritis_LeftEye,'0')Scleritis_LeftEye  
      ,ISNULL(Nystagmus_RightEye,'0')Nystagmus_RightEye  
      ,ISNULL(Nystagmus_LeftEye,'0')Nystagmus_LeftEye  
      ,ISNULL(CornealDefect_RightEye,'0')CornealDefect_RightEye  
      ,ISNULL(CornealDefect_LeftEye,'0')CornealDefect_LeftEye  
      ,ISNULL(Cataract_RightEye,'0')Cataract_RightEye  
      ,ISNULL(Cataract_LeftEye,'0')Cataract_LeftEye  
      ,ISNULL(Keratoconus_RightEye,'0')Keratoconus_RightEye  
      ,ISNULL(Keratoconus_LeftEye,'0')Keratoconus_LeftEye  
      ,ISNULL(Ptosis_RightEye,'0')Ptosis_RightEye  
      ,ISNULL(Ptosis_LeftEye,'0')Ptosis_LeftEye  
      ,ISNULL(LowVision_RightEye,'0')LowVision_RightEye  
      ,ISNULL(LowVision_LeftEye,'0')LowVision_LeftEye  
      ,ISNULL(Pterygium_RightEye,'0')Pterygium_RightEye  
      ,ISNULL(Pterygium_LeftEye,'0')Pterygium_LeftEye  
      ,ISNULL(ColorBlindness_RightEye,'0')ColorBlindness_RightEye  
      ,ISNULL(ColorBlindness_LeftEye,'0')ColorBlindness_LeftEye  
      ,ISNULL(Others_RightEye,'0')Others_RightEye  
      ,ISNULL(Others_LeftEye,'0')Others_LeftEye  
      ,ISNULL(Fundoscopy_RightEye,'0')Fundoscopy_RightEye  
      ,ISNULL(Fundoscopy_LeftEye,'0')Fundoscopy_LeftEye  
      ,ISNULL(Surgery_RightEye,'0')Surgery_RightEye  
      ,ISNULL(Surgery_LeftEye,'0')Surgery_LeftEye  
      ,ISNULL(CataractSurgery_RightEye,'0')CataractSurgery_RightEye  
      ,ISNULL(CataractSurgery_LeftEye,'0')CataractSurgery_LeftEye  
      ,ISNULL(SurgeryPterygium_RightEye,'0')SurgeryPterygium_RightEye  
      ,ISNULL(SurgeryPterygium_LeftEye,'0')SurgeryPterygium_LeftEye  
      ,ISNULL(SurgeryCornealDefect_RightEye,'0')SurgeryCornealDefect_RightEye  
      ,ISNULL(SurgeryCornealDefect_LeftEye,'0')SurgeryCornealDefect_LeftEye  
      ,ISNULL(SurgeryPtosis_RightEye,'0')SurgeryPtosis_RightEye  
      ,ISNULL(SurgeryPtosis_LeftEye,'0')SurgeryPtosis_LeftEye  
      ,ISNULL(SurgeryKeratoconus_RightEye,'0')SurgeryKeratoconus_RightEye  
      ,ISNULL(SurgeryKeratoconus_LeftEye,'0')SurgeryKeratoconus_LeftEye  
      ,ISNULL(Chalazion_RightEye,'0')Chalazion_RightEye  
      ,ISNULL(Chalazion_LeftEye,'0')Chalazion_LeftEye  
      ,ISNULL(Hordeolum_RightEye,'0')Hordeolum_RightEye  
      ,ISNULL(Hordeolum_LeftEye,'0')Hordeolum_LeftEye  
      ,ISNULL(SurgeryOthers_RightEye,'0')SurgeryOthers_RightEye  
      ,ISNULL(SurgeryOthers_LeftEye,'0')SurgeryOthers_LeftEye  
      ,ISNULL(Douchrome,'0')Douchrome  
      ,ISNULL(Achromatopsia,'0')Achromatopsia  
      ,ISNULL(RetinoScopy_RightEye,'0')RetinoScopy_RightEye  
      ,ISNULL(Condition_RightEye,'0')Condition_RightEye  
      ,ISNULL(Meridian1_RightEye,'')Meridian1_RightEye  
      ,ISNULL(Meridian2_RightEye,'')Meridian2_RightEye  
      ,ISNULL(FinalPrescription_RightEye,'')FinalPrescription_RightEye  
      ,ISNULL(RetinoScopy_LeftEye,'0')RetinoScopy_LeftEye  
      ,ISNULL(Condition_LeftEye,'')Condition_LeftEye  
      ,ISNULL(Meridian1_LeftEye,'')Meridian1_LeftEye  
      ,ISNULL(Meridian2_LeftEye,'')Meridian2_LeftEye  
      ,ISNULL(FinalPrescription_LeftEye,'')FinalPrescription_LeftEye  
   ,ISNULL(RightPupilDefects,0)RightPupilDefects  
   ,ISNULL(LeftPupilDefects,0)LeftPupilDefects  
   ,ISNULL(LeftSquint_Surgery,0)LeftSquint_Surgery  
   ,ISNULL(RightSquint_Surgery,0)RightSquint_Surgery  
      ,ISNULL(UserId,'0')UserId  
      ,ISNULL(EntDate,Getdate())  
      ,ISNULL(EntOperation,'0')  
      ,ISNULL(EntTerminal,'0')  
      ,ISNULL(EntTerminalIP,'0')  
      ,ISNULL(ExtraOccularMuscleRemarks_LeftEye,'')ExtraOccularMuscleRemarks_LeftEye,  
   ISNULL(RightAmblyopia,0)RightAmblyopia,ISNULL(LeftAmblyopia,0)LeftAmblyopia  
      ,ISNULL(ApplicationID,'0')  
      ,ISNULL(FormId,'0')  
      ,ISNULL(UserEmpId,'0')  
      ,ISNULL(UserEmpName,'0')  
      ,ISNULL(UserEmpCode,'0')  
    FROM   dbo.tblOptometristGothResident  
    WHERE  OptometristGothResidentId = @OptometristGothResidentId   
 END  
  
 ELSE IF @operation = 'GetListForGothResidentOptometrist'  
 BEGIN  
 SELECT DISTINCT TOP 10 cw.ResidentAutoId,cw.ResidentCode,cw.ResidentName,c.GothName,cw.CNIC,cw.MobileNo  
 FROM tblGoths  c  
 INNER JOIN tblGothsResident   cw ON c.GothAutoId=cw.GothAutoId  
 INNER JOIN tblGothAutoRefTestResident  artw ON cw.ResidentAutoId=artw.ResidentAutoId  
 ORDER BY cw.ResidentAutoId desc  
 END  
    
  ELSE IF @operation = 'Search'  
 BEGIN  
 SELECT DISTINCT TOP 10 c.ResidentAutoId,c.ResidentCode,c.ResidentName,A.GothName,c.CNIC,c.MobileNo  
     FROM tblGoths A INNER JOIN tblGothsResident c ON A.GothAutoId = c.GothAutoId  
     INNER JOIN tblGothAutoRefTestResident artw ON c.ResidentAutoId=artw.ResidentAutoId  
   WHERE  
   (  
   CAST(c.ResidentCode AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR  
   CAST(A.GothName AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR  
   CAST(c.ResidentName AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR  
   CAST(c.CNIC AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR  
   CAST(c.MobileNo AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'      
   )  
   ORDER BY c.ResidentAutoId desc  
 END  
  
   
ELSE IF @operation='NewResidentForOpt'  
BEGIN  
 SELECT DISTINCT TOP 5 cw.ResidentAutoId Id,cw.ResidentName+' | '+cw.ResidentCode text,cw.ResidentCode Code FROM tblGothsResident cw  
 INNER JOIN tblGothAutoRefTestResident  artr ON cw.ResidentAutoId = artr.ResidentAutoId
 WHERE   
 cw.GothAutoId=@GothAutoId AND   
 cw.ResidentAutoId NOT IN (SELECT artw.ResidentAutoId FROM tblOptometristGothResident artw WITH (NOLOCK) WHERE CAST(artw.OptometristGothResidentTransDate  AS DATE) = CAST(@OptometristGothResidentTransDate AS DATE))  
 AND  (cw.ResidentName LIKE '%'+ISNULL('','')+ '%' OR  
    cw.ResidentCode LIKE '%'+ISNULL('','')+ '%' )  
 ORDER BY cw.ResidentName+' | '+cw.ResidentCode    
END  
  
ELSE IF @operation='EditResidentForOpt'  
BEGIN  
 SELECT DISTINCT TOP 5 cw.ResidentAutoId Id,cw.ResidentName+' | '+cw.ResidentCode text,cw.ResidentCode Code   
 FROM tblGothsResident cw INNER JOIN tblOptometristGothResident artw ON cw.ResidentAutoId=artw.ResidentAutoId  
 WHERE   
 cw.GothAutoId=@GothAutoId   
 AND  (cw.ResidentName LIKE '%'+ISNULL('','')+ '%' OR  
    cw.ResidentCode LIKE '%'+ISNULL('','')+ '%' )  
 ORDER BY cw.ResidentName+' | '+cw.ResidentCode  
END  
  
 ELSE IF @operation= 'GetDatesofResident'  
 BEGIN  
  SELECT CAST(FORMAT(artw.OptometristGothResidentTransDate ,'dd | MMM | yyyy') AS VARCHAR) Text,  
  artw.OptometristGothResidentId AS Id  
  FROM tblOptometristGothResident artw  
  WHERE artw.ResidentAutoId=@ResidentAutoId  
  ORDER BY artw.OptometristGothResidentId desc  
 END  
  
 ELSE IF @operation= 'DeleteOptometristById'  
 BEGIN  
  DELETE FROM tblOptometristGothResident WHERE OptometristGothResidentId=@OptometristGothResidentId  
  SELECT @OptometristGothResidentId AS OptometristGothResidentId,'Successfully Deleted' AS RESULT  
 END  
   
    
GO

 

   
CREATE OR ALTER PROC [dbo].[Sp_GothGlassDispenseResident]          
    @GlassDispenseResidentId INT = NULL,          
  @OptometristGothResidentId INT =NULL,      
    @GlassDispenseResidentTransDate DATETIME   = NULL,          
    @ResidentAutoId INT   = NULL,          
    @VisionwithGlasses_RightEye INT= NULL,          
    @VisionwithGlasses_LeftEye int= NULL,          
 @NearVA_RightEye INT=NULL,        
 @NearVA_LeftEye INT=NULL,        
    @ResidentSatisficaion int= NULL,          
    @Unsatisfied int= NULL,          
    @Unsatisfied_Remarks nvarchar(250)= NULL,          
    @Unsatisfied_Reason int= NULL,          
 @GothAutoId INT =NULL,        
    @UserId nvarchar(250)= NULL,          
    @EntDate datetime= NULL,          
    @EntOperation nvarchar(100)= NULL,          
    @EntryTerminal  nvarchar(200)= NULL,          
    @EntryTerminalIP nvarchar(200) =NULL,          
 @UserEmpName NVARCHAR(200)=NULL,        
 @SearchText NVARCHAR(200)=NULL,        
 @Operation VARCHAR(50)=NULL          
AS           
 IF @operation='Save'          
BEGIN          
IF NOT EXISTS(SELECT 1 FROM tblGothGlassDispenseResident artw WHERE artw.ResidentAutoId=@ResidentAutoId AND CAST(artw.GlassDispenseResidentTransDate AS DATE)=CAST(@GlassDispenseResidentTransDate AS date))          
 BEGIN          
          
INSERT INTO [tblGothGlassDispenseResident] (      
           OptometristGothResidentId,GlassDispenseResidentTransDate, ResidentAutoId,       
           VisionwithGlasses_RightEye, VisionwithGlasses_LeftEye,NearVA_RightEye, NearVA_LeftEye,      
           ResidentSatisficaion, Unsatisfied, Unsatisfied_Remarks, Unsatisfied_Reason,      
           UserId, EntDate, EntOperation, EntTerminal, EntTerminalIP)      
 SELECT @OptometristGothResidentId,@GlassDispenseResidentTransDate, @ResidentAutoId, @VisionwithGlasses_RightEye,           
   @VisionwithGlasses_LeftEye, @NearVA_RightEye,@NearVA_LeftEye,@ResidentSatisficaion,       
   @Unsatisfied, @Unsatisfied_Remarks, @Unsatisfied_Reason,              
   @UserId, GETDATE(), @EntOperation, @EntryTerminal ,           
   @EntryTerminalIP          
          
      SET @GlassDispenseResidentId = SCOPE_IDENTITY()          
     SELECT @GlassDispenseResidentId  AS GlassDispenseResidentId,'Successfully Saved' AS RESULT          
 END          
 ELSE          
    SELECT @GlassDispenseResidentId  AS GlassDispenseResidentId,'Resident Glass Dispense Already Exists in selected Date' AS RESULT          
            
END          
ELSE IF @operation='UPDATE'          
BEGIN          
   UPDATE dbo.tblGothGlassDispenseResident          
  SET            
      VisionwithGlasses_RightEye = @VisionwithGlasses_RightEye, VisionwithGlasses_LeftEye = @VisionwithGlasses_LeftEye,         
    NearVA_RightEye=@NearVA_RightEye,NearVA_LeftEye=@NearVA_LeftEye,        
      ResidentSatisficaion = @ResidentSatisficaion, Unsatisfied = @Unsatisfied, Unsatisfied_Remarks = @Unsatisfied_Remarks,           
      Unsatisfied_Reason = @Unsatisfied_Reason,      
      UserId = @UserId,  EntOperation = @EntOperation, EntTerminal = @EntryTerminal ,           
      EntTerminalIP = @EntryTerminalIP          
    WHERE  GlassDispenseResidentId = @GlassDispenseResidentId          
  SELECT @GlassDispenseResidentId AS GlassDispenseResidentId,'Successfully Updated' AS RESULT         
END          
          
ELSE IF @operation='GetById'          
BEGIN          
          
    SELECT a.OptometristGothResidentId,      
     GlassDispenseResidentId,GlassDispenseResidentTransDate, a.ResidentAutoId,       
     VisionwithGlasses_RightEye, VisionwithGlasses_LeftEye,         
   NearVA_RightEye,NearVA_LeftEye,        
           ResidentSatisficaion, Unsatisfied, Unsatisfied_Remarks, Unsatisfied_Reason,      
     a.UserId, a.EntDate, a.EntOperation, a.EntTerminal, a.EntTerminalIP,          
     cw.Distance,cw.Near,cw.WearGlasses          
    FROM   dbo.tblGothGlassDispenseResident a WITH (NOLOCK)          
 INNER JOIN tblGothsResident cw ON a.ResidentAutoId = cw.ResidentAutoId          
  
    WHERE  GlassDispenseResidentId = @GlassDispenseResidentId          
END          
          
ELSE IF @operation='DeleteById'          
BEGIN          
 DELETE FROM tblGothGlassDispenseResident WHERE GlassDispenseResidentId=@GlassDispenseResidentId          
  SELECT @GlassDispenseResidentId  AS GlassDispenseResidentId,'Successfully Deleted' AS RESULT          
END           
        
        
ELSE IF @Operation ='GetDatesofGlassDispenseResident'          
BEGIN          
 SELECT CAST(FORMAT(artw.GlassDispenseResidentTransDate,'dd | MMM | yyyy') AS VARCHAR) Text,          
   artw.GlassDispenseResidentId AS Id          
   FROM tblGothGlassDispenseResident artw          
   WHERE artw.ResidentAutoId=@ResidentAutoId          
   ORDER BY artw.GlassDispenseResidentId desc          
END          
        
        
ELSE IF @operation='NewResidentForGlassDispense'          
BEGIN          
 SELECT DISTINCT top 5 cw.ResidentAutoId Id, cw.ResidentName +' | '+cw.ResidentCode  text,cw.ResidentCode Code FROM tblGothsResident cw          
 INNER JOIN tblOptometristGothResident ow ON cw.ResidentAutoId = ow.ResidentAutoId AND ow.OptometristGothResidentTransDate <= CAST(@GlassDispenseResidentTransDate AS DATE)
 WHERE           
 cw.GothAutoId=@GothAutoId AND           
 cw.ResidentAutoId NOT IN         
   (SELECT artw.ResidentAutoId FROM tblGothGlassDispenseResident  artw WITH (NOLOCK) WHERE CAST(artw.GlassDispenseResidentTransDate AS DATE) = CAST(@GlassDispenseResidentTransDate AS DATE))          
 AND           
 (cw.ResidentCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR          
 cw.ResidentName LIKE '%'+ISNULL(@SearchText,'')+ '%' )          
 ORDER BY  cw.ResidentName +' | '+cw.ResidentCode           
END          
          
ELSE IF @operation='EditResidentForGlassDispense'          
BEGIN          
 SELECT DISTINCT TOP 5 cw.ResidentAutoId Id, cw.ResidentName +' | '+cw.ResidentCode  text,cw.ResidentCode Code           
 FROM tblGothsResident cw INNER JOIN tblGothGlassDispenseResident artw ON cw.ResidentAutoId=artw.ResidentAutoId          
 WHERE           
 cw.GothAutoId=@GothAutoId           
 AND (cw.ResidentCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR          
 cw.ResidentName LIKE '%'+ISNULL(@SearchText,'')+ '%' )          
 ORDER BY  cw.ResidentName +' | '+cw.ResidentCode           
END          
      
ELSE IF @operation='GetGlassDispenseHistoryByResidentId '      
 BEGIN      
  SELECT TOP 1 artw.OptometristGothResidentId,CAST(FORMAT(artw.OptometristGothResidentTransDate,'dd | MMM | yyyy') AS VARCHAR) AS Last_Visit_Date,      
   CASE WHEN artw.Right_Spherical_Status ='P' AND artw.Right_Spherical_Points>0 THEN '+' +CAST(Right_Spherical_Points AS VARCHAR)      
   WHEN artw.Right_Spherical_Status='N'AND artw.Right_Spherical_Points>0 THEN '-' +CAST(Right_Spherical_Points AS VARCHAR)      
   WHEN artw.Right_Spherical_Status='E' THEN 'Error '+CAST(ISNULL(Right_Spherical_Points,'') AS VARCHAR)      
   WHEN artw.Right_Spherical_Status='l' THEN 'Plano '+CAST(ISNULL(Right_Spherical_Points,'') AS VARCHAR)      
   ELSE CAST(Right_Spherical_Points AS VARCHAR) END 'Right Spherical',      
      
   CASE WHEN artw.Left_Spherical_Status ='P' AND artw.Left_Spherical_Points>0 THEN '+' +CAST(Left_Spherical_Points AS VARCHAR)      
   WHEN artw.Left_Spherical_Status='N'AND artw.Left_Spherical_Points>0 THEN '-' +CAST(Left_Spherical_Points AS VARCHAR)      
    WHEN artw.Left_Spherical_Status='E' THEN 'Error '+CAST(ISNULL(Left_Spherical_Points,'') AS VARCHAR)      
   WHEN artw.Left_Spherical_Status='l' THEN 'Plano '+CAST(ISNULL(Left_Spherical_Points,'') AS VARCHAR)      
   ELSE CAST(Left_Spherical_Points AS VARCHAR) END 'Left Spherical',      
      
   CASE WHEN artw.Right_Spherical_Status ='P' AND artw.Left_Spherical_Points>0 THEN '+' +CAST(Left_Spherical_Points AS VARCHAR)      
   WHEN artw.Right_Spherical_Status='N'AND artw.Right_Spherical_Points>0 THEN '-' +CAST(Right_Spherical_Points AS VARCHAR)      
   ELSE CAST(Right_Spherical_Points AS VARCHAR) END 'Right Spherical',      
      
   CASE WHEN artw.Right_Cyclinderical_Status ='P' AND artw.Right_Cyclinderical_Points>0 THEN '+' +CAST(Right_Cyclinderical_Points AS VARCHAR)      
   WHEN artw.Right_Cyclinderical_Status='N'AND artw.Right_Cyclinderical_Points>0 THEN '-' +CAST(Right_Cyclinderical_Points AS VARCHAR)      
   ELSE CAST(Right_Cyclinderical_Points AS VARCHAR) END 'Right Cyclinderical',       
      
         
   CASE WHEN artw.Left_Cyclinderical_Status ='P' AND artw.Left_Cyclinderical_Points>0 THEN '+' +CAST(Left_Cyclinderical_Points AS VARCHAR)      
   WHEN artw.Left_Cyclinderical_Status='N'AND artw.Left_Cyclinderical_Points>0 THEN '-' +CAST(Left_Cyclinderical_Points AS VARCHAR)      
   ELSE CAST(artw.Left_Cyclinderical_Points AS VARCHAR) END 'Left Cyclinderical',       
   artw.Right_Axix_From 'Right Axis' ,      
    artw.Left_Axix_From 'Left Axis' ,ISNULL(cw.WearGlasses,0)WearGlasses,ISNULL(cw.Near,0)Near,      
    ISNULL(cw.Distance,0) Distance,  ISNULL(artw.IPD,0)IPD,CASE WHEN cw.GenderAutoId=1 THEN 'Male' ELSE 'Female' END Gender,      
    cw.Age      
      
  FROM tblOptometristGothResident   artw      
  INNER JOIN tblGothsResident  cw ON artw.ResidentAutoId = cw.ResidentAutoId      
  WHERE  artw.ResidentAutoId=@ResidentAutoId AND      
  artw.OptometristGothResidentTransDate <= CAST(@GlassDispenseResidentTransDate AS DATE)       
  ORDER BY artw.OptometristGothResidentId desc      
 END      
          
    /*          
    -- Begin Return row code block          
          
    SELECT GlassDispenseResidentTransDate, ResidentAutoId, VisionwithGlasses_RightEye, VisionwithGlasses_LeftEye,           
           ResidentSatisficaion, Unsatisfied, Unsatisfied_Remarks, Unsatisfied_Reason, Right_Spherical_Status,           
           Right_Spherical_Points, Right_Cyclinderical_Status, Right_Cyclinderical_Points, Right_Axix_From,           
           Right_Axix_To, Right_Near_Status, Right_Near_Points, Left_Spherical_Status, Left_Spherical_Points,           
           Left_Cyclinderical_Status, Left_Cyclinderical_Points, Left_Axix_From, Left_Axix_To, Left_Near_Status,           
           Left_Near_Points, FollowupRequired, TreatmentId, Medicines, Prescription, ProvideGlasses,           
           ReferToHospital, UserId, EntDate, EntOperation, EntTerminal, EntTerminalIP          
    FROM   dbo.tblGothGlassDispenseResident          
    WHERE  GlassDispenseResidentId = @GlassDispenseResidentId          
          
    -- End Return row code block          
          
    */   
GO


 
   
CREATE OR ALTER PROC [dbo].[Sp_SetupLocality]   
    @LocalityAutoId INT       =NULL ,  
    @LocalityCode nvarchar(10) =NULL,  
    @LocalityName nvarchar(250) =NULL,  
 @Website nvarchar(250) =null,  
    @Address1 nvarchar(1000) =NULL,  
    @Address2 nvarchar(1000) =NULL,  
 @Address3 nvarchar(1000) = NULL,  
    @District nvarchar(1000) =NULL,  
 @Town nvarchar(200) =NULL,  
    @City nvarchar(100) =NULL,  
 @WorkForce INT = null,  
    @NameofPerson varchar(100) =NULL,  
    @PersonMobile varchar(50) =NULL,  
    @PersonRole varchar(100) =NULL,  
 @TitleAutoId INT =NULL,  
    @UserId nvarchar(250) =NULL,  
    @EntDate DATETIME =NULL,  
    @EntOperation nvarchar(100) =NULL,  
    @FormId nvarchar(250) =NULL,  
    @UserEmpId INT =NULL,  
    @UserEmpName nvarchar(250) =NULL,  
    @UserEmpCode nvarchar(10) =NULL,  
    @EnrollmentDate DATETIME =NULL,  
 @operation VARCHAR(50)= NULL,  
 @EntryTerminal VARCHAR(200)=NULL,  
 @EntryTerminalIP VARCHAR(200)=NULL,  
 @SearchText VARCHAR(MAX)=NULL  
AS   
    IF @operation = 'Save'  
 Begin  
 IF NOT EXISTS(SELECT 1 FROM tblLocalities  l WHERE l.LocalityName=@LocalityName AND l.City=@City AND l.Address1=@Address1 )  
 BEGIN  
      
    INSERT INTO tblLocalities (LocalityCode, LocalityName, Website, Address1, Address2, Address3, Town, District, City,  
          NameofPerson, PersonMobile, PersonRole, TitleAutoId, UserId, EntDate,   
          EntOperation, EntTerminal, EntTerminalIP,  
          FormId, UserEmpId, UserEmpName, UserEmpCode, EnrollmentDate)  
          
  
    SELECT '',UPPER(@LocalityName),@Website, @Address1, @Address2,@Address3,@Town, @District, @City, @NameofPerson,   
 @PersonMobile, @PersonRole,@TitleAutoId, @UserId, GETDATE(), 'INSERT', @EntryTerminal, @EntryTerminalIP,   
           @FormId, @UserEmpId, @UserEmpName, @UserEmpCode, ISNULL(@EnrollmentDate,GETDATE())  
       
     SET @LocalityAutoId = SCOPE_IDENTITY()  
     SELECT @LocalityCode =MAX(CAST(cd.LocalityCode AS INT))+1 FROM tblLocalities  cd  
  
   UPDATE tblLocalities SET LocalityCode=CASE WHEN LEN(@LocalityCode)=1 THEN '00'+@LocalityCode  
   WHEN LEN(@LocalityCode )=2 THEN '0'+@LocalityCode  
   ELSE @LocalityCode END  
   WHERE LocalityAutoId=@LocalityAutoId  
  
   SELECT @LocalityAutoId AS LocalityAutoId,'Successfully Saved' AS RESULT  
  END  
  ELSE  
  SELECT @LocalityAutoId AS LocalityAutoId,'Locality with same detail Already Exists.' AS RESULT  
 END  
  
 ELSE IF @operation = 'Update'  
 BEGIN  
 UPDATE dbo.tblLocalities  
    SET    LocalityName = @LocalityName,Website=@Website, Address1 = @Address1, Address2 = @Address2,   
            Address3 = @Address3, Town = @Town, District = @District, City = @City,   
           NameofPerson=@NameofPerson,PersonMobile=@PersonMobile, PersonRole=@PersonRole,  
           UserId = @UserId, EntDate = @EntDate, EntOperation = 'Update', EntTerminal = @EntryTerminal,   
           EntTerminalIP = @EntryTerminalIP, FormId = @FormId, UserEmpId = @UserEmpId, UserEmpName = @UserEmpName,   
           UserEmpCode = @UserEmpCode  
    WHERE  LocalityAutoId = @LocalityAutoId  
 SELECT @LocalityAutoId AS LocalityAutoId,'Successfully Updated' AS RESULT  
 END  
      
 ELSE IF @operation='GetLocalityByiD'  
 BEGIN   
    SELECT c.LocalityAutoId, LocalityCode, LocalityName, Website, Address1, Address2, Address3, Town, District, City,  
 NameofPerson, PersonMobile, PersonRole, TitleAutoId, c.UserId, c.EntDate, c.EntOperation, c.EntTerminal,   
 c.EntTerminalIP, FormId, UserEmpId, UserEmpName, UserEmpCode, EnrollmentDate ,  
 ci.LocalityImageAutoId,ci.LocalityAutoId DetailLocalityImageAutoId,ci.LocalityPic,ci.FileType,ci.FileSize,ci.CaptureDate,ci.CaptureRemarks  
    FROM   dbo.tblLocalities AS c   LEFT JOIN tblLocalityImage  ci ON c.LocalityAutoId = ci.LocalityAutoId  
    WHERE  c.LocalityAutoId = @LocalityAutoId   
     
 END  
  
 ELSE IF @operation='GetAllLocality'  
 BEGIN  
 SELECT  DISTINCT c.LocalityAutoId, c.LocalityCode 'Locality Code', c.LocalityName 'Locality Name', ISNULL(ISNULL(Address1,c.Address2),c.Address3) Address  
     FROM tblLocalities  c  
     ORDER BY c.LocalityAutoId desc  
 END  
  
 ELSE IF @operation = 'Search'  
 BEGIN  
 SELECT A.LocalityAutoId, A.LocalityCode, A.LocalityName, ISNULL(ISNULL(Address1,a.Address2),a.Address3) Address  
     FROM tblLocalities A  
   WHERE  
   (  
   CAST(A.LocalityCode AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR  
   CAST(A.LocalityName AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR  
   CAST(A.Address1 AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR  
   CAST(A.Address2 AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR  
   CAST(A.Address3 AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR  
   CAST(A.District AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR  
   CAST(A.City AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'      
   )  
   ORDER BY A.LocalityAutoId desc  
 END  
    
 ELSE IF @operation='GetLocalities'  
 BEGIN  
 SELECT DISTINCT TOP 5 c.LocalityAutoId Id, c.LocalityCode 'Code', c.LocalityName 'Text'  
     FROM tblLocalities  c  
      WHERE   
     (c.LocalityCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR  
     c.LocalityName LIKE '%'+ISNULL(@SearchText,'')+ '%' )  
     ORDER BY c.LocalityAutoId desc  
 END  
  ELSE IF @operation = 'Delete'  
 BEGIN  
  DELETE  
  FROM   dbo.tblLocalities  
  WHERE  LocalityAutoId = @LocalityAutoId  
  IF EXISTS(SELECT 1 FROM tblLocalityImage  ci WHERE ci.LocalityAutoId=@LocalityAutoId)  
  BEGIN  
   DELETE FROM tblLocalityImage WHERE LocalityAutoId=@LocalityAutoId  
  END  
  SELECT @LocalityAutoId AS LocalityAutoId,'Successfully Deleted' AS RESULT  
 END  
  
 ELSE IF @operation='GetNewLocalitiesForAutoRef'  
 BEGIN  
 SELECT  DISTINCT TOP 5 c.LocalityAutoId Id, c.LocalityCode 'Code', c.LocalityName 'Text'  
     FROM tblLocalities  c INNER JOIN tblLocalityResident  cw ON c.LocalityAutoId = cw.LocalityAutoId  
     Left JOIN tblAutoRefTestResident artw ON cw.ResidentAutoId=artw.ResidentAutoId  
     WHERE   
     (c.LocalityCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR  
     c.LocalityName LIKE '%'+ISNULL(@SearchText,'')+ '%' )  
     ORDER BY c.LocalityName  
 END  
  
 ELSE IF @operation='GetEditLocalitiesForAutoRef'  
 BEGIN  
 SELECT DISTINCT TOP 5 c.LocalityAutoId Id, c.LocalityCode 'Code', c.LocalityName 'Text'  
      FROM tblLocalities  c INNER JOIN tblLocalityResident  cw ON c.LocalityAutoId = cw.LocalityAutoId  
     Left JOIN tblAutoRefTestResident artw ON cw.ResidentAutoId=artw.ResidentAutoId  
     WHERE   
     (c.LocalityCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR  
     c.LocalityName LIKE '%'+ISNULL(@SearchText,'')+ '%' )  
     ORDER BY c.LocalityName  
 END  
  
 ELSE IF @operation='GetNewLocalitiesForOptometristResident'  
 BEGIN  
 SELECT DISTINCT  TOP 5  c.LocalityAutoId Id, c.LocalityCode 'Code', c.LocalityName 'Text'  
     FROM tblLocalities c INNER JOIN tblLocalityResident cw ON c.LocalityAutoId = cw.LocalityAutoId  
     JOIN tblAutoRefTestResident artw ON cw.ResidentAutoId=artw.ResidentAutoId  
     WHERE  
     (c.LocalityCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR  
     c.LocalityName LIKE '%'+ISNULL(@SearchText,'')+ '%' )  
     ORDER BY c.LocalityName  
 END  
  
 ELSE IF @operation='GetEditLocalitiesForOptometristResident'  
 BEGIN  
 SELECT  DISTINCT TOP 5 c.LocalityAutoId Id, c.LocalityCode 'Code', c.LocalityName 'Text'  
     FROM tblLocalities c INNER JOIN tblLocalityResident cw ON c.LocalityAutoId = cw.LocalityAutoId  
     INNER JOIN tblAutoRefTestResident artw ON cw.ResidentAutoId=artw.ResidentAutoId  
     INNER JOIN tblOptometristResident ow ON artw.AutoRefResidentId=ow.AutoRefResidentId  
     WHERE  
     (c.LocalityCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR  
     c.LocalityName LIKE '%'+ISNULL(@SearchText,'')+ '%' )  
     ORDER BY c.LocalityName  
 END  
  
  
 ELSE IF @operation='GetNewLocalitiesForGlassDispense'      
 BEGIN      
 SELECT  DISTINCT TOP 5 c.LocalityAutoId Id, c.LocalityCode 'Code', c.LocalityName 'Text'      
     FROM tblLocalities c INNER JOIN tblLocalityResident cw ON c.LocalityAutoId = cw.LocalityAutoId      
  INNER JOIN tblOptometristResident ow ON cw.ResidentAutoId = ow.ResidentAutoId  
     --Left JOIN tblGlassDispenseResident  artw ON cw.ResidentAutoId=artw.ResidentAutoId      
  
     WHERE       
     (c.LocalityCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR      
     c.LocalityName LIKE '%'+ISNULL(@SearchText,'')+ '%' )      
     ORDER BY c.LocalityName      
 END      
      
 ELSE IF @operation='GetEditLocalitiesForGlassDispense'      
 BEGIN      
 SELECT DISTINCT TOP 5  c.LocalityAutoId Id, c.LocalityCode 'Code', c.LocalityName 'Text'      
     FROM tblLocalities c INNER JOIN tblLocalityResident cw ON c.LocalityAutoId = cw.LocalityAutoId      
     INNER JOIN tblGlassDispenseResident artw ON cw.ResidentAutoId=artw.ResidentAutoId      
     WHERE       
     (c.LocalityCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR      
     c.LocalityName LIKE '%'+ISNULL(@SearchText,'')+ '%' )      
     ORDER BY c.LocalityName      
 END      
    
 
GO


 

CREATE OR ALTER PROCEDURE [dbo].[sp_SetupLocalityImage](
	@LocalityImageAutoId INT=NULL,
	@LocalityAutoId	INT = NULL,  
	@LocalityPic		VARCHAR(MAX) = NULL,
	@FileType		NVARCHAR(20) = NULL,
	@CaptureDate DATE=NULL,
	@FileSize		INT = NULL,
	@CaptureRemarks  VARCHAR(500)=NULL,
	@UserId nvarchar(500) = NULL,  
	@EntDate DATETIME = NULL,  
	@EntOperation nvarchar(200) = NULL,  
	@EntryTerminal nvarchar(400) = NULL,  
	@EntryTerminalIP  nvarchar(400)  = NULL,
	@UserEmpName NVARCHAR(100)=NULL,
	@operation VARCHAR(50)= NULL
)
AS
IF @operation = 'Save'
BEGIN
    	INSERT INTO tblLocalityImage (LocalityAutoId, LocalityPic, FileType, FileSize, CaptureDate, CaptureRemarks, UserId, EntDate, EntOperation, EntTerminal, EntTerminalIP)
		SELECT   @LocalityAutoId, @LocalityPic, SUBSTRING(@FileType,1,10), @FileSize,@CaptureDate,@CaptureRemarks, @UserId, @EntDate, 'INSERT', @EntryTerminal, @EntryTerminalIP  
		SET @LocalityImageAutoId=SCOPE_IDENTITY()
		SELECT  @LocalityImageAutoId LocalityImageAutoId ,'Successfully Saved' AS Result
END

ELSE IF @operation='Update'
BEGIN

		UPDATE tblLocalityImage SET LocalityPic = @LocalityPic, FileType = @FileType, FileSize = @FileSize, 
		UserId = @UserId, EntDate = @EntDate, EntOperation = 'UPDATE', EntTerminal = @EntryTerminal, EntTerminalIP = @EntryTerminalIP    
		WHERE LocalityAutoId = @LocalityAutoId
		SELECT @LocalityAutoId AS LocalityAutoId,'Successfully Updated' AS RESULT
END


ELSE IF @operation='Delete'
BEGIN

		DELETE FROM tblLocalityImage WHERE LocalityImageAutoId=@LocalityImageAutoId
		SELECT @LocalityImageAutoId AS LocalityImageAutoId,'Successfully Deleted' AS RESULT
END
GO


 

CREATE OR ALTER PROCEDURE [dbo].[Sp_LocalityResident](  
@ResidentAutoId int=NULL,  
@LocalityAutoId int=NULL,  
@LocalityCode VARCHAR(20)=NULL,  
@ResidentCode nvarchar(15)=NULL,  
@ResidentName nvarchar(500)=NULL,  
@RelationType NVARCHAR(100)=NULL,  
@RelationName nvarchar(500)=NULL,  
@Age int=NULL,  
@GenderAutoId int=NULL,  
@DecreasedVision bit=NULL,  
@Distance BIT= NULL,  
@Near BIT= NULL,  
@WearGlasses bit=NULL,  
@CNIC VARCHAR(30) =NULL,  
@UserId nvarchar(250)=NULL,  
@EnrollmentDate DATETIME =NULL,  
@EntryTerminal VARCHAR(200)=NULL,  
@EntryTerminalIP VARCHAR(200)=NULL,  
@HasOccularHistory bit=NULL,  
@OccularHistoryRemarks nvarchar(500)=NULL,  
@HasMedicalHistory bit=NULL,  
@MedicalHistoryRemarks nvarchar(500)=NULL,  
@HasChiefComplain bit=NULL,  
@ChiefComplainRemarks nvarchar(500)=NULL,  
@ResidentTestDate datetime=NULL,  
@ResidentRegNo varchar(20)=NULL,  
@MobileNo VARCHAR(15)=NULL,  
@SectionAutoId int=NULL,  
@ApplicationID nvarchar(20)=NULL,  
@FormId nvarchar(250)=NULL,  
@UserEmpId int=NULL,  
@UserEmpName nvarchar(250)=NULL,  
@UserEmpCode nvarchar(10)=NULL,  
@Religion bit=NULL,  
@operation VARCHAR(50)= NULL,  
@SearchText VARCHAR(MAX)=NULL  
)  
AS  
DECLARE @ResidentNewCode VARCHAR(10)=NULL  
IF @operation='Save'  
BEGIN  
 IF NOT EXISTS(SELECT 1 FROM tblLocalityResident c WITH (NOLOCK) WHERE c.ResidentName=@ResidentName AND c.CNIC=@CNIC )  
 BEGIN  
 BEGIN TRAN  
 BEGIN TRy  
 SELECT @LocalityAutoId=c.LocalityAutoId FROM tblLocalities  c WHERE c.LocalityCode=@LocalityCode  
 EXEC Sp_GetCode @CodeType = 'LocalityResident',@CodeLength = 4    ,@PreFix = '05'    ,@LocalityCode = @LocalityCode     
     ,@LocalityId = @LocalityAutoId   ,@operation = 'GetLocalityCode',@Code = @ResidentNewCode OUTPUT  
  
   INSERT INTO tblLocalityResident (LocalityAutoId, ResidentCode, ResidentName, RelationType, RelationName,  
      Age, GenderAutoId, CNIC, UserId, EntDate, EntOperation, EntTerminal, EntTerminalIP,   
      WearGlasses, Distance, Near, DecreasedVision, HasOccularHistory, OccularHistoryRemarks,  
      HasMedicalHistory, MedicalHistoryRemarks, HasChiefComplain, ChiefComplainRemarks,  
      ResidentTestDate, ResidentRegNo, SectionAutoId, ApplicationID, FormId, UserEmpId,  
      UserEmpName, UserEmpCode, Religion, MobileNo, LocalityCode)     
  
   SELECT @LocalityAutoId,@ResidentNewCode, @ResidentName, @RelationType,@RelationName,   
                    @Age, @GenderAutoId,@CNIC,@UserId,@EnrollmentDate,'INSERT', @EntryTerminal, @EntryTerminalIP,  
     @WearGlasses,@Distance,@Near,@DecreasedVision,@HasOccularHistory,@OccularHistoryRemarks  
     ,@HasMedicalHistory, @MedicalHistoryRemarks, @HasChiefComplain, @ChiefComplainRemarks,   
     @ResidentTestDate,@ResidentRegNo,@SectionAutoId,@ApplicationID,@FormId,@UserEmpId,  
     @UserEmpName,@UserEmpCode,@Religion,@MobileNo,@LocalityCode  
  
     SET @ResidentAutoId = SCOPE_IDENTITY()  
  
   EXEC Sp_GetCode @CodeType = 'LocalityResident',@CodeLength = 4    ,@PreFix = '05'    ,@LocalityCode = @LocalityCode,  
       @LocalityId = @LocalityAutoId   ,@operation = 'UpdateLocalityCode'  
   SELECT @ResidentAutoId AS ResidentAutoId,'Successfully Saved: <br> Resident Code: '+CAST(@ResidentNewCode AS VARCHAR) AS RESULT  
   Commit  
END TRY  
BEGIN CATCH  
  SELECT @ResidentAutoId AS ResidentAutoId,'Error'+ERROR_MESSAGE() AS RESULT  
ROLLBACK TRAN  
END Catch  
  END  
  ELSE  
  SELECT @ResidentAutoId AS ResidentAutoId,'Worker with same detail Already Exists.' AS RESULT  
END  
ELSE IF @operation = 'Update'  
BEGIN  
BEGIN TRAN  
BEGIN TRy  
  
 IF(@LocalityCode != (SELECT top 1 cw.LocalityCode FROM tblLocalityResident cw WITH (NOLOCK) WHERE cw.ResidentAutoId=@ResidentAutoId))  
 BEGIN  
  SELECT @LocalityAutoId=c.LocalityAutoId FROM tblLocalityResident c WHERE c.LocalityCode=@LocalityCode   
  EXEC Sp_GetCode @CodeType = 'LocalityResident',@CodeLength = 4    ,@PreFix = '05'    ,  
  @LocalityCode = @LocalityCode,@LocalityId= @LocalityAutoId,@operation = 'GetLocalityCode',@Code = @ResidentNewCode OUTPUT  
  
  EXEC Sp_GetCode @CodeType = 'LocalityResident',@CodeLength = 4    ,@PreFix = '05'    ,@LocalityCode = @LocalityCode,@LocalityId = @LocalityAutoId  
  ,@operation = 'UpdateLocalityCode'  
  
  SET @ResidentCode=@ResidentNewCode  
 END  
 ELSE  
 BEGIN  
  SELECT @ResidentCode=cw.ResidentCode FROM tblLocalityResident cw WITH (NOLOCK) WHERE cw.ResidentAutoId=@ResidentAutoId  
 END  
  UPDATE tblLocalityResident   
  SET LocalityAutoId = @LocalityAutoId  
     ,ResidentName = @ResidentName   
     ,ResidentCode=@ResidentCode  
     ,RelationType=@RelationType  
     ,RelationName = @RelationName   
     ,Age = @Age   
     ,GenderAutoId = @GenderAutoId  
     ,CNIC = @CNIC  
     ,DecreasedVision = @DecreasedVision   
     ,Near=@Near  
     ,Distance=@Distance  
     ,EntOperation = 'Update'  
     ,WearGlasses = @WearGlasses   
     ,HasOccularHistory = @HasOccularHistory   
     ,OccularHistoryRemarks = @OccularHistoryRemarks   
     ,HasMedicalHistory = @HasMedicalHistory   
     ,MedicalHistoryRemarks = @MedicalHistoryRemarks   
     ,HasChiefComplain = @HasChiefComplain   
     ,ChiefComplainRemarks = @ChiefComplainRemarks   
     ,ResidentTestDate = @ResidentTestDate  
     ,MobileNo = @MobileNo  
     ,Religion = @Religion   
   WHERE   
   ResidentAutoId = @ResidentAutoId  
  
    SELECT @ResidentAutoId AS ResidentAutoId,'Successfully Updated: <br> '+@ResidentCode AS RESULT  
   Commit  
END TRY  
BEGIN CATCH  
  SELECT @ResidentAutoId AS ResidentAutoId,'Error Updated: '+ERROR_MESSAGE() AS RESULT  
ROLLBACK TRAN  
END Catch  
END  
  
ELSE IF @operation = 'Delete'  
 BEGIN  
  DELETE  
  FROM   dbo.tblLocalityResident  
  WHERE  ResidentAutoId = @ResidentAutoId
  IF EXISTS(SELECT 1 FROM tblLocalityResidentImage ci WHERE ci.ResidentAutoId=@ResidentAutoId)  
  BEGIN  
   DELETE FROM tblLocalityResidentImage WHERE ResidentAutoId=@ResidentAutoId
  END  
  SELECT @ResidentAutoId AS ResidentAutoId,'Successfully Deleted' AS RESULT  
 END  
  
ELSE IF @operation='GetResidentByiD'  
 BEGIN  
 SELECT c.ResidentAutoId,c.LocalityAutoId,c.LocalityCode, c.ResidentCode, c.ResidentName,c.RelationType,  
   c.RelationName, c.Age, ISNULL(c.GenderAutoId,0)GenderAutoId , c.CNIC, c.DecreasedVision,c.UserId,  
   c.EntDate, c.Near,c.Distance, c.EntOperation, c.EntTerminal, c.EntTerminalIP, c.WearGlasses,  
   c.HasOccularHistory, c.OccularHistoryRemarks, c.HasMedicalHistory, c.MedicalHistoryRemarks,c.EntDate EnrollmentDate,c.MobileNo,  
   c.HasChiefComplain, c.ChiefComplainRemarks, c.ResidentTestDate, c.ResidentRegNo, c.SectionAutoId, c.ApplicationID, c.FormId, c.UserEmpId,c1.LocalityName,  
   c.UserEmpName, c.UserEmpCode, c.Religion, c.EntDate,ci.ResidentImageAutoId,ci.ResidentAutoId,  
   ISNULL(ci.CaptureRemarks,'')CaptureRemarks ,ci.LocalityAutoId DetailLocalityAutoId,ci.FileType,ci.FileSize,ci.ResidentPic  
     FROM tblLocalityResident c  INNER JOIN tblLocalities  c1 ON c1.LocalityAutoId =c.LocalityAutoId  
     LEFT JOIN tblLocalityResidentImage ci ON c.ResidentAutoId = ci.ResidentAutoId  
  WHERE   
  c.ResidentAutoId=@ResidentAutoId  
 END  
  
-- ELSE IF @operation='GetAllWorker'  
-- BEGIN  
-- SELECT TOP 10 c.WorkerAutoId,c.WorkerCode,c.WorkerName,c1.CompanyName,c.CNIC,c.MobileNo  
--     FROM tblLocalityResident c INNER JOIN tblCompany c1 ON c.CompanyAutoId = c1.CompanyAutoId  
--     ORDER BY c.WorkerAutoId desc  
-- END  
  
   
 ELSE IF @operation = 'Search'  
 BEGIN  
 SELECT TOP 10 c.ResidentAutoId,c.ResidentCode,c.ResidentName,A.LocalityName,c.CNIC,c.MobileNo  
     FROM tblLocalities  A INNER JOIN tblLocalityResident c ON A.LocalityAutoId = c.LocalityAutoId  
   WHERE  
   (  
    CAST(c.ResidentCode AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR  
    CAST(A.LocalityName AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR  
    CAST(c.ResidentName AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR  
    CAST(c.CNIC AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR  
    CAST(c.MobileNo AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'      
   )  
   ORDER BY c.ResidentAutoId desc  
 END  
  
 ELSE IF @operation= 'GetResidents'  
 BEGIN  
 SELECT DISTINCT  TOP 5  cw.ResidentAutoId 'Id', cw.ResidentCode 'Code', cw.ResidentName +' | '+cw.ResidentCode 'Text'  
     FROM tblLocalityResident cw WITH(NOLOCK)   
     WHERE  
     cw.LocalityAutoId=@LocalityAutoId AND   
     (cw.ResidentCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR  
     cw.ResidentName LIKE '%'+ISNULL(@SearchText,'')+ '%' )  
     ORDER BY  cw.ResidentName+' | '+cw.ResidentCode   
 END  
 ELSE IF @operation= 'DeleteWorkerById'  
 BEGIN  
  DELETE FROM tblLocalityResident WHERE ResidentAutoId=@ResidentAutoId  
  DELETE FROM tblLocalityResidentImage WHERE ResidentAutoId=@ResidentAutoId  
  
  SELECT @ResidentAutoId AS ResidentAutoId,'Successfully Deleted' AS RESULT  
  
    
 END
GO


 

CREATE OR ALTER PROCEDURE [dbo].[sp_LocalityResidentImage](
	@ResidentAutoId INT =NULL,
	@ResidentImageAutoId INT=NULL,
	@LocalityAutoId	INT = NULL,  
	@ResidentPic		VARCHAR(MAX) = NULL,
	@FileType		NVARCHAR(20) = NULL,
	@FileSize		INT = NULL,
	@CaptureRemarks  VARCHAR(500)=NULL,
	@UserId nvarchar(500) = NULL,  
	@EntDate DATETIME = NULL,  
	@EntOperation nvarchar(200) = NULL,  
	@EntryTerminal nvarchar(400) = NULL,  
	@EntryTerminalIP  nvarchar(400)  = NULL,
	@UserEmpName NVARCHAR(100)=NULL,
	@operation VARCHAR(50)= NULL
)
AS
IF @operation = 'Save'
BEGIN
    	INSERT INTO tblLocalityResidentImage (ResidentAutoId, LocalityAutoId, ResidentPic, FileType, FileSize, CaptureDate, CaptureRemarks, UserId, EntDate, EntOperation, EntTerminal, EntTerminalIP)
		SELECT  @ResidentAutoId, @LocalityAutoId, @ResidentPic, SUBSTRING(@FileType,1,10), @FileSize,GETDATE(),@CaptureRemarks, @UserId, @EntDate, 'INSERT', @EntryTerminal, @EntryTerminalIP  
		SET @ResidentImageAutoId=SCOPE_IDENTITY()
		SELECT  @ResidentImageAutoId LocalityImageAutoId ,'Successfully Saved' AS Result
END

ELSE IF @operation='Update'
BEGIN

		UPDATE tblLocalityResidentImage SET ResidentPic = @ResidentPic, FileType = @FileType, FileSize = @FileSize, 
		UserId = @UserId, EntDate = @EntDate, EntOperation = 'UPDATE', EntTerminal = @EntryTerminal, EntTerminalIP = @EntryTerminalIP    
		WHERE @ResidentAutoId = @ResidentAutoId
		SELECT @ResidentAutoId AS ResidentAutoId,'Successfully Updated' AS RESULT
END


ELSE IF @operation='Delete'
BEGIN

		DELETE FROM tblLocalityResidentImage WHERE ResidentImageAutoId=@ResidentImageAutoId
		SELECT @ResidentImageAutoId AS ResidentImageAutoId,'Successfully Deleted' AS RESULT
END
GO


 
   
CREATE OR ALTER PROCEDURE [dbo].[Sp_AutoRefTestResident](  
 @LocalityAutoId INT =NULL,  
 @AutoRefResidentId INT = NULL,  
    @AutoRefResidentTransId varchar(15) = NULL,  
    @AutoRefResidentTransDate datetime = NULL,  
    @ResidentAutoId int= NULL,  
    @Right_Spherical_Status char(1)= NULL,  
    @Right_Spherical_Points decimal(9, 2)= NULL,  
    @Right_Cyclinderical_Status char(1)= NULL,  
    @Right_Cyclinderical_Points decimal(9, 2)= NULL,  
    @Right_Axix_From int= NULL,  
    @Right_Axix_To int= NULL,  
    @Left_Spherical_Status char(1)= NULL,  
    @Left_Spherical_Points decimal(9, 2)= NULL,  
    @Left_Cyclinderical_Status char(1)= NULL,  
    @Left_Cyclinderical_Points decimal(9, 2)= NULL,  
    @Left_Axix_From int= NULL,  
    @Left_Axix_To int= NULL,  
 @IPD INT = NULL,   
    @UserId nvarchar(250)= NULL,  
    @EntDate datetime= NULL,  
    @EntOperation nvarchar(100)= NULL,  
    @EntryTerminal nvarchar(200)= NULL,  
    @EntryTerminalIP nvarchar(200)= NULL,  
    @ApplicationID nvarchar(20)= NULL,  
    @FormId nvarchar(250)= NULL,  
    @UserEmpId int= NULL,  
    @UserEmpName nvarchar(250)= NULL,  
    @UserEmpCode nvarchar(10)= NULL,  
 @operation VARCHAR(50)= NULL,  
 @SearchText VARCHAR(MAX)=NULL  
 )  
 AS  
IF @operation='Save'  
 BEGIN  
 IF NOT EXISTS(SELECT 1 FROM tblAutoRefTestResident artw WHERE artw.ResidentAutoId=@ResidentAutoId AND CAST(artw.AutoRefResidentTransDate AS DATE)=CAST(@AutoRefResidentTransDate AS date))  
 BEGIN  
  INSERT INTO dbo.tblAutoRefTestResident (AutoRefResidentTransId, AutoRefResidentTransDate,   
                                          ResidentAutoId, Right_Spherical_Status, Right_Spherical_Points,   
                                          Right_Cyclinderical_Status, Right_Cyclinderical_Points, Right_Axix_From,   
                                          Right_Axix_To, Left_Spherical_Status, Left_Spherical_Points,   
                                          Left_Cyclinderical_Status, Left_Cyclinderical_Points, Left_Axix_From,   
                                          Left_Axix_To,IPD, UserId, EntDate, EntOperation, EntTerminal,   
                                          EntTerminalIP, ApplicationID, FormId, UserEmpId, UserEmpName,   
                                          UserEmpCode)  
    SELECT @AutoRefResidentTransId, @AutoRefResidentTransDate, @ResidentAutoId, @Right_Spherical_Status,   
           @Right_Spherical_Points, @Right_Cyclinderical_Status, @Right_Cyclinderical_Points, @Right_Axix_From,   
           @Right_Axix_To, @Left_Spherical_Status, @Left_Spherical_Points, @Left_Cyclinderical_Status,   
           @Left_Cyclinderical_Points, @Left_Axix_From, @Left_Axix_To,@IPD, @UserId, GETDATE(), 'INSERT',   
           @EntryTerminal, @EntryTerminalIP, @ApplicationID, @FormId, @UserEmpId, @UserEmpName, @UserEmpCode  
     SET @AutoRefResidentId = SCOPE_IDENTITY()  
     SELECT @AutoRefResidentId  AS AutoRefResidentId,'Successfully Saved' AS RESULT  
 END  
 ELSE  
    SELECT @AutoRefResidentId  AS AutoRefResidentId,'Resident Auto Refraction Already Exists in selected Date' AS RESULT  
 END  
ELSE IF @operation='Update'  
 BEGIN  
  UPDATE dbo.tblAutoRefTestResident  
  SET    AutoRefResidentTransId = @AutoRefResidentTransId, ResidentAutoId = @ResidentAutoId, Right_Spherical_Status = @Right_Spherical_Status,   
    Right_Spherical_Points = @Right_Spherical_Points, Right_Cyclinderical_Status = @Right_Cyclinderical_Status,   
    Right_Cyclinderical_Points = @Right_Cyclinderical_Points, Right_Axix_From = @Right_Axix_From,   
    Right_Axix_To = @Right_Axix_To, Left_Spherical_Status = @Left_Spherical_Status, Left_Spherical_Points = @Left_Spherical_Points,   
    Left_Cyclinderical_Status = @Left_Cyclinderical_Status, Left_Cyclinderical_Points = @Left_Cyclinderical_Points,   
    Left_Axix_From = @Left_Axix_From, Left_Axix_To = @Left_Axix_To,IPD=@IPD, UserId = @UserId,  
    EntOperation = 'Update', EntTerminal = @EntryTerminal, EntTerminalIP = @EntryTerminalIP,   
    UserEmpId = @UserEmpId, UserEmpName = @UserEmpName, UserEmpCode = @UserEmpCode  
  WHERE  AutoRefResidentId = @AutoRefResidentId   
  
  SELECT @AutoRefResidentId  AS AutoRefResidentId,'Successfully Updated' AS RESULT  
 END  
  
ELSE IF @operation='GetByAutoRefResidentId '  
 BEGIN  
  SELECT AutoRefResidentId, AutoRefResidentTransId, AutoRefResidentTransDate, ResidentAutoId, Right_Spherical_Status, Right_Spherical_Points,   
  Right_Cyclinderical_Status, Right_Cyclinderical_Points, Right_Axix_From, Right_Axix_To, Left_Spherical_Status, Left_Spherical_Points,   
  Left_Cyclinderical_Status, Left_Cyclinderical_Points, Left_Axix_From, Left_Axix_To, ISNULL(IPD,0)IPD,UserId, EntDate, EntOperation, EntTerminal,  
  EntTerminalIP, ApplicationID, FormId, UserEmpId, UserEmpName, UserEmpCode ,0 WearGlasses  
  FROM   dbo.tblAutoRefTestResident  
  WHERE  AutoRefResidentId = @AutoRefResidentId    
END  
ELSE IF @operation='GetAutoRefByResidentId '  
 BEGIN  
  SELECT   
      a.AutoRefResidentId, a.AutoRefResidentTransId, CAST(a.AutoRefResidentTransDate AS varchar) 'Test Date', a.ResidentAutoId AutoRefResidentId,   
      a.Right_Spherical_Status, CASE WHEN  ISNULL(a.Right_Spherical_Points,0) > 0 THEN '+'+CAST(a.Right_Spherical_Points AS VARCHAR)  
      ELSE CAST(a.Right_Spherical_Points AS VARCHAR) END 'Right Spherical', a.Right_Cyclinderical_Status,      
      CASE WHEN ISNULL(a.Left_Spherical_Points,0) > 0 THEN '+'+ CAST(a.Left_Spherical_Points AS VARCHAR)  
      ELSE CAST(a.Left_Spherical_Points AS VARCHAR) END 'Left Spherical',   
      '', a.Right_Axix_To, a.Left_Spherical_Status, a.Right_Cyclinderical_Points 'Right Cyclinderical',   
      a.Left_Cyclinderical_Status,a.Left_Cyclinderical_Points 'Left Cyclinderical',a.Right_Axix_From AS 'Right Axis' , a.Left_Axix_From AS 'Left Axis',  
      a.Left_Axix_To,ISNULL(IPD,0)IPD, a.UserId, a.EntDate, a.EntOperation, a.EntTerminal, a.EntTerminalIP, a.ApplicationID, a.FormId, a.UserEmpId, a.UserEmpName, a.UserEmpCode   
      ,b.WearGlasses  
      FROM  tblLocalityResident b INNER  JOIN dbo.tblAutoRefTestResident a ON b.ResidentAutoId=a.ResidentAutoId  
  WHERE  b.ResidentAutoId=@ResidentAutoId  
 END  
ELSE IF @operation='NewResidentForRef'  
BEGIN  
 SELECT DISTINCT top 5 cw.ResidentAutoId Id, cw.ResidentName +' | '+cw.ResidentCode  text,cw.ResidentCode Code   
 FROM tblLocalityResident cw  
 WHERE   
 cw.LocalityAutoId=@LocalityAutoId AND   
 cw.ResidentAutoId NOT IN (SELECT artw.ResidentAutoId FROM tblAutoRefTestResident artw WITH (NOLOCK) WHERE CAST(artw.AutoRefResidentTransDate AS DATE) = CAST(@AutoRefResidentTransDate AS DATE))  
  AND   
 (cw.ResidentCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR  
  cw.ResidentName LIKE '%'+ISNULL(@SearchText,'')+ '%' )  
 ORDER BY  cw.ResidentName +' | '+cw.ResidentCode   
END  
  
ELSE IF @operation='EditResidentForRef'  
BEGIN  
 SELECT DISTINCT TOP 5 cw.ResidentAutoId Id, cw.ResidentName +' | '+cw.ResidentCode  text,cw.ResidentCode Code   
 FROM tblLocalityResident cw INNER JOIN tblAutoRefTestResident artw ON cw.ResidentAutoId=artw.ResidentAutoId  
 WHERE   
 cw.LocalityAutoId=@LocalityAutoId  
 AND (cw.ResidentCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR  
  cw.ResidentName LIKE '%'+ISNULL(@SearchText,'')+ '%' )  
 ORDER BY  cw.ResidentName +' | '+cw.ResidentCode   
END  
ELSE IF @operation='GetAutoRefHistoryByResidentId '  
 BEGIN  
    
   SELECT TOP 1 CAST(FORMAT(artw.AutoRefResidentTransDate,'dd | MMM | yyyy') AS VARCHAR) AS Last_Visit_Date,  
   CASE WHEN artw.Right_Spherical_Status ='P' AND artw.Right_Spherical_Points>0 THEN '+' +CAST(Right_Spherical_Points AS VARCHAR)  
   WHEN artw.Right_Spherical_Status='N'AND artw.Right_Spherical_Points>0 THEN '-' +CAST(Right_Spherical_Points AS VARCHAR)  
   WHEN artw.Right_Spherical_Status='E' THEN 'Error '+CAST(ISNULL(Right_Spherical_Points,'') AS VARCHAR)  
   WHEN artw.Right_Spherical_Status='l' THEN 'Plano '+CAST(ISNULL(Right_Spherical_Points,'') AS VARCHAR)  
   ELSE CAST(Right_Spherical_Points AS VARCHAR) END 'Right Spherical',  
  
   CASE WHEN artw.Left_Spherical_Status ='P' AND artw.Left_Spherical_Points>0 THEN '+' +CAST(Left_Spherical_Points AS VARCHAR)  
   WHEN artw.Left_Spherical_Status='N'AND artw.Left_Spherical_Points>0 THEN '-' +CAST(Left_Spherical_Points AS VARCHAR)  
    WHEN artw.Left_Spherical_Status='E' THEN 'Error '+CAST(ISNULL(Left_Spherical_Points,'') AS VARCHAR)  
   WHEN artw.Left_Spherical_Status='l' THEN 'Plano '+CAST(ISNULL(Left_Spherical_Points,'') AS VARCHAR)  
   ELSE CAST(Left_Spherical_Points AS VARCHAR) END 'Left Spherical',  
  
   CASE WHEN artw.Right_Spherical_Status ='P' AND artw.Left_Spherical_Points>0 THEN '+' +CAST(Left_Spherical_Points AS VARCHAR)  
   WHEN artw.Right_Spherical_Status='N'AND artw.Right_Spherical_Points>0 THEN '-' +CAST(Right_Spherical_Points AS VARCHAR)  
   ELSE CAST(Right_Spherical_Points AS VARCHAR) END 'Right Spherical',  
  
   CASE WHEN artw.Right_Cyclinderical_Status ='P' AND artw.Right_Cyclinderical_Points>0 THEN '+' +CAST(Right_Cyclinderical_Points AS VARCHAR)  
   WHEN artw.Right_Cyclinderical_Status='N'AND artw.Right_Cyclinderical_Points>0 THEN '-' +CAST(Right_Cyclinderical_Points AS VARCHAR)  
   ELSE CAST(Right_Cyclinderical_Points AS VARCHAR) END 'Right Cyclinderical',   
  
     
   CASE WHEN artw.Left_Cyclinderical_Status ='P' AND artw.Left_Cyclinderical_Points>0 THEN '+' +CAST(Left_Cyclinderical_Points AS VARCHAR)  
   WHEN artw.Left_Cyclinderical_Status='N'AND artw.Left_Cyclinderical_Points>0 THEN '-' +CAST(Left_Cyclinderical_Points AS VARCHAR)  
   ELSE CAST(artw.Left_Cyclinderical_Points AS VARCHAR) END 'Left Cyclinderical',   
   artw.Right_Axix_From 'Right Axis' ,  
    artw.Left_Axix_From 'Left Axis' ,0 WearGlasses,  
    ISNULL(artw.IPD,0)IPD  
  
  FROM tblAutoRefTestResident artw  
  WHERE  artw.ResidentAutoId=@ResidentAutoId  
  ORDER BY artw.AutoRefResidentTransDate desc  
 END  
  
 ELSE IF @operation= 'GetDatesofResident'  
 BEGIN  
  SELECT CAST(FORMAT(artw.AutoRefResidentTransDate,'dd | MMM | yyyy') AS VARCHAR) Text,  
  artw.AutoRefResidentId AS Id  
  FROM tblAutoRefTestResident artw  
  WHERE artw.ResidentAutoId=@ResidentAutoId  
  ORDER BY artw.AutoRefResidentId desc  
 END  
  
   
ELSE IF @operation='GetByAutoRefResidentIdForOpt '  
 BEGIN  
  SELECT TOP 1 a.AutoRefResidentId, a.AutoRefResidentTransId, a.AutoRefResidentTransDate,a. ResidentAutoId, a.Right_Spherical_Status,   
  a.Right_Spherical_Points, a.Right_Cyclinderical_Status, a.Right_Cyclinderical_Points, a.Right_Axix_From, a.Right_Axix_To,  
  a.Left_Spherical_Status, a.Left_Spherical_Points,  a.Left_Cyclinderical_Status, a.Left_Cyclinderical_Points,  
  a.Left_Axix_From, Left_Axix_To, ISNULL(IPD,0)IPD, a.UserId, a.EntDate, a.EntOperation, a.EntTerminal,  
  a.EntTerminalIP, a.ApplicationID, a.FormId, a.UserEmpId, a.UserEmpName, a.UserEmpCode ,ISNULL(cw.WearGlasses,0)WearGlasses  
  FROM   dbo.tblAutoRefTestResident  a INNER JOIN  tblLocalityResident cw ON cw.ResidentAutoId=a.ResidentAutoId  
  WHERE  a.ResidentAutoId = @ResidentAutoId  
  ORDER BY AutoRefResidentId desc  
END  
ELSE IF @operation= 'DeleteAutoRefById'  
 BEGIN  
  DELETE FROM tblAutoRefTestResident WHERE AutoRefResidentId=@AutoRefResidentId  
  SELECT @AutoRefResidentId AS AutoRefResidentId,'Successfully Deleted' AS RESULT  
 END
GO


 

 CREATE OR ALTER PROC [dbo].[Sp_OptometristResident](  
    @OptometristResidentId INT = NULL,  
    @OptometristResidentTransDate datetime = NULL,  
    @ResidentAutoId int = NULL,  
 @AutoRefResidentId INT=NULL,  
    @HasChiefComplain int = NULL,  
    @ChiefComplainRemarks nvarchar(200)= NULL,  
    @HasOccularHistory int= NULL,  
    @OccularHistoryRemarks nvarchar(200)= NULL,  
    @HasMedicalHistory int= NULL,  
    @MedicalHistoryRemarks nvarchar(200)= NULL,  
    @DistanceVision_RightEye_Unaided int= NULL,  
    @DistanceVision_RightEye_WithGlasses int= NULL,  
    @DistanceVision_RightEye_PinHole int= NULL,  
    @NearVision_RightEye int= NULL,  
    @NeedCycloRefraction_RightEye int= NULL,  
    @NeedCycloRefractionRemarks_RightEye nvarchar(200)= NULL,  
    @DistanceVision_LeftEye_Unaided int= NULL,  
    @DistanceVision_LeftEye_WithGlasses int= NULL,  
    @DistanceVision_LeftEye_PinHole int= NULL,  
    @NearVision_LeftEye int= NULL,  
    @NeedCycloRefraction_LeftEye int= NULL,  
    @NeedCycloRefractionRemarks_LeftEye nvarchar(200)= NULL,  
    @Right_Spherical_Status char(1)= NULL,  
    @Right_Spherical_Points decimal(9, 2)= NULL,  
    @Right_Cyclinderical_Status char(1)= NULL,  
    @Right_Cyclinderical_Points decimal(9, 2)= NULL,  
    @Right_Axix_From int= NULL,  
    @Right_Axix_To int= NULL,  
    @Right_Near_Status char(1)= NULL,  
    @Right_Near_Points decimal(9, 2)= NULL,  
    @Left_Spherical_Status char(1)= NULL,  
    @Left_Spherical_Points decimal(9, 2)= NULL,  
    @Left_Cyclinderical_Status char(1)= NULL,  
    @Left_Cyclinderical_Points decimal(9, 2)= NULL,  
    @Left_Axix_From int= NULL,  
    @Left_Axix_To int= NULL,  
    @Left_Near_Status char(1)= NULL,  
    @Left_Near_Points decimal(9, 2)= NULL,  
    @Douchrome int= NULL,  
    @Achromatopsia varchar(20)= NULL,  
    @RetinoScopy_RightEye int= NULL,   
    @Condition_RightEye varchar(200)= NULL,  
    @Meridian1_RightEye varchar(200)= NULL,  
    @Meridian2_RightEye varchar(200)= NULL,  
    @FinalPrescription_RightEye varchar(200)= NULL,  
    @RetinoScopy_LeftEye int= NULL,   
    @Condition_LeftEye varchar(200)= NULL,  
    @Meridian1_LeftEye varchar(200)= NULL,  
    @Meridian2_LeftEye varchar(200)= NULL,  
    @FinalPrescription_LeftEye varchar(200)= NULL,  
    @Hirchberg_Distance int= NULL,  
    @Hirchberg_Near int= NULL,  
    @Ophthalmoscope_RightEye int= NULL,  
    @PupillaryReactions_RightEye int= NULL,  
    @CoverUncovertTest_RightEye int= NULL,  
    @CoverUncovertTestRemarks_RightEye nvarchar(200)= NULL,  
    @ExtraOccularMuscleRemarks_RightEye nvarchar(200)= NULL,  
    @Ophthalmoscope_LeftEye int= NULL,  
    @PupillaryReactions_LeftEye int= NULL,  
    @CoverUncovertTest_LeftEye int= NULL,  
    @CoverUncovertTestRemarks_LeftEye nvarchar(200)= NULL,  
 @CycloplegicRefraction_RightEye BIT =NULL,  
 @CycloplegicRefraction_LeftEye BIT =NULL,  
 @Conjunctivitis_RightEye BIT =NULL,  
 @Conjunctivitis_LeftEye BIT =NULL,  
 @Scleritis_RightEye BIT =NULL,          
 @Scleritis_LeftEye BIT =NULL,  
 @Nystagmus_RightEye BIT =NULL,          
 @Nystagmus_LeftEye BIT =NULL,  
 @CornealDefect_RightEye BIT =NULL,  
 @CornealDefect_LeftEye BIT =NULL,  
 @Cataract_RightEye BIT =NULL,  
 @Cataract_LeftEye BIT =NULL,  
 @Keratoconus_RightEye BIT =NULL,  
 @Keratoconus_LeftEye BIT =NULL,  
 @Ptosis_RightEye BIT =NULL,  
 @Ptosis_LeftEye BIT =NULL,  
 @LowVision_RightEye BIT =NULL,  
 @LowVision_LeftEye BIT =NULL,  
 @Pterygium_RightEye BIT =NULL,  
 @Pterygium_LeftEye BIT =NULL,  
 @ColorBlindness_RightEye BIT =NULL,  
 @ColorBlindness_LeftEye BIT =NULL,  
 @Others_RightEye BIT =NULL,  
 @Others_LeftEye BIT =NULL,  
 @Fundoscopy_RightEye BIT =NULL,  
 @Fundoscopy_LeftEye BIT =NULL,  
 @Surgery_RightEye BIT =NULL,  
 @Surgery_LeftEye BIT =NULL,  
 @CataractSurgery_RightEye BIT =NULL,  
 @CataractSurgery_LeftEye BIT =NULL,  
 @SurgeryPterygium_RightEye BIT =NULL,  
 @SurgeryPterygium_LeftEye BIT =NULL,  
 @SurgeryCornealDefect_RightEye BIT =NULL,  
 @SurgeryCornealDefect_LeftEye BIT =NULL,  
 @SurgeryPtosis_RightEye BIT =NULL,  
 @SurgeryPtosis_LeftEye BIT =NULL,  
 @SurgeryKeratoconus_RightEye BIT =NULL,  
 @SurgeryKeratoconus_LeftEye BIT =NULL,  
 @Chalazion_RightEye BIT =NULL,  
 @Chalazion_LeftEye BIT =NULL,  
 @Hordeolum_RightEye BIT =NULL,  
 @Hordeolum_LeftEye BIT =NULL,  
 @SurgeryOthers_RightEye BIT =NULL,  
 @SurgeryOthers_LeftEye BIT =NULL,  
 @RightPupilDefects BIT =NULL,  
 @LeftPupilDefects BIT =NULL,  
 @RightSquint_Surgery BIT =NULL,  
 @LeftSquint_Surgery BIT =NULL,  
  
 @LocalityAutoId INT=NULL,  
    @UserId nvarchar(250)= NULL,  
    @EntDate datetime= NULL,  
    @EntOperation nvarchar(100)= NULL,  
    @EntryTerminal nvarchar(200)= NULL,  
    @EntryTerminalIP nvarchar(200)= NULL,  
    @ExtraOccularMuscleRemarks_LeftEye nvarchar(200)= NULL,  
    @ApplicationID nvarchar(20)= NULL,  
    @FormId nvarchar(250)= NULL,  
    @UserEmpId int= NULL,  
    @UserEmpName nvarchar(250)= NULL,  
    @UserEmpCode nvarchar(10)= NULL,  
    @VisualAcuity_RightEye int= NULL,  
    @VisualAcuity_LeftEye int= NULL,  
 @LeftSquint_VA BIT= NULL,  
 @RightSquint_VA BIT= NULL,  
 @LeftAmblyopic_VA BIT= NULL,  
 @RightAmblyopic_VA BIT= NULL,  
 @LeftAmblyopia BIT=NULL,  
 @RightAmblyopia BIT=NULL,  
 @Operation VARCHAR(50)=NULL,  
 @SearchText VARCHAR(1000)=null  
 )  
AS   
    IF @operation = 'Save'  
 BEGIN  
  SELECT @LocalityAutoId=@LocalityAutoId FROM tblLocalityResident cw WHERE cw.ResidentAutoId=@ResidentAutoId  
  INSERT INTO dbo.tblOptometristResident (OptometristResidentTransDate,AutoRefResidentId , ResidentAutoId,LocalityAutoId, HasChiefComplain, ChiefComplainRemarks, HasOccularHistory,  
             OccularHistoryRemarks, HasMedicalHistory, MedicalHistoryRemarks, DistanceVision_RightEye_Unaided,  
             DistanceVision_RightEye_WithGlasses, DistanceVision_RightEye_PinHole, NearVision_RightEye,  
             NeedCycloRefraction_RightEye, NeedCycloRefractionRemarks_RightEye, DistanceVision_LeftEye_Unaided,  
             DistanceVision_LeftEye_WithGlasses, DistanceVision_LeftEye_PinHole, NearVision_LeftEye,  
             NeedCycloRefraction_LeftEye, NeedCycloRefractionRemarks_LeftEye, Right_Spherical_Status,   
             Right_Spherical_Points, Right_Cyclinderical_Status, Right_Cyclinderical_Points, Right_Axix_From, Right_Axix_To,  
             Right_Near_Status, Right_Near_Points, Left_Spherical_Status, Left_Spherical_Points,   
             Left_Cyclinderical_Status, Left_Cyclinderical_Points, Left_Axix_From, Left_Axix_To, Left_Near_Status, Left_Near_Points,   
             Douchrome, Achromatopsia, RetinoScopy_RightEye,  Condition_RightEye, Meridian1_RightEye,   
             Meridian2_RightEye, FinalPrescription_RightEye, RetinoScopy_LeftEye,  Condition_LeftEye,  
             Meridian1_LeftEye, Meridian2_LeftEye, FinalPrescription_LeftEye, Hirchberg_Distance, Hirchberg_Near, Ophthalmoscope_RightEye,   
             PupillaryReactions_RightEye, CoverUncovertTest_RightEye, CoverUncovertTestRemarks_RightEye, ExtraOccularMuscleRemarks_RightEye,  
             Ophthalmoscope_LeftEye, PupillaryReactions_LeftEye, CoverUncovertTest_LeftEye, CoverUncovertTestRemarks_LeftEye,  
             CycloplegicRefraction_RightEye , CycloplegicRefraction_LeftEye , Conjunctivitis_RightEye , Conjunctivitis_LeftEye ,  
                            Scleritis_RightEye , Scleritis_LeftEye , Nystagmus_RightEye , Nystagmus_LeftEye , CornealDefect_RightEye ,CornealDefect_LeftEye , Cataract_RightEye ,  
             Cataract_LeftEye , Keratoconus_RightEye , Keratoconus_LeftEye , Ptosis_RightEye , Ptosis_LeftEye , LowVision_RightEye , LowVision_LeftEye ,  
             Pterygium_RightEye , Pterygium_LeftEye , ColorBlindness_RightEye , ColorBlindness_LeftEye , Others_RightEye , Others_LeftEye , Fundoscopy_RightEye ,  
             Fundoscopy_LeftEye , Surgery_RightEye , Surgery_LeftEye , CataractSurgery_RightEye , CataractSurgery_LeftEye , SurgeryPterygium_RightEye ,  
             SurgeryPterygium_LeftEye , SurgeryCornealDefect_RightEye , SurgeryCornealDefect_LeftEye , SurgeryPtosis_RightEye , SurgeryPtosis_LeftEye ,  
             SurgeryKeratoconus_RightEye , SurgeryKeratoconus_LeftEye , Chalazion_RightEye , Chalazion_LeftEye , Hordeolum_RightEye , Hordeolum_LeftEye ,  
             SurgeryOthers_RightEye , SurgeryOthers_LeftEye,RightPupilDefects,LeftPupilDefects,UserId, EntDate,  
             EntOperation, EntTerminal, EntTerminalIP, ExtraOccularMuscleRemarks_LeftEye, ApplicationID, FormId, UserEmpId, UserEmpName, UserEmpCode,  
             VisualAcuity_RightEye, VisualAcuity_LeftEye, LeftSquint_VA ,RightSquint_VA,LeftAmblyopic_VA  ,RightAmblyopic_VA ,  
             RightAmblyopia,LeftAmblyopia,LeftSquint_Surgery,RightSquint_Surgery  
             )  
  
  SELECT  @OptometristResidentTransDate,@AutoRefResidentId , @ResidentAutoId, @LocalityAutoId,@HasChiefComplain, @ChiefComplainRemarks,   
      @HasOccularHistory, @OccularHistoryRemarks, @HasMedicalHistory, @MedicalHistoryRemarks, @DistanceVision_RightEye_Unaided,   
      @DistanceVision_RightEye_WithGlasses, @DistanceVision_RightEye_PinHole, @NearVision_RightEye,   
      @NeedCycloRefraction_RightEye, @NeedCycloRefractionRemarks_RightEye, @DistanceVision_LeftEye_Unaided,   
      @DistanceVision_LeftEye_WithGlasses, @DistanceVision_LeftEye_PinHole, @NearVision_LeftEye,   
      @NeedCycloRefraction_LeftEye, @NeedCycloRefractionRemarks_LeftEye, @Right_Spherical_Status,   
      @Right_Spherical_Points, @Right_Cyclinderical_Status, @Right_Cyclinderical_Points, @Right_Axix_From,   
      @Right_Axix_To, @Right_Near_Status, @Right_Near_Points, @Left_Spherical_Status, @Left_Spherical_Points,   
      @Left_Cyclinderical_Status, @Left_Cyclinderical_Points, @Left_Axix_From, @Left_Axix_To, @Left_Near_Status,   
      @Left_Near_Points, @Douchrome, @Achromatopsia, @RetinoScopy_RightEye,    
      @Condition_RightEye, @Meridian1_RightEye, @Meridian2_RightEye, @FinalPrescription_RightEye,   
      @RetinoScopy_LeftEye, @Condition_LeftEye, @Meridian1_LeftEye,   
      @Meridian2_LeftEye, @FinalPrescription_LeftEye, @Hirchberg_Distance, @Hirchberg_Near, @Ophthalmoscope_RightEye,   
      @PupillaryReactions_RightEye, @CoverUncovertTest_RightEye, @CoverUncovertTestRemarks_RightEye,   
      @ExtraOccularMuscleRemarks_RightEye, @Ophthalmoscope_LeftEye, @PupillaryReactions_LeftEye,   
      @CoverUncovertTest_LeftEye, @CoverUncovertTestRemarks_LeftEye,   
      @CycloplegicRefraction_RightEye , @CycloplegicRefraction_LeftEye , @Conjunctivitis_RightEye , @Conjunctivitis_LeftEye ,  
    @Scleritis_RightEye ,@Scleritis_LeftEye , @Nystagmus_RightEye , @Nystagmus_LeftEye , @CornealDefect_RightEye , @CornealDefect_LeftEye ,  
    @Cataract_RightEye , @Cataract_LeftEye , @Keratoconus_RightEye , @Keratoconus_LeftEye , @Ptosis_RightEye , @Ptosis_LeftEye ,  
    @LowVision_RightEye , @LowVision_LeftEye , @Pterygium_RightEye , @Pterygium_LeftEye , @ColorBlindness_RightEye , @ColorBlindness_LeftEye ,  
    @Others_RightEye , @Others_LeftEye , @Fundoscopy_RightEye , @Fundoscopy_LeftEye , @Surgery_RightEye , @Surgery_LeftEye ,  
    @CataractSurgery_RightEye , @CataractSurgery_LeftEye , @SurgeryPterygium_RightEye , @SurgeryPterygium_LeftEye , @SurgeryCornealDefect_RightEye ,  
    @SurgeryCornealDefect_LeftEye , @SurgeryPtosis_RightEye , @SurgeryPtosis_LeftEye , @SurgeryKeratoconus_RightEye , @SurgeryKeratoconus_LeftEye ,  
    @Chalazion_RightEye , @Chalazion_LeftEye , @Hordeolum_RightEye , @Hordeolum_LeftEye , @SurgeryOthers_RightEye , @SurgeryOthers_LeftEye ,  
      @RightPupilDefects,@LeftPupilDefects,@UserId, GETDATE(), 'INSERT',   
      @EntryTerminal, @EntryTerminalIP, @ExtraOccularMuscleRemarks_LeftEye, @ApplicationID, @FormId,   
      @UserEmpId, @UserEmpName, @UserEmpCode, @VisualAcuity_RightEye, @VisualAcuity_LeftEye, @LeftSquint_VA ,@RightSquint_VA,@LeftAmblyopic_VA    
      ,@RightAmblyopic_VA ,@RightAmblyopia,@LeftAmblyopia,@LeftSquint_Surgery,@RightSquint_Surgery  
  
      SET @OptometristResidentId=SCOPE_IDENTITY()  
  
  SELECT @OptometristResidentId AS OptometristResidentId,'Successfully Saved' AS RESULT  
 END  
 ELSE IF @operation='Update'  
 BEGIN  
 UPDATE dbo.tblOptometristResident  
  SET    HasChiefComplain = @HasChiefComplain, ChiefComplainRemarks = @ChiefComplainRemarks, HasOccularHistory = @HasOccularHistory,   
           LocalityAutoId=@LocalityAutoId,OccularHistoryRemarks = @OccularHistoryRemarks, HasMedicalHistory = @HasMedicalHistory, MedicalHistoryRemarks = @MedicalHistoryRemarks,   
           DistanceVision_RightEye_Unaided = @DistanceVision_RightEye_Unaided, DistanceVision_RightEye_WithGlasses = @DistanceVision_RightEye_WithGlasses,   
           DistanceVision_RightEye_PinHole = @DistanceVision_RightEye_PinHole, NearVision_RightEye = @NearVision_RightEye,   
           NeedCycloRefraction_RightEye = @NeedCycloRefraction_RightEye, NeedCycloRefractionRemarks_RightEye = @NeedCycloRefractionRemarks_RightEye,   
           DistanceVision_LeftEye_Unaided = @DistanceVision_LeftEye_Unaided, DistanceVision_LeftEye_WithGlasses = @DistanceVision_LeftEye_WithGlasses,   
           DistanceVision_LeftEye_PinHole = @DistanceVision_LeftEye_PinHole, NearVision_LeftEye = @NearVision_LeftEye,   
           NeedCycloRefraction_LeftEye = @NeedCycloRefraction_LeftEye, NeedCycloRefractionRemarks_LeftEye = @NeedCycloRefractionRemarks_LeftEye,   
           Right_Spherical_Status = @Right_Spherical_Status, Right_Spherical_Points = @Right_Spherical_Points,   
           Right_Cyclinderical_Status = @Right_Cyclinderical_Status, Right_Cyclinderical_Points = @Right_Cyclinderical_Points,   
           Right_Axix_From = @Right_Axix_From, Right_Axix_To = @Right_Axix_To, Right_Near_Status = @Right_Near_Status,   
           Right_Near_Points = @Right_Near_Points, Left_Spherical_Status = @Left_Spherical_Status, Left_Spherical_Points = @Left_Spherical_Points,   
           Left_Cyclinderical_Status = @Left_Cyclinderical_Status, Left_Cyclinderical_Points = @Left_Cyclinderical_Points,   
           Left_Axix_From = @Left_Axix_From, Left_Axix_To = @Left_Axix_To, Left_Near_Status = @Left_Near_Status,   
           Left_Near_Points = @Left_Near_Points, Douchrome = @Douchrome, Achromatopsia = @Achromatopsia,   
           RetinoScopy_RightEye = @RetinoScopy_RightEye, CycloplegicRefraction_RightEye = @CycloplegicRefraction_RightEye,   
           Condition_RightEye = @Condition_RightEye, Meridian1_RightEye = @Meridian1_RightEye, Meridian2_RightEye = @Meridian2_RightEye,   
           FinalPrescription_RightEye = @FinalPrescription_RightEye, RetinoScopy_LeftEye = @RetinoScopy_LeftEye,   
            Condition_LeftEye = @Condition_LeftEye,   
           Meridian1_LeftEye = @Meridian1_LeftEye, Meridian2_LeftEye = @Meridian2_LeftEye, FinalPrescription_LeftEye = @FinalPrescription_LeftEye,   
           Hirchberg_Distance = @Hirchberg_Distance, Hirchberg_Near = @Hirchberg_Near, Ophthalmoscope_RightEye = @Ophthalmoscope_RightEye,   
           PupillaryReactions_RightEye = @PupillaryReactions_RightEye, CoverUncovertTest_RightEye = @CoverUncovertTest_RightEye,   
           CoverUncovertTestRemarks_RightEye = @CoverUncovertTestRemarks_RightEye, ExtraOccularMuscleRemarks_RightEye = @ExtraOccularMuscleRemarks_RightEye,   
           Ophthalmoscope_LeftEye = @Ophthalmoscope_LeftEye, PupillaryReactions_LeftEye = @PupillaryReactions_LeftEye,   
           CoverUncovertTest_LeftEye = @CoverUncovertTest_LeftEye, CoverUncovertTestRemarks_LeftEye = @CoverUncovertTestRemarks_LeftEye,  
      CycloplegicRefraction_LeftEye = @CycloplegicRefraction_LeftEye    
      ,Conjunctivitis_RightEye =  @Conjunctivitis_RightEye  
      ,Conjunctivitis_LeftEye =  @Conjunctivitis_LeftEye  
      ,Scleritis_RightEye =   @Scleritis_RightEye  
      ,Scleritis_LeftEye =   @Scleritis_LeftEye  
      ,Nystagmus_RightEye =   @Nystagmus_RightEye  
      ,Nystagmus_LeftEye =   @Nystagmus_LeftEye  
      ,CornealDefect_RightEye =  @CornealDefect_RightEye  
      ,CornealDefect_LeftEye =  @CornealDefect_LeftEye  
      ,Cataract_RightEye =   @Cataract_RightEye  
      ,Cataract_LeftEye =    @Cataract_LeftEye  
      ,Keratoconus_RightEye =   @Keratoconus_RightEye  
      ,Keratoconus_LeftEye =    @Keratoconus_LeftEye  
      ,Ptosis_RightEye =    @Ptosis_RightEye  
      ,Ptosis_LeftEye =    @Ptosis_LeftEye  
      ,LowVision_RightEye =   @LowVision_RightEye  
      ,LowVision_LeftEye =   @LowVision_LeftEye  
      ,Pterygium_RightEye =   @Pterygium_RightEye  
      ,Pterygium_LeftEye =   @Pterygium_LeftEye  
      ,ColorBlindness_RightEye =  @ColorBlindness_RightEye  
      ,ColorBlindness_LeftEye =  @ColorBlindness_LeftEye  
      ,Others_RightEye =    @Others_RightEye  
   ,RightPupilDefects =    @RightPupilDefects,  
   LeftPupilDefects  =    @LeftPupilDefects  
      ,Others_LeftEye =    @Others_LeftEye  
      ,Fundoscopy_RightEye =   @Fundoscopy_RightEye  
      ,Fundoscopy_LeftEye =    @Fundoscopy_LeftEye  
      ,Surgery_RightEye =    @Surgery_RightEye  
      ,Surgery_LeftEye =    @Surgery_LeftEye  
      ,CataractSurgery_RightEye =  @CataractSurgery_RightEye  
      ,CataractSurgery_LeftEye =  @CataractSurgery_LeftEye  
      ,SurgeryPterygium_RightEye = @SurgeryPterygium_RightEye  
      ,SurgeryPterygium_LeftEye =  @SurgeryPterygium_LeftEye  
      ,SurgeryCornealDefect_RightEye =@SurgeryCornealDefect_RightEye  
      ,SurgeryCornealDefect_LeftEye = @SurgeryCornealDefect_LeftEye  
      ,SurgeryPtosis_RightEye =  @SurgeryPtosis_RightEye  
      ,SurgeryPtosis_LeftEye =  @SurgeryPtosis_LeftEye  
      ,SurgeryKeratoconus_RightEye = @SurgeryKeratoconus_RightEye  
      ,SurgeryKeratoconus_LeftEye = @SurgeryKeratoconus_LeftEye  
      ,Chalazion_RightEye =   @Chalazion_RightEye  
      ,Chalazion_LeftEye =   @Chalazion_LeftEye  
      ,Hordeolum_RightEye =   @Hordeolum_RightEye  
      ,Hordeolum_LeftEye =   @Hordeolum_LeftEye  
   ,LeftAmblyopia =    @LeftAmblyopia  
   ,RightAmblyopia =    @RightAmblyopia  
      ,SurgeryOthers_RightEye =  @SurgeryOthers_RightEye  
      ,SurgeryOthers_LeftEye =  @SurgeryOthers_LeftEye,  
   LeftSquint_Surgery =  @LeftSquint_Surgery,  
   RightSquint_Surgery =  @RightSquint_Surgery,  
           UserId = @UserId ,ExtraOccularMuscleRemarks_LeftEye = @ExtraOccularMuscleRemarks_LeftEye,   
           ApplicationID = @ApplicationID, FormId = @FormId, UserEmpId = @UserEmpId, UserEmpName = @UserEmpName,   
           UserEmpCode = @UserEmpCode, VisualAcuity_RightEye = @VisualAcuity_RightEye, VisualAcuity_LeftEye = @VisualAcuity_LeftEye,   
           LeftSquint_VA =@LeftSquint_VA ,RightSquint_VA=@RightSquint_VA,LeftAmblyopic_VA =@LeftAmblyopic_VA,RightAmblyopic_VA =@RightAmblyopic_VA,  
     EntOperation='UPDATE'  
  WHERE  OptometristResidentId = @OptometristResidentId   
  
   SELECT @OptometristResidentId  AS OptometristResidentId,'Successfully Updated' AS RESULT  
  
 END  
  
 ELSE IF @operation='GetById'  
 BEGIN  
       
SELECT ISNULL(OptometristResidentId,'0')OptometristResidentId  
      ,ISNULL(OptometristResidentTransDate,GETDATE())OptometristResidentTransDate  
      ,ISNULL(ResidentAutoId,'0')ResidentAutoId  
      ,ISNULL(LocalityAutoId,'0')LocalityAutoId  
      ,ISNULL(HasChiefComplain,'0')HasChiefComplain  
      ,ISNULL(ChiefComplainRemarks,'')ChiefComplainRemarks  
      ,ISNULL(HasOccularHistory,'0')HasOccularHistory  
      ,ISNULL(OccularHistoryRemarks,'')OccularHistoryRemarks  
      ,ISNULL(HasMedicalHistory,'0')HasMedicalHistory  
      ,ISNULL(MedicalHistoryRemarks,'')MedicalHistoryRemarks  
      ,ISNULL(DistanceVision_RightEye_Unaided,'0')DistanceVision_RightEye_Unaided  
      ,ISNULL(DistanceVision_RightEye_WithGlasses,'0')DistanceVision_RightEye_WithGlasses  
      ,ISNULL(DistanceVision_RightEye_PinHole,'0')DistanceVision_RightEye_PinHole  
      ,ISNULL(NearVision_RightEye,'0')NearVision_RightEye  
      ,ISNULL(NeedCycloRefraction_RightEye,'0')NeedCycloRefraction_RightEye  
      ,ISNULL(NeedCycloRefractionRemarks_RightEye,'')NeedCycloRefractionRemarks_RightEye  
      ,ISNULL(DistanceVision_LeftEye_Unaided,'0')DistanceVision_LeftEye_Unaided  
      ,ISNULL(DistanceVision_LeftEye_WithGlasses,'0')DistanceVision_LeftEye_WithGlasses  
      ,ISNULL(DistanceVision_LeftEye_PinHole,'0')DistanceVision_LeftEye_PinHole  
      ,ISNULL(NearVision_LeftEye,'0')NearVision_LeftEye  
      ,ISNULL(NeedCycloRefraction_LeftEye,'0')NeedCycloRefraction_LeftEye  
      ,ISNULL(NeedCycloRefractionRemarks_LeftEye,'')NeedCycloRefractionRemarks_LeftEye  
      ,ISNULL(Right_Spherical_Status,'0')Right_Spherical_Status  
      ,ISNULL(Right_Spherical_Points,'0')Right_Spherical_Points  
      ,ISNULL(Right_Cyclinderical_Status,'0')Right_Cyclinderical_Status  
      ,ISNULL(Right_Cyclinderical_Points,'0')Right_Cyclinderical_Points  
      ,ISNULL(Right_Axix_From,'0')Right_Axix_From  
      ,ISNULL(Right_Axix_To,'0')Right_Axix_To  
      ,ISNULL(Right_Near_Status,'P')Right_Near_Status  
      ,ISNULL(Right_Near_Points,'0')Right_Near_Points  
      ,ISNULL(Left_Spherical_Status,'P')Left_Spherical_Status  
      ,ISNULL(Left_Spherical_Points,'0')Left_Spherical_Points  
      ,ISNULL(Left_Cyclinderical_Status,'P')Left_Cyclinderical_Status  
      ,ISNULL(Left_Cyclinderical_Points,'0')Left_Cyclinderical_Points  
      ,ISNULL(Left_Axix_From,'0')Left_Axix_From  
      ,ISNULL(Left_Axix_To,'0')Left_Axix_To  
      ,ISNULL(Left_Near_Status,'')Left_Near_Status  
      ,ISNULL(Left_Near_Points,'0')Left_Near_Points  
      ,ISNULL(VisualAcuity_RightEye,'0')VisualAcuity_RightEye  
      ,ISNULL(VisualAcuity_LeftEye,'0')VisualAcuity_LeftEye  
      ,ISNULL(LeftSquint_VA,'0')LeftSquint_VA  
      ,ISNULL(RightSquint_VA,'0')RightSquint_VA  
      ,ISNULL(LeftAmblyopic_VA,'0')LeftAmblyopic_VA  
      ,ISNULL(RightAmblyopic_VA,'0')RightAmblyopic_VA  
      ,ISNULL(AutoRefResidentId,'0')AutoRefResidentId  
      ,ISNULL(Hirchberg_Distance,'0')Hirchberg_Distance  
      ,ISNULL(Hirchberg_Near,'0')Hirchberg_Near  
      ,ISNULL(Ophthalmoscope_RightEye,'0')Ophthalmoscope_RightEye  
      ,ISNULL(Ophthalmoscope_LeftEye,'0')Ophthalmoscope_LeftEye  
      ,ISNULL(PupillaryReactions_RightEye,'0')PupillaryReactions_RightEye  
      ,ISNULL(CoverUncovertTest_RightEye,'0')CoverUncovertTest_RightEye  
      ,ISNULL(CoverUncovertTestRemarks_RightEye,'')CoverUncovertTestRemarks_RightEye  
      ,ISNULL(ExtraOccularMuscleRemarks_RightEye,'')ExtraOccularMuscleRemarks_RightEye  
      ,ISNULL(PupillaryReactions_LeftEye,'0')PupillaryReactions_LeftEye  
      ,ISNULL(CoverUncovertTest_LeftEye,'0')CoverUncovertTest_LeftEye  
      ,ISNULL(CoverUncovertTestRemarks_LeftEye,'0')CoverUncovertTestRemarks_LeftEye  
      ,ISNULL(CycloplegicRefraction_RightEye,'0')CycloplegicRefraction_RightEye  
      ,ISNULL(CycloplegicRefraction_LeftEye,'0')CycloplegicRefraction_LeftEye  
      ,ISNULL(Conjunctivitis_RightEye,'0')Conjunctivitis_RightEye  
      ,ISNULL(Conjunctivitis_LeftEye,'0')Conjunctivitis_LeftEye  
      ,ISNULL(Scleritis_RightEye,'0')Scleritis_RightEye  
      ,ISNULL(Scleritis_LeftEye,'0')Scleritis_LeftEye  
      ,ISNULL(Nystagmus_RightEye,'0')Nystagmus_RightEye  
      ,ISNULL(Nystagmus_LeftEye,'0')Nystagmus_LeftEye  
      ,ISNULL(CornealDefect_RightEye,'0')CornealDefect_RightEye  
      ,ISNULL(CornealDefect_LeftEye,'0')CornealDefect_LeftEye  
      ,ISNULL(Cataract_RightEye,'0')Cataract_RightEye  
      ,ISNULL(Cataract_LeftEye,'0')Cataract_LeftEye  
      ,ISNULL(Keratoconus_RightEye,'0')Keratoconus_RightEye  
      ,ISNULL(Keratoconus_LeftEye,'0')Keratoconus_LeftEye  
      ,ISNULL(Ptosis_RightEye,'0')Ptosis_RightEye  
      ,ISNULL(Ptosis_LeftEye,'0')Ptosis_LeftEye  
      ,ISNULL(LowVision_RightEye,'0')LowVision_RightEye  
      ,ISNULL(LowVision_LeftEye,'0')LowVision_LeftEye  
      ,ISNULL(Pterygium_RightEye,'0')Pterygium_RightEye  
      ,ISNULL(Pterygium_LeftEye,'0')Pterygium_LeftEye  
      ,ISNULL(ColorBlindness_RightEye,'0')ColorBlindness_RightEye  
      ,ISNULL(ColorBlindness_LeftEye,'0')ColorBlindness_LeftEye  
      ,ISNULL(Others_RightEye,'0')Others_RightEye  
      ,ISNULL(Others_LeftEye,'0')Others_LeftEye  
      ,ISNULL(Fundoscopy_RightEye,'0')Fundoscopy_RightEye  
      ,ISNULL(Fundoscopy_LeftEye,'0')Fundoscopy_LeftEye  
      ,ISNULL(Surgery_RightEye,'0')Surgery_RightEye  
      ,ISNULL(Surgery_LeftEye,'0')Surgery_LeftEye  
      ,ISNULL(CataractSurgery_RightEye,'0')CataractSurgery_RightEye  
      ,ISNULL(CataractSurgery_LeftEye,'0')CataractSurgery_LeftEye  
      ,ISNULL(SurgeryPterygium_RightEye,'0')SurgeryPterygium_RightEye  
      ,ISNULL(SurgeryPterygium_LeftEye,'0')SurgeryPterygium_LeftEye  
      ,ISNULL(SurgeryCornealDefect_RightEye,'0')SurgeryCornealDefect_RightEye  
      ,ISNULL(SurgeryCornealDefect_LeftEye,'0')SurgeryCornealDefect_LeftEye  
      ,ISNULL(SurgeryPtosis_RightEye,'0')SurgeryPtosis_RightEye  
      ,ISNULL(SurgeryPtosis_LeftEye,'0')SurgeryPtosis_LeftEye  
      ,ISNULL(SurgeryKeratoconus_RightEye,'0')SurgeryKeratoconus_RightEye  
      ,ISNULL(SurgeryKeratoconus_LeftEye,'0')SurgeryKeratoconus_LeftEye  
      ,ISNULL(Chalazion_RightEye,'0')Chalazion_RightEye  
      ,ISNULL(Chalazion_LeftEye,'0')Chalazion_LeftEye  
      ,ISNULL(Hordeolum_RightEye,'0')Hordeolum_RightEye  
      ,ISNULL(Hordeolum_LeftEye,'0')Hordeolum_LeftEye  
      ,ISNULL(SurgeryOthers_RightEye,'0')SurgeryOthers_RightEye  
      ,ISNULL(SurgeryOthers_LeftEye,'0')SurgeryOthers_LeftEye  
      ,ISNULL(Douchrome,'0')Douchrome  
      ,ISNULL(Achromatopsia,'0')Achromatopsia  
      ,ISNULL(RetinoScopy_RightEye,'0')RetinoScopy_RightEye  
      ,ISNULL(Condition_RightEye,'0')Condition_RightEye  
      ,ISNULL(Meridian1_RightEye,'')Meridian1_RightEye  
      ,ISNULL(Meridian2_RightEye,'')Meridian2_RightEye  
      ,ISNULL(FinalPrescription_RightEye,'')FinalPrescription_RightEye  
      ,ISNULL(RetinoScopy_LeftEye,'0')RetinoScopy_LeftEye  
      ,ISNULL(Condition_LeftEye,'')Condition_LeftEye  
      ,ISNULL(Meridian1_LeftEye,'')Meridian1_LeftEye  
      ,ISNULL(Meridian2_LeftEye,'')Meridian2_LeftEye  
      ,ISNULL(FinalPrescription_LeftEye,'')FinalPrescription_LeftEye  
   ,ISNULL(RightPupilDefects,0)RightPupilDefects  
   ,ISNULL(LeftPupilDefects,0)LeftPupilDefects  
   ,ISNULL(LeftSquint_Surgery,0)LeftSquint_Surgery  
   ,ISNULL(RightSquint_Surgery,0)RightSquint_Surgery  
      ,ISNULL(UserId,'0')UserId  
      ,ISNULL(EntDate,Getdate())  
      ,ISNULL(EntOperation,'0')  
      ,ISNULL(EntTerminal,'0')  
      ,ISNULL(EntTerminalIP,'0')  
      ,ISNULL(ExtraOccularMuscleRemarks_LeftEye,'')ExtraOccularMuscleRemarks_LeftEye,  
   ISNULL(RightAmblyopia,0)RightAmblyopia,ISNULL(LeftAmblyopia,0)LeftAmblyopia  
      ,ISNULL(ApplicationID,'0')  
      ,ISNULL(FormId,'0')  
      ,ISNULL(UserEmpId,'0')  
      ,ISNULL(UserEmpName,'0')  
      ,ISNULL(UserEmpCode,'0')  
    FROM   dbo.tblOptometristResident  
    WHERE  OptometristResidentId = @OptometristResidentId   
 END  
  
 ELSE IF @operation = 'GetListForResidentOptometrist'  
 BEGIN  
 SELECT DISTINCT TOP 10 cw.ResidentAutoId,cw.ResidentCode,cw.ResidentName,c.LocalityName,cw.CNIC,cw.MobileNo  
 FROM tblLocalities c  
 INNER JOIN tblLocalityResident cw ON c.LocalityAutoId=cw.LocalityAutoId  
 INNER JOIN tblAutoRefTestResident artw ON cw.ResidentAutoId=artw.ResidentAutoId  
 ORDER BY cw.ResidentAutoId desc  
 END  
    
  ELSE IF @operation = 'Search'  
 BEGIN  
 SELECT DISTINCT TOP 10 c.ResidentAutoId,c.ResidentCode,c.ResidentName,A.LocalityName,c.CNIC,c.MobileNo  
     FROM tblLocalities A INNER JOIN tblLocalityResident c ON A.LocalityAutoId = c.LocalityAutoId  
     INNER JOIN tblAutoRefTestResident artw ON c.ResidentAutoId=artw.ResidentAutoId  
   WHERE  
   (  
   CAST(c.ResidentCode AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR  
   CAST(A.LocalityName AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR  
   CAST(c.ResidentName AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR  
   CAST(c.CNIC AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR  
   CAST(c.MobileNo AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'      
   )  
   ORDER BY c.ResidentAutoId desc  
 END  
  
   
ELSE IF @operation='NewResidentForOpt'  
BEGIN  
 SELECT DISTINCT TOP 5 cw.ResidentAutoId Id,cw.ResidentName+' | '+cw.ResidentCode text,cw.ResidentCode Code FROM tblLocalityResident cw  
 INNER JOIN tblAutoRefTestResident artr ON cw.ResidentAutoId = artr.ResidentAutoId
 WHERE   
 cw.LocalityAutoId=@LocalityAutoId AND   
 cw.ResidentAutoId NOT IN (SELECT artw.ResidentAutoId FROM tblOptometristResident artw WITH (NOLOCK) WHERE CAST(artw.OptometristResidentTransDate AS DATE) = CAST(@OptometristResidentTransDate AS DATE))  
 AND  (cw.ResidentName LIKE '%'+ISNULL('','')+ '%' OR  
    cw.ResidentCode LIKE '%'+ISNULL('','')+ '%' )  
 ORDER BY cw.ResidentName+' | '+cw.ResidentCode    
END  
  
ELSE IF @operation='EditResidentForOpt'  
BEGIN  
 SELECT DISTINCT TOP 5 cw.ResidentAutoId Id,cw.ResidentName+' | '+cw.ResidentCode text,cw.ResidentCode Code   
 FROM tblLocalityResident cw INNER JOIN tblOptometristResident artw ON cw.ResidentAutoId=artw.ResidentAutoId  
 WHERE   
 cw.LocalityAutoId=@LocalityAutoId   
 AND  (cw.ResidentName LIKE '%'+ISNULL('','')+ '%' OR  
    cw.ResidentCode LIKE '%'+ISNULL('','')+ '%' )  
 ORDER BY cw.ResidentName+' | '+cw.ResidentCode  
END  
  
 ELSE IF @operation= 'GetDatesofResident'  
 BEGIN  
  SELECT CAST(FORMAT(artw.OptometristResidentTransDate,'dd | MMM | yyyy') AS VARCHAR) Text,  
  artw.OptometristResidentId AS Id  
  FROM tblOptometristResident artw  
  WHERE artw.ResidentAutoId=@ResidentAutoId  
  ORDER BY artw.OptometristResidentId desc  
 END  
  
 ELSE IF @operation= 'DeleteOptometristById'  
 BEGIN  
  DELETE FROM tblOptometristResident WHERE OptometristResidentId=@OptometristResidentId  
  SELECT @OptometristResidentId AS OptometristResidentId,'Successfully Deleted' AS RESULT  
 END  
  
  
    
GO


 
           
CREATE OR ALTER PROC [dbo].[Sp_GlassDispenseResident]          
    @GlassDispenseResidentId INT = NULL,          
  @OptometristResidentId INT =NULL,      
    @GlassDispenseResidentTransDate DATETIME   = NULL,          
    @ResidentAutoId INT   = NULL,          
    @VisionwithGlasses_RightEye INT= NULL,          
    @VisionwithGlasses_LeftEye int= NULL,          
 @NearVA_RightEye INT=NULL,        
 @NearVA_LeftEye INT=NULL,        
    @ResidentSatisficaion int= NULL,          
    @Unsatisfied int= NULL,          
    @Unsatisfied_Remarks nvarchar(250)= NULL,          
    @Unsatisfied_Reason int= NULL,          
 @LocalityAutoId INT =NULL,        
    @UserId nvarchar(250)= NULL,          
    @EntDate datetime= NULL,          
    @EntOperation nvarchar(100)= NULL,          
    @EntryTerminal  nvarchar(200)= NULL,          
    @EntryTerminalIP nvarchar(200) =NULL,          
 @UserEmpName NVARCHAR(200)=NULL,        
 @SearchText NVARCHAR(200)=NULL,        
 @Operation VARCHAR(50)=NULL          
AS           
 IF @operation='Save'          
BEGIN          
IF NOT EXISTS(SELECT 1 FROM tblGlassDispenseResident artw WHERE artw.ResidentAutoId=@ResidentAutoId AND CAST(artw.GlassDispenseResidentTransDate AS DATE)=CAST(@GlassDispenseResidentTransDate AS date))          
 BEGIN          
          
INSERT INTO [tblGlassDispenseResident] (      
           OptometristResidentId,GlassDispenseResidentTransDate, ResidentAutoId,       
           VisionwithGlasses_RightEye, VisionwithGlasses_LeftEye,NearVA_RightEye, NearVA_LeftEye,      
           ResidentSatisficaion, Unsatisfied, Unsatisfied_Remarks, Unsatisfied_Reason,      
           UserId, EntDate, EntOperation, EntTerminal, EntTerminalIP)      
 SELECT @OptometristResidentId,@GlassDispenseResidentTransDate, @ResidentAutoId, @VisionwithGlasses_RightEye,           
   @VisionwithGlasses_LeftEye, @NearVA_RightEye,@NearVA_LeftEye,@ResidentSatisficaion,       
   @Unsatisfied, @Unsatisfied_Remarks, @Unsatisfied_Reason,              
   @UserId, GETDATE(), @EntOperation, @EntryTerminal ,           
   @EntryTerminalIP          
          
      SET @GlassDispenseResidentId = SCOPE_IDENTITY()          
     SELECT @GlassDispenseResidentId  AS GlassDispenseResidentId,'Successfully Saved' AS RESULT          
 END          
 ELSE          
    SELECT @GlassDispenseResidentId  AS GlassDispenseResidentId,'Resident Glass Dispense Already Exists in selected Date' AS RESULT          
            
END          
ELSE IF @operation='UPDATE'          
BEGIN          
   UPDATE dbo.tblGlassDispenseResident          
  SET            
      VisionwithGlasses_RightEye = @VisionwithGlasses_RightEye, VisionwithGlasses_LeftEye = @VisionwithGlasses_LeftEye,         
    NearVA_RightEye=@NearVA_RightEye,NearVA_LeftEye=@NearVA_LeftEye,        
      ResidentSatisficaion = @ResidentSatisficaion, Unsatisfied = @Unsatisfied, Unsatisfied_Remarks = @Unsatisfied_Remarks,           
      Unsatisfied_Reason = @Unsatisfied_Reason,      
      UserId = @UserId,  EntOperation = @EntOperation, EntTerminal = @EntryTerminal ,           
      EntTerminalIP = @EntryTerminalIP          
    WHERE  GlassDispenseResidentId = @GlassDispenseResidentId          
  SELECT @GlassDispenseResidentId AS GlassDispenseResidentId,'Successfully Updated' AS RESULT         
END          
          
ELSE IF @operation='GetById'          
BEGIN          
          
    SELECT a.OptometristResidentId,      
     GlassDispenseResidentId,GlassDispenseResidentTransDate, a.ResidentAutoId,       
     VisionwithGlasses_RightEye, VisionwithGlasses_LeftEye,         
   NearVA_RightEye,NearVA_LeftEye,        
           ResidentSatisficaion, Unsatisfied, Unsatisfied_Remarks, Unsatisfied_Reason,      
     a.UserId, a.EntDate, a.EntOperation, a.EntTerminal, a.EntTerminalIP,          
     cw.Distance,cw.Near,cw.WearGlasses          
    FROM   dbo.tblGlassDispenseResident a WITH (NOLOCK)          
 INNER JOIN tblLocalityResident cw ON a.ResidentAutoId = cw.ResidentAutoId          
          
    WHERE  GlassDispenseResidentId = @GlassDispenseResidentId          
END          
          
ELSE IF @operation='DeleteById'          
BEGIN          
 DELETE FROM tblGlassDispenseResident WHERE GlassDispenseResidentId=@GlassDispenseResidentId          
  SELECT @GlassDispenseResidentId  AS GlassDispenseResidentId,'Successfully Deleted' AS RESULT          
END           
        
        
ELSE IF @Operation ='GetDatesofGlassDispenseResident'          
BEGIN          
 SELECT CAST(FORMAT(artw.GlassDispenseResidentTransDate,'dd | MMM | yyyy') AS VARCHAR) Text,          
   artw.GlassDispenseResidentId AS Id          
   FROM tblGlassDispenseResident artw          
   WHERE artw.ResidentAutoId=@ResidentAutoId          
   ORDER BY artw.GlassDispenseResidentId desc          
END          
        
        
ELSE IF @operation='NewResidentForGlassDispense'          
BEGIN          
 SELECT DISTINCT top 5 cw.ResidentAutoId Id, cw.ResidentName +' | '+cw.ResidentCode  text,cw.ResidentCode Code FROM tblLocalityResident cw          
 INNER JOIN tblOptometristResident ow ON cw.ResidentAutoId = ow.ResidentAutoId  AND ow.OptometristResidentTransDate <= CAST(@GlassDispenseResidentTransDate AS DATE)
 WHERE           
 cw.LocalityAutoId=@LocalityAutoId AND           
 cw.ResidentAutoId NOT IN         
   (SELECT artw.ResidentAutoId FROM tblGlassDispenseResident  artw WITH (NOLOCK) WHERE CAST(artw.GlassDispenseResidentTransDate AS DATE) = CAST(@GlassDispenseResidentTransDate AS DATE))          
 AND           
 (cw.ResidentCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR          
 cw.ResidentName LIKE '%'+ISNULL(@SearchText,'')+ '%' )          
 ORDER BY  cw.ResidentName +' | '+cw.ResidentCode           
END          
          
ELSE IF @operation='EditResidentForGlassDispense'          
BEGIN          
 SELECT DISTINCT TOP 5 cw.ResidentAutoId Id, cw.ResidentName +' | '+cw.ResidentCode  text,cw.ResidentCode Code           
 FROM tblLocalityResident cw INNER JOIN tblGlassDispenseResident artw ON cw.ResidentAutoId=artw.ResidentAutoId          
 WHERE           
 cw.LocalityAutoId=@LocalityAutoId           
 AND (cw.ResidentCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR          
 cw.ResidentName LIKE '%'+ISNULL(@SearchText,'')+ '%' )          
 ORDER BY  cw.ResidentName +' | '+cw.ResidentCode           
END          
      
ELSE IF @operation='GetGlassDispenseHistoryByResidentId '      
 BEGIN      
  SELECT TOP 1 artw.OptometristResidentId,CAST(FORMAT(artw.OptometristResidentTransDate,'dd | MMM | yyyy') AS VARCHAR) AS Last_Visit_Date,      
   CASE WHEN artw.Right_Spherical_Status ='P' AND artw.Right_Spherical_Points>0 THEN '+' +CAST(Right_Spherical_Points AS VARCHAR)      
   WHEN artw.Right_Spherical_Status='N'AND artw.Right_Spherical_Points>0 THEN '-' +CAST(Right_Spherical_Points AS VARCHAR)      
   WHEN artw.Right_Spherical_Status='E' THEN 'Error '+CAST(ISNULL(Right_Spherical_Points,'') AS VARCHAR)      
   WHEN artw.Right_Spherical_Status='l' THEN 'Plano '+CAST(ISNULL(Right_Spherical_Points,'') AS VARCHAR)      
   ELSE CAST(Right_Spherical_Points AS VARCHAR) END 'Right Spherical',      
      
   CASE WHEN artw.Left_Spherical_Status ='P' AND artw.Left_Spherical_Points>0 THEN '+' +CAST(Left_Spherical_Points AS VARCHAR)      
   WHEN artw.Left_Spherical_Status='N'AND artw.Left_Spherical_Points>0 THEN '-' +CAST(Left_Spherical_Points AS VARCHAR)      
    WHEN artw.Left_Spherical_Status='E' THEN 'Error '+CAST(ISNULL(Left_Spherical_Points,'') AS VARCHAR)      
   WHEN artw.Left_Spherical_Status='l' THEN 'Plano '+CAST(ISNULL(Left_Spherical_Points,'') AS VARCHAR)      
   ELSE CAST(Left_Spherical_Points AS VARCHAR) END 'Left Spherical',      
      
   CASE WHEN artw.Right_Spherical_Status ='P' AND artw.Left_Spherical_Points>0 THEN '+' +CAST(Left_Spherical_Points AS VARCHAR)      
   WHEN artw.Right_Spherical_Status='N'AND artw.Right_Spherical_Points>0 THEN '-' +CAST(Right_Spherical_Points AS VARCHAR)      
   ELSE CAST(Right_Spherical_Points AS VARCHAR) END 'Right Spherical',      
      
   CASE WHEN artw.Right_Cyclinderical_Status ='P' AND artw.Right_Cyclinderical_Points>0 THEN '+' +CAST(Right_Cyclinderical_Points AS VARCHAR)      
   WHEN artw.Right_Cyclinderical_Status='N'AND artw.Right_Cyclinderical_Points>0 THEN '-' +CAST(Right_Cyclinderical_Points AS VARCHAR)      
   ELSE CAST(Right_Cyclinderical_Points AS VARCHAR) END 'Right Cyclinderical',       
      
         
   CASE WHEN artw.Left_Cyclinderical_Status ='P' AND artw.Left_Cyclinderical_Points>0 THEN '+' +CAST(Left_Cyclinderical_Points AS VARCHAR)      
   WHEN artw.Left_Cyclinderical_Status='N'AND artw.Left_Cyclinderical_Points>0 THEN '-' +CAST(Left_Cyclinderical_Points AS VARCHAR)      
   ELSE CAST(artw.Left_Cyclinderical_Points AS VARCHAR) END 'Left Cyclinderical',       
   artw.Right_Axix_From 'Right Axis' ,      
    artw.Left_Axix_From 'Left Axis' ,ISNULL(cw.WearGlasses,0)WearGlasses,ISNULL(cw.Near,0)Near,      
    ISNULL(cw.Distance,0) Distance,  ISNULL(artw.IPD,0)IPD,CASE WHEN cw.GenderAutoId=1 THEN 'Male' ELSE 'Female' END Gender,      
    cw.Age      
      
  FROM tblOptometristResident  artw      
  INNER JOIN tblLocalityResident  cw ON artw.ResidentAutoId = cw.ResidentAutoId      
  WHERE  artw.ResidentAutoId=@ResidentAutoId AND      
  artw.OptometristResidentTransDate <= CAST(@GlassDispenseResidentTransDate AS DATE)       
  ORDER BY artw.OptometristResidentId desc      
 END      
          
    /*          
    -- Begin Return row code block          
          
    SELECT GlassDispenseResidentTransDate, ResidentAutoId, VisionwithGlasses_RightEye, VisionwithGlasses_LeftEye,           
           ResidentSatisficaion, Unsatisfied, Unsatisfied_Remarks, Unsatisfied_Reason, Right_Spherical_Status,           
           Right_Spherical_Points, Right_Cyclinderical_Status, Right_Cyclinderical_Points, Right_Axix_From,           
           Right_Axix_To, Right_Near_Status, Right_Near_Points, Left_Spherical_Status, Left_Spherical_Points,           
           Left_Cyclinderical_Status, Left_Cyclinderical_Points, Left_Axix_From, Left_Axix_To, Left_Near_Status,           
           Left_Near_Points, FollowupRequired, TreatmentId, Medicines, Prescription, ProvideGlasses,           
           ReferToHospital, UserId, EntDate, EntOperation, EntTerminal, EntTerminalIP          
    FROM   dbo.tblGlassDispenseResident          
    WHERE  GlassDispenseResidentId = @GlassDispenseResidentId          
          
    -- End Return row code block          
          
    */ 
GO

 

CREATE OR ALTER PROC [dbo].[Sp_SetupPublicSpaces]           
@PublicSpacesAutoId INT       =NULL ,          
@PublicSpacesCode nvarchar(10) =NULL,          
@PublicSpacesName nvarchar(250) =NULL,          
@Website nvarchar(250) =null,          
@Address1 nvarchar(1000) =NULL,          
@Address2 nvarchar(1000) =NULL,          
@Address3 nvarchar(1000) = NULL,          
@District nvarchar(1000) =NULL,          
@Town nvarchar(200) =NULL,          
@City nvarchar(100) =NULL,          
@WorkForce INT = null,          
@NameofPerson varchar(100) =NULL,          
@PersonMobile varchar(50) =NULL,          
@PersonRole varchar(100) =NULL,          
@TitleAutoId INT =NULL,          
@UserId nvarchar(250) =NULL,          
@EntDate DATETIME =NULL,          
@EntOperation nvarchar(100) =NULL,          
@FormId nvarchar(250) =NULL,          
@UserEmpId INT =NULL,          
@UserEmpName nvarchar(250) =NULL,          
@UserEmpCode nvarchar(10) =NULL,          
@EnrollmentDate DATETIME =NULL,          
@operation VARCHAR(100)= NULL,          
@EntryTerminal VARCHAR(200)=NULL,          
@EntryTerminalIP VARCHAR(200)=NULL,          
@SearchText VARCHAR(MAX)=NULL          
AS           
    IF @operation = 'Save'          
 Begin          
 IF NOT EXISTS(SELECT 1 FROM tblPublicSpaces  l WHERE l.PublicSpacesName=@PublicSpacesName AND l.City=@City AND l.Address1=@Address1 )          
 BEGIN          
              
    INSERT INTO tblPublicSpaces (PublicSpacesCode, PublicSpacesName, Website, Address1, Address2, Address3, Town, District, City,          
          NameofPerson, PersonMobile, PersonRole, TitleAutoId, UserId, EntDate,           
          EntOperation, EntTerminal, EntTerminalIP,          
          FormId, UserEmpId, UserEmpName, UserEmpCode, EnrollmentDate)          
                  
          
    SELECT '',UPPER(@PublicSpacesName),@Website, @Address1, @Address2,@Address3,@Town, @District, @City, @NameofPerson,           
 @PersonMobile, @PersonRole,@TitleAutoId, @UserId, GETDATE(), 'INSERT', @EntryTerminal, @EntryTerminalIP,           
           @FormId, @UserEmpId, @UserEmpName, @UserEmpCode, ISNULL(@EnrollmentDate,GETDATE())          
               
     SET @PublicSpacesAutoId = SCOPE_IDENTITY()          
     SELECT @PublicSpacesCode =MAX(CAST(cd.PublicSpacesCode AS INT))+1 FROM tblPublicSpaces  cd          
          
   UPDATE tblPublicSpaces SET PublicSpacesCode=CASE WHEN LEN(@PublicSpacesCode)=1 THEN '00'+@PublicSpacesCode          
   WHEN LEN(@PublicSpacesCode )=2 THEN '0'+@PublicSpacesCode          
   ELSE @PublicSpacesCode END          
   WHERE PublicSpacesAutoId=@PublicSpacesAutoId          
          
   SELECT @PublicSpacesAutoId AS PublicSpacesAutoId,'Successfully Saved' AS RESULT          
  END          
  ELSE          
  SELECT @PublicSpacesAutoId AS PublicSpacesAutoId,'Public Spaces with same detail Already Exists.' AS RESULT          
 END          
          
 ELSE IF @operation = 'Update'          
 BEGIN          
 UPDATE dbo.tblPublicSpaces          
    SET    PublicSpacesName = @PublicSpacesName,Website=@Website, Address1 = @Address1, Address2 = @Address2,           
            Address3 = @Address3, Town = @Town, District = @District, City = @City,           
           NameofPerson=@NameofPerson,PersonMobile=@PersonMobile, PersonRole=@PersonRole,          
           UserId = @UserId, EntDate = @EntDate, EntOperation = 'Update', EntTerminal = @EntryTerminal,           
           EntTerminalIP = @EntryTerminalIP, FormId = @FormId, UserEmpId = @UserEmpId, UserEmpName = @UserEmpName,           
           UserEmpCode = @UserEmpCode          
    WHERE  PublicSpacesAutoId = @PublicSpacesAutoId          
 SELECT @PublicSpacesAutoId AS PublicSpacesAutoId,'Successfully Updated' AS RESULT          
 END          
              
 ELSE IF @operation='GetPublicSpacesByiD'          
 BEGIN           
    SELECT c.PublicSpacesAutoId, PublicSpacesCode, PublicSpacesName, Website, Address1, Address2, Address3, Town, District, City,          
 NameofPerson, PersonMobile, PersonRole, TitleAutoId, c.UserId, c.EntDate, c.EntOperation, c.EntTerminal,           
 c.EntTerminalIP, FormId, UserEmpId, UserEmpName, UserEmpCode, EnrollmentDate ,        
 ci.PublicSpacesImageAutoId,ci.PublicSpacesImageAutoId DetailPublicSpacesImageAutoId,ci.PublicSpacesPic,ci.FileType,ci.FileSize,ci.CaptureDate,ci.CaptureRemarks          
    FROM   dbo.tblPublicSpaces AS c   LEFT JOIN tblPublicSpacesImage  ci ON c.PublicSpacesAutoId = ci.PublicSpacesAutoId          
    WHERE  c.PublicSpacesAutoId = @PublicSpacesAutoId           
             
 END          
          
 ELSE IF @operation='GetAllPublicSpaces'          
 BEGIN          
 SELECT  DISTINCT c.PublicSpacesAutoId, c.PublicSpacesCode 'PublicSpaces Code', c.PublicSpacesName 'PublicSpaces Name', ISNULL(ISNULL(Address1,c.Address2),c.Address3) Address          
     FROM tblPublicSpaces  c          
     ORDER BY c.PublicSpacesAutoId desc          
 END          
          
 ELSE IF @operation = 'Search'          
 BEGIN          
 SELECT A.PublicSpacesAutoId, A.PublicSpacesCode, A.PublicSpacesName, ISNULL(ISNULL(Address1,a.Address2),a.Address3) Address          
     FROM tblPublicSpaces A          
   WHERE     (          
   CAST(A.PublicSpacesCode AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR          
   CAST(A.PublicSpacesName AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR          
   CAST(A.Address1 AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR          
   CAST(A.Address2 AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR          
   CAST(A.Address3 AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR          
   CAST(A.District AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR          
   CAST(A.City AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'              
   )          
   ORDER BY A.PublicSpacesAutoId desc          
 END          
            
 ELSE IF @operation='GetPublicSpaces'          
 BEGIN          
 SELECT DISTINCT TOP 5 c.PublicSpacesAutoId Id, c.PublicSpacesCode 'Code', c.PublicSpacesName 'Text'          
     FROM tblPublicSpaces  c          
      WHERE           
     (c.PublicSpacesCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR          
     c.PublicSpacesName LIKE '%'+ISNULL(@SearchText,'')+ '%' )          
     ORDER BY c.PublicSpacesAutoId desc          
 END          
  ELSE IF @operation = 'Delete'          
 BEGIN          
  DELETE          
  FROM   dbo.tblPublicSpaces          
  WHERE  PublicSpacesAutoId = @PublicSpacesAutoId          
  IF EXISTS(SELECT 1 FROM tblPublicSpacesImage  ci WHERE ci.PublicSpacesAutoId=@PublicSpacesAutoId)          
  BEGIN          
   DELETE FROM tblPublicSpacesImage WHERE PublicSpacesAutoId=@PublicSpacesAutoId          
  END          
  SELECT @PublicSpacesAutoId AS PublicSpacesAutoId,'Successfully Deleted' AS RESULT          
 END          
          
 ELSE IF @operation='GetNewPublicSpacesForAutoRef'          
 BEGIN          
 SELECT  DISTINCT TOP 5 c.PublicSpacesAutoId Id, c.PublicSpacesCode 'Code', c.PublicSpacesName 'Text'          
     FROM tblPublicSpaces  c INNER JOIN tblPublicSpacesResident   cw ON c.PublicSpacesAutoId = cw.PublicSpacesAutoId          
     Left JOIN tblPublicSpacesAutoRefTestResident artw ON cw.ResidentAutoId=artw.ResidentAutoId          
     WHERE           
     (c.PublicSpacesCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR          
     c.PublicSpacesName LIKE '%'+ISNULL(@SearchText,'')+ '%' )          
     ORDER BY c.PublicSpacesName          
 END          
          
 ELSE IF @operation='GetEditPublicSpacesForAutoRef'          
 BEGIN          
 SELECT DISTINCT TOP 5 c.PublicSpacesAutoId Id, c.PublicSpacesCode 'Code', c.PublicSpacesName 'Text'          
      FROM tblPublicSpaces  c INNER JOIN tblPublicSpacesResident  cw ON c.PublicSpacesAutoId = cw.PublicSpacesAutoId          
     Left JOIN tblPublicSpacesAutoRefTestResident  artw ON cw.ResidentAutoId=artw.ResidentAutoId          
     WHERE           
     (c.PublicSpacesCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR          
     c.PublicSpacesName LIKE '%'+ISNULL(@SearchText,'')+ '%' )          
     ORDER BY c.PublicSpacesName          
 END           
    
 ELSE IF @operation='GetNewPublicSpacesForOptometristResident'            
 BEGIN            
 SELECT DISTINCT  TOP 5  c.PublicSpacesAutoId Id, c.PublicSpacesCode 'Code', c.PublicSpacesName 'Text'            
     FROM tblPublicSpaces  c INNER JOIN tblPublicSpacesResident   cw ON c.PublicSpacesAutoId = cw.PublicSpacesAutoId        
     JOIN tblPublicSpacesAutoRefTestResident artw ON cw.ResidentAutoId=artw.ResidentAutoId            
     WHERE            
     (c.PublicSpacesCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR            
     c.PublicSpacesName LIKE '%'+ISNULL(@SearchText,'')+ '%' )            
     ORDER BY c.PublicSpacesName            
 END            
            
 ELSE IF @operation='GetEditPublicSpacesForOptometristResident'            
 BEGIN            
 SELECT  DISTINCT TOP 5 c.PublicSpacesAutoId Id, c.PublicSpacesCode 'Code', c.PublicSpacesName 'Text'            
     FROM tblPublicSpaces c INNER JOIN tblPublicSpacesResident   cw ON c.PublicSpacesAutoId = cw.PublicSpacesAutoId            
     INNER JOIN tblPublicSpacesAutoRefTestResident artw ON cw.ResidentAutoId=artw.ResidentAutoId         
     INNER JOIN tblPublicSpacesOptometristResident   ow ON artw.AutoRefResidentId=ow.AutoRefResidentId            
     WHERE            
     (c.PublicSpacesCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR            
     c.PublicSpacesName LIKE '%'+ISNULL(@SearchText,'')+ '%' )            
     ORDER BY c.PublicSpacesName            
 END   
  
 ELSE IF @operation='GetNewPublicSpacesForGlassDispense'          
 BEGIN          
  SELECT  DISTINCT TOP 5 c.PublicSpacesAutoId Id, c.PublicSpacesCode 'Code', c.PublicSpacesName 'Text'          
  FROM tblPublicSpaces c INNER JOIN tblPublicSpacesResident cw ON c.PublicSpacesAutoId = cw.PublicSpacesAutoId          
  INNER JOIN tblPublicSpacesOptometristResident  ow ON cw.ResidentAutoId = ow.ResidentAutoId      
  --Left JOIN tblGlassDispenseResident  artw ON cw.ResidentAutoId=artw.ResidentAutoId          
      
  WHERE           
  (c.PublicSpacesCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR          
  c.PublicSpacesName LIKE '%'+ISNULL(@SearchText,'')+ '%' )          
  ORDER BY c.PublicSpacesName          
 END          
          
 ELSE IF @operation='GetEditPublicSpacesForGlassDispense'          
 BEGIN          
  SELECT DISTINCT TOP 5  c.PublicSpacesAutoId Id, c.PublicSpacesCode 'Code', c.PublicSpacesName 'Text'          
  FROM tblPublicSpaces c INNER JOIN tblPublicSpacesResident cw ON c.PublicSpacesAutoId = cw.PublicSpacesAutoId          
  INNER JOIN tblPublicSpacesGlassDispenseResident  artw ON cw.ResidentAutoId=artw.ResidentAutoId          
  WHERE           
  (c.PublicSpacesCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR          
  c.PublicSpacesName LIKE '%'+ISNULL(@SearchText,'')+ '%' )          
  ORDER BY c.PublicSpacesName    
  
 END
GO

 

CREATE OR ALTER PROCEDURE [dbo].[sp_SetupPublicSpacesImage](  
 @PublicSpacesImageAutoId INT=NULL,  
 @PublicSpacesAutoId INT = NULL,    
 @PublicSpacesPic  VARCHAR(MAX) = NULL,  
 @FileType  NVARCHAR(20) = NULL,  
 @CaptureDate DATE=NULL,  
 @FileSize  INT = NULL,  
 @CaptureRemarks  VARCHAR(500)=NULL,  
 @UserId nvarchar(500) = NULL,    
 @EntDate DATETIME = NULL,    
 @EntOperation nvarchar(200) = NULL,    
 @EntryTerminal nvarchar(400) = NULL,    
 @EntryTerminalIP  nvarchar(400)  = NULL,  
 @UserEmpName NVARCHAR(100)=NULL,  
 @operation VARCHAR(50)= NULL  
)  
AS  
IF @operation = 'Save'  
BEGIN  
     INSERT INTO tblPublicSpacesImage (PublicSpacesAutoId, PublicSpacesPic, FileType, FileSize, CaptureDate, CaptureRemarks, UserId, EntDate, EntOperation, EntTerminal, EntTerminalIP)  
  SELECT   @PublicSpacesAutoId, @PublicSpacesPic, SUBSTRING(@FileType,1,10), @FileSize,@CaptureDate,@CaptureRemarks, @UserId, @EntDate, 'INSERT', @EntryTerminal, @EntryTerminalIP    
  SET @PublicSpacesImageAutoId=SCOPE_IDENTITY()  
  SELECT  @PublicSpacesImageAutoId PublicSpacesImageAutoId ,'Successfully Saved' AS Result  
END  
  
ELSE IF @operation='Update'  
BEGIN  
  
  UPDATE tblPublicSpacesImage SET PublicSpacesPic = @PublicSpacesPic, FileType = @FileType, FileSize = @FileSize,   
  UserId = @UserId, EntDate = @EntDate, EntOperation = 'UPDATE', EntTerminal = @EntryTerminal, EntTerminalIP = @EntryTerminalIP      
  WHERE PublicSpacesAutoId = @PublicSpacesAutoId  
  SELECT @PublicSpacesAutoId AS PublicSpacesAutoId,'Successfully Updated' AS RESULT  
END  
  
  
ELSE IF @operation='Delete'  
BEGIN  
  
  DELETE FROM tblPublicSpacesImage WHERE PublicSpacesImageAutoId=@PublicSpacesImageAutoId  
  SELECT @PublicSpacesImageAutoId AS PublicSpacesImageAutoId,'Successfully Deleted' AS RESULT  
END  
GO


 
 
CREATE OR ALTER PROCEDURE [dbo].[Sp_PublicSpacesResident](
@ResidentAutoId int=NULL,
@PublicSpacesAutoId int=NULL,
@PublicSpacesCode VARCHAR(20)=NULL,
@ResidentCode nvarchar(15)=NULL,
@ResidentName nvarchar(500)=NULL,
@RelationType NVARCHAR(100)=NULL,
@RelationName nvarchar(500)=NULL,
@Age int=NULL,
@GenderAutoId int=NULL,
@DecreasedVision bit=NULL,
@Distance BIT= NULL,
@Near BIT= NULL,
@WearGlasses bit=NULL,
@CNIC VARCHAR(30) =NULL,
@UserId nvarchar(250)=NULL,
@EnrollmentDate DATETIME =NULL,
@EntryTerminal VARCHAR(200)=NULL,
@EntryTerminalIP VARCHAR(200)=NULL,
@HasOccularHistory bit=NULL,
@OccularHistoryRemarks nvarchar(500)=NULL,
@HasMedicalHistory bit=NULL,
@MedicalHistoryRemarks nvarchar(500)=NULL,
@HasChiefComplain bit=NULL,
@ChiefComplainRemarks nvarchar(500)=NULL,
@ResidentTestDate datetime=NULL,
@ResidentRegNo varchar(20)=NULL,
@MobileNo VARCHAR(15)=NULL,
@SectionAutoId int=NULL,
@ApplicationID nvarchar(20)=NULL,
@FormId nvarchar(250)=NULL,
@UserEmpId int=NULL,
@UserEmpName nvarchar(250)=NULL,
@UserEmpCode nvarchar(10)=NULL,
@Religion bit=NULL,
@operation VARCHAR(50)= NULL,
@SearchText VARCHAR(MAX)=NULL
)
AS
DECLARE @ResidentNewCode VARCHAR(10)=NULL
IF @operation='Save'
BEGIN
	IF NOT EXISTS(SELECT 1 FROM tblPublicSpacesResident c WITH (NOLOCK) WHERE c.ResidentName=@ResidentName AND c.CNIC=@CNIC )
	BEGIN
	BEGIN TRAN
	BEGIN TRy
	SELECT @PublicSpacesAutoId=c.PublicSpacesAutoId FROM tblPublicSpaces   c WHERE c.PublicSpacesCode=@PublicSpacesCode
	EXEC Sp_GetCode @CodeType = 'PublicSpacesResident',@CodeLength = 4	   ,@PreFix = '07'	   ,@PublicSpacesCode = @PublicSpacesCode	  
					,@PublicSpacesId = @PublicSpacesAutoId   ,@operation = 'GetPublicSpacesCode',@Code = @ResidentNewCode OUTPUT
				 
			INSERT INTO tblPublicSpacesResident (PublicSpacesAutoId, ResidentCode, ResidentName, RelationType, RelationName,
						Age, GenderAutoId, CNIC, UserId, EntDate, EntOperation, EntTerminal, EntTerminalIP, 
						WearGlasses, Distance, Near, DecreasedVision, HasOccularHistory, OccularHistoryRemarks,
						HasMedicalHistory, MedicalHistoryRemarks, HasChiefComplain, ChiefComplainRemarks,
						ResidentTestDate, ResidentRegNo, SectionAutoId, ApplicationID, FormId, UserEmpId,
						UserEmpName, UserEmpCode, Religion, MobileNo, PublicSpacesCode)			

			SELECT @PublicSpacesAutoId,@ResidentNewCode, @ResidentName, @RelationType,@RelationName, 
                    @Age, @GenderAutoId,@CNIC,@UserId,@EnrollmentDate,'INSERT', @EntryTerminal, @EntryTerminalIP,
					@WearGlasses,@Distance,@Near,@DecreasedVision,@HasOccularHistory,@OccularHistoryRemarks
					,@HasMedicalHistory, @MedicalHistoryRemarks, @HasChiefComplain, @ChiefComplainRemarks, 
					@ResidentTestDate,@ResidentRegNo,@SectionAutoId,@ApplicationID,@FormId,@UserEmpId,
					@UserEmpName,@UserEmpCode,@Religion,@MobileNo,@PublicSpacesCode

			  SET @ResidentAutoId = SCOPE_IDENTITY()

			EXEC Sp_GetCode @CodeType = 'PublicSpacesResident',@CodeLength = 4	   ,@PreFix = '07'	   ,@PublicSpacesCode = @PublicSpacesCode,
							@PublicSpacesId = @PublicSpacesAutoId   ,@operation = 'UpdatePublicSpacesCode'
			SELECT @ResidentAutoId AS ResidentAutoId,'Successfully Saved: <br> Resident Code: '+@ResidentNewCode AS RESULT
			Commit
END TRY
BEGIN CATCH
		SELECT @ResidentAutoId AS ResidentAutoId,'Error'+ERROR_MESSAGE() AS RESULT
ROLLBACK TRAN
END Catch
		END
		ELSE
		SELECT @ResidentAutoId AS ResidentAutoId,'Resident with same detail Already Exists.' AS RESULT
END
ELSE IF @operation = 'Update'
BEGIN
BEGIN TRAN
BEGIN TRy

	IF(@PublicSpacesCode != (SELECT top 1 cw.PublicSpacesCode FROM tblPublicSpacesResident cw WITH (NOLOCK) WHERE cw.ResidentAutoId=@ResidentAutoId))
	BEGIN
		SELECT @PublicSpacesAutoId=c.PublicSpacesAutoId FROM tblPublicSpacesResident c WHERE c.PublicSpacesCode=@PublicSpacesCode 
		EXEC Sp_GetCode @CodeType = 'PublicSpacesResident',@CodeLength = 4	   ,@PreFix = '07'	   ,
		@PublicSpacesCode = @PublicSpacesCode,@PublicSpacesId= @PublicSpacesAutoId,@operation = 'GetPublicSpacesCode',@Code = @ResidentNewCode OUTPUT

		EXEC Sp_GetCode @CodeType = 'PublicSpacesResident',@CodeLength = 4	   ,@PreFix = '07'	   ,@PublicSpacesCode = @PublicSpacesCode,@PublicSpacesId = @PublicSpacesAutoId
		,@operation = 'UpdatePublicSpacesCode'

		SET @ResidentCode=@ResidentNewCode
	END
	ELSE
	BEGIN
		SELECT @ResidentCode=cw.ResidentCode FROM tblPublicSpacesResident cw WITH (NOLOCK) WHERE cw.ResidentAutoId=@ResidentAutoId
	END
		UPDATE tblPublicSpacesResident 
		SET PublicSpacesAutoId = @PublicSpacesAutoId
		   ,ResidentName = @ResidentName 
		   ,ResidentCode=@ResidentCode
		   ,RelationType=@RelationType
		   ,RelationName = @RelationName 
		   ,Age = @Age 
		   ,GenderAutoId = @GenderAutoId
		   ,CNIC = @CNIC
		   ,DecreasedVision = @DecreasedVision 
		   ,Near=@Near
		   ,Distance=@Distance
		   ,EntOperation = 'Update'
		   ,WearGlasses = @WearGlasses 
		   ,HasOccularHistory = @HasOccularHistory 
		   ,OccularHistoryRemarks = @OccularHistoryRemarks 
		   ,HasMedicalHistory = @HasMedicalHistory 
		   ,MedicalHistoryRemarks = @MedicalHistoryRemarks 
		   ,HasChiefComplain = @HasChiefComplain 
		   ,ChiefComplainRemarks = @ChiefComplainRemarks 
		   ,ResidentTestDate = @ResidentTestDate
		   ,MobileNo = @MobileNo
		   ,Religion = @Religion 
			WHERE 
			ResidentAutoId = @ResidentAutoId

				SELECT @ResidentAutoId AS ResidentAutoId,'Successfully Updated: <br> '+@ResidentCode AS RESULT
			Commit
END TRY
BEGIN CATCH
		SELECT @ResidentAutoId AS ResidentAutoId,'Error Updated: '+ERROR_MESSAGE() AS RESULT
ROLLBACK TRAN
END Catch
END

ELSE IF @operation = 'Delete'
	BEGIN
		DELETE
		FROM   dbo.tblPublicSpacesResident
		WHERE  ResidentAutoId = @ResidentAutoId
		IF EXISTS(SELECT 1 FROM tblPublicSpacesResidentImage ci WHERE ci.PublicSpacesAutoId=@PublicSpacesAutoId)
		BEGIN
			DELETE FROM tblPublicSpacesResidentImage WHERE ResidentAutoId=@ResidentAutoId
		END
		SELECT @ResidentAutoId AS ResidentAutoId,'Successfully Deleted' AS RESULT
	END

ELSE IF @operation='GetResidentByiD'
	BEGIN
	SELECT	c.ResidentAutoId,c.PublicSpacesAutoId,c.PublicSpacesCode, c.ResidentCode, c.ResidentName,c.RelationType,
			c.RelationName, c.Age, ISNULL(c.GenderAutoId,0)GenderAutoId , c.CNIC, c.DecreasedVision,c.UserId,
			c.EntDate, c.Near,c.Distance,	c.EntOperation, c.EntTerminal, c.EntTerminalIP, c.WearGlasses,
			c.HasOccularHistory, c.OccularHistoryRemarks, c.HasMedicalHistory, c.MedicalHistoryRemarks,c.EntDate EnrollmentDate,c.MobileNo,
			c.HasChiefComplain, c.ChiefComplainRemarks, c.ResidentTestDate, c.ResidentRegNo, c.SectionAutoId, c.ApplicationID, c.FormId, c.UserEmpId,c1.PublicSpacesName,
			c.UserEmpName, c.UserEmpCode, c.Religion, c.EntDate,ci.ResidentImageAutoId,ci.ResidentAutoId,
			ISNULL(ci.CaptureRemarks,'')CaptureRemarks	,ci.PublicSpacesAutoId DetailPublicSpacesAutoId,ci.FileType,ci.FileSize,ci.ResidentPic
		   FROM tblPublicSpacesResident c	 INNER JOIN tblPublicSpaces  c1 ON c1.PublicSpacesAutoId =c.PublicSpacesAutoId
		   LEFT JOIN tblPublicSpacesResidentImage ci ON c.ResidentAutoId = ci.ResidentAutoId
		WHERE 
		c.ResidentAutoId=@ResidentAutoId
	END

--	ELSE IF @operation='GetAllWorker'
--	BEGIN
--	SELECT TOP 10 c.WorkerAutoId,c.WorkerCode,c.WorkerName,c1.CompanyName,c.CNIC,c.MobileNo
--		   FROM tblPublicSpacesResident c INNER JOIN tblCompany c1 ON c.CompanyAutoId = c1.CompanyAutoId
--		   ORDER BY c.WorkerAutoId desc
--	END

	
	ELSE IF @operation = 'Search'
	BEGIN
	SELECT TOP 10 c.ResidentAutoId,c.ResidentCode,c.ResidentName,A.PublicSpacesName,c.CNIC,c.MobileNo
		   FROM tblPublicSpaces   A INNER JOIN tblPublicSpacesResident c ON A.PublicSpacesAutoId = c.PublicSpacesAutoId
		 WHERE
		 (
			 CAST(c.ResidentCode AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR
			 CAST(A.PublicSpacesName AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR
			 CAST(c.ResidentName AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR
			 CAST(c.CNIC AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR
			 CAST(c.MobileNo AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'    
		 )
		 ORDER BY c.ResidentAutoId desc
	END

	ELSE IF @operation= 'GetResidents'
	BEGIN
	SELECT DISTINCT  TOP 5  cw.ResidentAutoId 'Id', cw.ResidentCode 'Code', cw.ResidentName +' | '+cw.ResidentCode 'Text'
		   FROM tblPublicSpacesResident cw WITH(NOLOCK) 
		   WHERE
		   cw.PublicSpacesAutoId=@PublicSpacesAutoId AND 
		   (cw.ResidentCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR
		   cw.ResidentName LIKE '%'+ISNULL(@SearchText,'')+ '%' )
		   ORDER BY  cw.ResidentName+' | '+cw.ResidentCode 
	END
	ELSE IF @operation= 'DeleteWorkerById'
	BEGIN
		DELETE FROM tblPublicSpacesResident WHERE ResidentAutoId=@ResidentAutoId
		DELETE FROM tblPublicSpacesResidentImage WHERE ResidentAutoId=@ResidentAutoId

		SELECT @ResidentAutoId AS ResidentAutoId,'Successfully Deleted' AS RESULT

		
	END
GO


 

CREATE OR ALTER PROCEDURE [dbo].[sp_PublicSpacesResidentImage](  
 @ResidentAutoId INT =NULL,  
 @ResidentImageAutoId INT=NULL,  
 @PublicSpacesAutoId INT = NULL,    
 @ResidentPic  VARCHAR(MAX) = NULL,  
 @FileType  NVARCHAR(20) = NULL,  
 @FileSize  INT = NULL,  
 @CaptureRemarks  VARCHAR(500)=NULL,  
 @UserId nvarchar(500) = NULL,    
 @EntDate DATETIME = NULL,    
 @EntOperation nvarchar(200) = NULL,    
 @EntryTerminal nvarchar(400) = NULL,    
 @EntryTerminalIP  nvarchar(400)  = NULL,  
 @UserEmpName NVARCHAR(100)=NULL,  
 @operation VARCHAR(50)= NULL  
)  
AS  
IF @operation = 'Save'  
BEGIN  
     INSERT INTO tblPublicSpacesResidentImage (ResidentAutoId, PublicSpacesAutoId, ResidentPic, FileType, FileSize, CaptureDate, CaptureRemarks, UserId, EntDate, EntOperation, EntTerminal, EntTerminalIP)  
  SELECT  @ResidentAutoId, @PublicSpacesAutoId, @ResidentPic, SUBSTRING(@FileType,1,10), @FileSize,GETDATE(),@CaptureRemarks, @UserId, @EntDate, 'INSERT', @EntryTerminal, @EntryTerminalIP    
  SET @ResidentImageAutoId=SCOPE_IDENTITY()  
  SELECT  @ResidentImageAutoId PublicSpacesImageAutoId ,'Successfully Saved' AS Result  
END  
  
ELSE IF @operation='Update'  
BEGIN  
  
  UPDATE tblPublicSpacesResidentImage SET ResidentPic = @ResidentPic, FileType = @FileType, FileSize = @FileSize,   
  UserId = @UserId, EntDate = @EntDate, EntOperation = 'UPDATE', EntTerminal = @EntryTerminal, EntTerminalIP = @EntryTerminalIP      
  WHERE @ResidentAutoId = @ResidentAutoId  
  SELECT @ResidentAutoId AS ResidentAutoId,'Successfully Updated' AS RESULT  
END  
  
  
ELSE IF @operation='Delete'  
BEGIN  
  
  DELETE FROM tblPublicSpacesResidentImage WHERE ResidentImageAutoId=@ResidentImageAutoId  
  SELECT @ResidentImageAutoId AS ResidentImageAutoId,'Successfully Deleted' AS RESULT  
END  
GO


 
CREATE OR ALTER PROCEDURE [dbo].[Sp_PublicSpacesAutoRefTestResident](    
 @PublicSpacesAutoId INT =NULL,    
 @AutoRefResidentId INT = NULL,    
    @AutoRefResidentTransId varchar(15) = NULL,    
    @AutoRefResidentTransDate datetime = NULL,    
    @ResidentAutoId int= NULL,    
    @Right_Spherical_Status char(1)= NULL,    
    @Right_Spherical_Points decimal(9, 2)= NULL,    
    @Right_Cyclinderical_Status char(1)= NULL,    
    @Right_Cyclinderical_Points decimal(9, 2)= NULL,    
    @Right_Axix_From int= NULL,    
    @Right_Axix_To int= NULL,    
    @Left_Spherical_Status char(1)= NULL,    
    @Left_Spherical_Points decimal(9, 2)= NULL,    
    @Left_Cyclinderical_Status char(1)= NULL,    
    @Left_Cyclinderical_Points decimal(9, 2)= NULL,    
    @Left_Axix_From int= NULL,    
    @Left_Axix_To int= NULL,    
 @IPD INT = NULL,     
    @UserId nvarchar(250)= NULL,    
    @EntDate datetime= NULL,    
    @EntOperation nvarchar(100)= NULL,    
    @EntryTerminal nvarchar(200)= NULL,    
    @EntryTerminalIP nvarchar(200)= NULL,    
    @ApplicationID nvarchar(20)= NULL,    
    @FormId nvarchar(250)= NULL,    
    @UserEmpId int= NULL,    
    @UserEmpName nvarchar(250)= NULL,    
    @UserEmpCode nvarchar(10)= NULL,    
 @operation VARCHAR(50)= NULL,    
 @SearchText VARCHAR(MAX)=NULL    
 )    
 AS    
IF @operation='Save'    
 BEGIN    
 IF NOT EXISTS(SELECT 1 FROM tblPublicSpacesAutoRefTestResident   artw WHERE artw.ResidentAutoId=@ResidentAutoId AND CAST(artw.AutoRefResidentTransDate AS DATE)=CAST(@AutoRefResidentTransDate AS date))    
 BEGIN    
  INSERT INTO dbo.tblPublicSpacesAutoRefTestResident (AutoRefResidentTransId, AutoRefResidentTransDate,     
                                          ResidentAutoId, Right_Spherical_Status, Right_Spherical_Points,     
                                          Right_Cyclinderical_Status, Right_Cyclinderical_Points, Right_Axix_From,     
                                          Right_Axix_To, Left_Spherical_Status, Left_Spherical_Points,     
                                          Left_Cyclinderical_Status, Left_Cyclinderical_Points, Left_Axix_From,     
                                          Left_Axix_To,IPD, UserId, EntDate, EntOperation, EntTerminal,     
                                          EntTerminalIP, ApplicationID, FormId, UserEmpId, UserEmpName,     
                                          UserEmpCode)    
    SELECT @AutoRefResidentTransId, @AutoRefResidentTransDate, @ResidentAutoId, @Right_Spherical_Status,     
           @Right_Spherical_Points, @Right_Cyclinderical_Status, @Right_Cyclinderical_Points, @Right_Axix_From,     
           @Right_Axix_To, @Left_Spherical_Status, @Left_Spherical_Points, @Left_Cyclinderical_Status,     
           @Left_Cyclinderical_Points, @Left_Axix_From, @Left_Axix_To,@IPD, @UserId, GETDATE(), 'INSERT',     
           @EntryTerminal, @EntryTerminalIP, @ApplicationID, @FormId, @UserEmpId, @UserEmpName, @UserEmpCode    
     SET @AutoRefResidentId = SCOPE_IDENTITY()    
     SELECT @AutoRefResidentId  AS AutoRefResidentId,'Successfully Saved' AS RESULT    
 END    
 ELSE    
    SELECT @AutoRefResidentId  AS AutoRefResidentId,'Resident Auto Refraction Already Exists in selected Date' AS RESULT    
 END    
ELSE IF @operation='Update'    
 BEGIN    
  UPDATE dbo.tblPublicSpacesAutoRefTestResident    
  SET    AutoRefResidentTransId = @AutoRefResidentTransId, ResidentAutoId = @ResidentAutoId, Right_Spherical_Status = @Right_Spherical_Status,     
    Right_Spherical_Points = @Right_Spherical_Points, Right_Cyclinderical_Status = @Right_Cyclinderical_Status,     
    Right_Cyclinderical_Points = @Right_Cyclinderical_Points, Right_Axix_From = @Right_Axix_From,     
    Right_Axix_To = @Right_Axix_To, Left_Spherical_Status = @Left_Spherical_Status, Left_Spherical_Points = @Left_Spherical_Points,     
    Left_Cyclinderical_Status = @Left_Cyclinderical_Status, Left_Cyclinderical_Points = @Left_Cyclinderical_Points,     
    Left_Axix_From = @Left_Axix_From, Left_Axix_To = @Left_Axix_To,IPD=@IPD, UserId = @UserId,    
    EntOperation = 'Update', EntTerminal = @EntryTerminal, EntTerminalIP = @EntryTerminalIP,     
    UserEmpId = @UserEmpId, UserEmpName = @UserEmpName, UserEmpCode = @UserEmpCode    
  WHERE  AutoRefResidentId = @AutoRefResidentId     
    
  SELECT @AutoRefResidentId  AS AutoRefResidentId,'Successfully Updated' AS RESULT    
 END    
    
ELSE IF @operation='GetByAutoRefResidentId '    
 BEGIN    
  SELECT AutoRefResidentId, AutoRefResidentTransId, AutoRefResidentTransDate, ResidentAutoId, Right_Spherical_Status, Right_Spherical_Points,     
  Right_Cyclinderical_Status, Right_Cyclinderical_Points, Right_Axix_From, Right_Axix_To, Left_Spherical_Status, Left_Spherical_Points,     
  Left_Cyclinderical_Status, Left_Cyclinderical_Points, Left_Axix_From, Left_Axix_To, ISNULL(IPD,0)IPD,UserId, EntDate, EntOperation, EntTerminal,    
  EntTerminalIP, ApplicationID, FormId, UserEmpId, UserEmpName, UserEmpCode ,0 WearGlasses    
  FROM   dbo.tblPublicSpacesAutoRefTestResident    
  WHERE  AutoRefResidentId = @AutoRefResidentId      
END    
ELSE IF @operation='GetAutoRefByResidentId '    
 BEGIN    
  SELECT     
      a.AutoRefResidentId, a.AutoRefResidentTransId, CAST(a.AutoRefResidentTransDate AS varchar) 'Test Date', a.ResidentAutoId AutoRefResidentId,     
      a.Right_Spherical_Status, CASE WHEN  ISNULL(a.Right_Spherical_Points,0) > 0 THEN '+'+CAST(a.Right_Spherical_Points AS VARCHAR)    
      ELSE CAST(a.Right_Spherical_Points AS VARCHAR) END 'Right Spherical', a.Right_Cyclinderical_Status,        
      CASE WHEN ISNULL(a.Left_Spherical_Points,0) > 0 THEN '+'+ CAST(a.Left_Spherical_Points AS VARCHAR)    
      ELSE CAST(a.Left_Spherical_Points AS VARCHAR) END 'Left Spherical',     
      '', a.Right_Axix_To, a.Left_Spherical_Status, a.Right_Cyclinderical_Points 'Right Cyclinderical',     
      a.Left_Cyclinderical_Status,a.Left_Cyclinderical_Points 'Left Cyclinderical',a.Right_Axix_From AS 'Right Axis' , a.Left_Axix_From AS 'Left Axis',    
      a.Left_Axix_To,ISNULL(IPD,0)IPD, a.UserId, a.EntDate, a.EntOperation, a.EntTerminal, a.EntTerminalIP, a.ApplicationID, a.FormId, a.UserEmpId, a.UserEmpName, a.UserEmpCode     
      ,b.WearGlasses    
      FROM  tblPublicSpacesResident b INNER  JOIN dbo.tblPublicSpacesAutoRefTestResident a ON b.ResidentAutoId=a.ResidentAutoId    
  WHERE  b.ResidentAutoId=@ResidentAutoId    
 END    
ELSE IF @operation='NewResidentForRef'    
BEGIN    
 SELECT DISTINCT top 5 cw.ResidentAutoId Id, cw.ResidentName +' | '+cw.ResidentCode  text,cw.ResidentCode Code     
 FROM tblPublicSpacesResident cw    
 WHERE     
 cw.PublicSpacesAutoId=@PublicSpacesAutoId AND     
 cw.ResidentAutoId NOT IN (SELECT artw.ResidentAutoId FROM tblPublicSpacesAutoRefTestResident artw WITH (NOLOCK) WHERE CAST(artw.AutoRefResidentTransDate AS DATE) = CAST(@AutoRefResidentTransDate AS DATE))    
  AND     
 (cw.ResidentCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR    
  cw.ResidentName LIKE '%'+ISNULL(@SearchText,'')+ '%' )    
 ORDER BY  cw.ResidentName +' | '+cw.ResidentCode     
END    
    
ELSE IF @operation='EditResidentForRef'    
BEGIN    
 SELECT DISTINCT TOP 5 cw.ResidentAutoId Id, cw.ResidentName +' | '+cw.ResidentCode  text,cw.ResidentCode Code     
 FROM tblPublicSpacesResident cw INNER JOIN tblPublicSpacesAutoRefTestResident artw ON cw.ResidentAutoId=artw.ResidentAutoId    
 WHERE     
 cw.PublicSpacesAutoId=@PublicSpacesAutoId    
 AND (cw.ResidentCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR    
  cw.ResidentName LIKE '%'+ISNULL(@SearchText,'')+ '%' )    
 ORDER BY  cw.ResidentName +' | '+cw.ResidentCode     
END    
ELSE IF @operation='GetAutoRefHistoryByResidentId '    
 BEGIN    
      
   SELECT TOP 1 CAST(FORMAT(artw.AutoRefResidentTransDate,'dd | MMM | yyyy') AS VARCHAR) AS Last_Visit_Date,    
   CASE WHEN artw.Right_Spherical_Status ='P' AND artw.Right_Spherical_Points>0 THEN '+' +CAST(Right_Spherical_Points AS VARCHAR)    
   WHEN artw.Right_Spherical_Status='N'AND artw.Right_Spherical_Points>0 THEN '-' +CAST(Right_Spherical_Points AS VARCHAR)    
   WHEN artw.Right_Spherical_Status='E' THEN 'Error '+CAST(ISNULL(Right_Spherical_Points,'') AS VARCHAR)    
   WHEN artw.Right_Spherical_Status='l' THEN 'Plano '+CAST(ISNULL(Right_Spherical_Points,'') AS VARCHAR)    
   ELSE CAST(Right_Spherical_Points AS VARCHAR) END 'Right Spherical',    
    
   CASE WHEN artw.Left_Spherical_Status ='P' AND artw.Left_Spherical_Points>0 THEN '+' +CAST(Left_Spherical_Points AS VARCHAR)    
   WHEN artw.Left_Spherical_Status='N'AND artw.Left_Spherical_Points>0 THEN '-' +CAST(Left_Spherical_Points AS VARCHAR)    
    WHEN artw.Left_Spherical_Status='E' THEN 'Error '+CAST(ISNULL(Left_Spherical_Points,'') AS VARCHAR)    
   WHEN artw.Left_Spherical_Status='l' THEN 'Plano '+CAST(ISNULL(Left_Spherical_Points,'') AS VARCHAR)    
   ELSE CAST(Left_Spherical_Points AS VARCHAR) END 'Left Spherical',    
    
   CASE WHEN artw.Right_Spherical_Status ='P' AND artw.Left_Spherical_Points>0 THEN '+' +CAST(Left_Spherical_Points AS VARCHAR)    
   WHEN artw.Right_Spherical_Status='N'AND artw.Right_Spherical_Points>0 THEN '-' +CAST(Right_Spherical_Points AS VARCHAR)    
   ELSE CAST(Right_Spherical_Points AS VARCHAR) END 'Right Spherical',    
    
   CASE WHEN artw.Right_Cyclinderical_Status ='P' AND artw.Right_Cyclinderical_Points>0 THEN '+' +CAST(Right_Cyclinderical_Points AS VARCHAR)    
   WHEN artw.Right_Cyclinderical_Status='N'AND artw.Right_Cyclinderical_Points>0 THEN '-' +CAST(Right_Cyclinderical_Points AS VARCHAR)    
   ELSE CAST(Right_Cyclinderical_Points AS VARCHAR) END 'Right Cyclinderical',     
    
       
   CASE WHEN artw.Left_Cyclinderical_Status ='P' AND artw.Left_Cyclinderical_Points>0 THEN '+' +CAST(Left_Cyclinderical_Points AS VARCHAR)    
   WHEN artw.Left_Cyclinderical_Status='N'AND artw.Left_Cyclinderical_Points>0 THEN '-' +CAST(Left_Cyclinderical_Points AS VARCHAR)    
   ELSE CAST(artw.Left_Cyclinderical_Points AS VARCHAR) END 'Left Cyclinderical',     
   artw.Right_Axix_From 'Right Axis' ,    
    artw.Left_Axix_From 'Left Axis' ,0 WearGlasses,    
    ISNULL(artw.IPD,0)IPD    
    
  FROM tblPublicSpacesAutoRefTestResident artw    
  WHERE  artw.ResidentAutoId=@ResidentAutoId    
  ORDER BY artw.AutoRefResidentTransDate desc    
 END    
    
 ELSE IF @operation= 'GetDatesofResident'    
 BEGIN    
  SELECT CAST(FORMAT(artw.AutoRefResidentTransDate,'dd | MMM | yyyy') AS VARCHAR) Text,    
  artw.AutoRefResidentId AS Id    
  FROM tblPublicSpacesAutoRefTestResident artw    
  WHERE artw.ResidentAutoId=@ResidentAutoId    
  ORDER BY artw.AutoRefResidentId desc    
 END    
    
     
ELSE IF @operation='GetByAutoRefResidentIdForOpt '    
 BEGIN    
  SELECT TOP 1 a.AutoRefResidentId, a.AutoRefResidentTransId, a.AutoRefResidentTransDate,a. ResidentAutoId, a.Right_Spherical_Status,     
  a.Right_Spherical_Points, a.Right_Cyclinderical_Status, a.Right_Cyclinderical_Points, a.Right_Axix_From, a.Right_Axix_To,    
  a.Left_Spherical_Status, a.Left_Spherical_Points,  a.Left_Cyclinderical_Status, a.Left_Cyclinderical_Points,    
  a.Left_Axix_From, Left_Axix_To, ISNULL(IPD,0)IPD, a.UserId, a.EntDate, a.EntOperation, a.EntTerminal,    
  a.EntTerminalIP, a.ApplicationID, a.FormId, a.UserEmpId, a.UserEmpName, a.UserEmpCode ,ISNULL(cw.WearGlasses,0)WearGlasses    
  FROM   dbo.tblPublicSpacesAutoRefTestResident  a INNER JOIN  tblPublicSpacesResident  cw ON cw.ResidentAutoId=a.ResidentAutoId
  WHERE  a.ResidentAutoId = @ResidentAutoId    
  ORDER BY a.AutoRefResidentId desc    
END    
ELSE IF @operation= 'DeleteAutoRefById'    
 BEGIN    
  DELETE FROM tblPublicSpacesAutoRefTestResident WHERE AutoRefResidentId=@AutoRefResidentId    
  SELECT @AutoRefResidentId AS AutoRefResidentId,'Successfully Deleted' AS RESULT    
 END 
GO


 

CREATE OR ALTER PROC [dbo].[Sp_OptometristPublicSpacesResident](    
    @OptometristPublicSpacesResidentId INT = NULL,    
    @OptometristResidentTransDate datetime = NULL,    
    @ResidentAutoId int = NULL,    
 @AutoRefResidentId INT=NULL,    
    @HasChiefComplain int = NULL,    
    @ChiefComplainRemarks nvarchar(200)= NULL,    
    @HasOccularHistory int= NULL,    
    @OccularHistoryRemarks nvarchar(200)= NULL,    
    @HasMedicalHistory int= NULL,    
    @MedicalHistoryRemarks nvarchar(200)= NULL,    
    @DistanceVision_RightEye_Unaided int= NULL,    
    @DistanceVision_RightEye_WithGlasses int= NULL,    
    @DistanceVision_RightEye_PinHole int= NULL,    
    @NearVision_RightEye int= NULL,    
    @NeedCycloRefraction_RightEye int= NULL,    
    @NeedCycloRefractionRemarks_RightEye nvarchar(200)= NULL,    
    @DistanceVision_LeftEye_Unaided int= NULL,    
    @DistanceVision_LeftEye_WithGlasses int= NULL,    
    @DistanceVision_LeftEye_PinHole int= NULL,    
    @NearVision_LeftEye int= NULL,    
    @NeedCycloRefraction_LeftEye int= NULL,    
    @NeedCycloRefractionRemarks_LeftEye nvarchar(200)= NULL,    
    @Right_Spherical_Status char(1)= NULL,    
    @Right_Spherical_Points decimal(9, 2)= NULL,    
    @Right_Cyclinderical_Status char(1)= NULL,    
    @Right_Cyclinderical_Points decimal(9, 2)= NULL,    
    @Right_Axix_From int= NULL,    
    @Right_Axix_To int= NULL,    
    @Right_Near_Status char(1)= NULL,    
    @Right_Near_Points decimal(9, 2)= NULL,    
    @Left_Spherical_Status char(1)= NULL,    
    @Left_Spherical_Points decimal(9, 2)= NULL,    
    @Left_Cyclinderical_Status char(1)= NULL,    
    @Left_Cyclinderical_Points decimal(9, 2)= NULL,    
    @Left_Axix_From int= NULL,    
    @Left_Axix_To int= NULL,    
    @Left_Near_Status char(1)= NULL,    
    @Left_Near_Points decimal(9, 2)= NULL,    
    @Douchrome int= NULL,    
    @Achromatopsia varchar(20)= NULL,    
    @RetinoScopy_RightEye int= NULL,     
    @Condition_RightEye varchar(200)= NULL,    
    @Meridian1_RightEye varchar(200)= NULL,    
    @Meridian2_RightEye varchar(200)= NULL,    
    @FinalPrescription_RightEye varchar(200)= NULL,    
    @RetinoScopy_LeftEye int= NULL,     
    @Condition_LeftEye varchar(200)= NULL,    
    @Meridian1_LeftEye varchar(200)= NULL,    
    @Meridian2_LeftEye varchar(200)= NULL,    
    @FinalPrescription_LeftEye varchar(200)= NULL,    
    @Hirchberg_Distance int= NULL,    
    @Hirchberg_Near int= NULL,    
    @Ophthalmoscope_RightEye int= NULL,    
    @PupillaryReactions_RightEye int= NULL,    
    @CoverUncovertTest_RightEye int= NULL,    
    @CoverUncovertTestRemarks_RightEye nvarchar(200)= NULL,    
    @ExtraOccularMuscleRemarks_RightEye nvarchar(200)= NULL,    
    @Ophthalmoscope_LeftEye int= NULL,    
    @PupillaryReactions_LeftEye int= NULL,    
    @CoverUncovertTest_LeftEye int= NULL,    
    @CoverUncovertTestRemarks_LeftEye nvarchar(200)= NULL,    
 @CycloplegicRefraction_RightEye BIT =NULL,    
 @CycloplegicRefraction_LeftEye BIT =NULL,    
 @Conjunctivitis_RightEye BIT =NULL,    
 @Conjunctivitis_LeftEye BIT =NULL,    
 @Scleritis_RightEye BIT =NULL,            
 @Scleritis_LeftEye BIT =NULL,    
 @Nystagmus_RightEye BIT =NULL,            
 @Nystagmus_LeftEye BIT =NULL,    
 @CornealDefect_RightEye BIT =NULL,    
 @CornealDefect_LeftEye BIT =NULL,    
 @Cataract_RightEye BIT =NULL,    
 @Cataract_LeftEye BIT =NULL,    
 @Keratoconus_RightEye BIT =NULL,    
 @Keratoconus_LeftEye BIT =NULL,    
 @Ptosis_RightEye BIT =NULL,    
 @Ptosis_LeftEye BIT =NULL,    
 @LowVision_RightEye BIT =NULL,    
 @LowVision_LeftEye BIT =NULL,    
 @Pterygium_RightEye BIT =NULL,    
 @Pterygium_LeftEye BIT =NULL,    
 @ColorBlindness_RightEye BIT =NULL,    
 @ColorBlindness_LeftEye BIT =NULL,    
 @Others_RightEye BIT =NULL,    
 @Others_LeftEye BIT =NULL,    
 @Fundoscopy_RightEye BIT =NULL,    
 @Fundoscopy_LeftEye BIT =NULL,    
 @Surgery_RightEye BIT =NULL,    
 @Surgery_LeftEye BIT =NULL,    
 @CataractSurgery_RightEye BIT =NULL,    
 @CataractSurgery_LeftEye BIT =NULL,    
 @SurgeryPterygium_RightEye BIT =NULL,    
 @SurgeryPterygium_LeftEye BIT =NULL,    
 @SurgeryCornealDefect_RightEye BIT =NULL,    
 @SurgeryCornealDefect_LeftEye BIT =NULL,    
 @SurgeryPtosis_RightEye BIT =NULL,    
 @SurgeryPtosis_LeftEye BIT =NULL,    
 @SurgeryKeratoconus_RightEye BIT =NULL,    
 @SurgeryKeratoconus_LeftEye BIT =NULL,    
 @Chalazion_RightEye BIT =NULL,    
 @Chalazion_LeftEye BIT =NULL,    
 @Hordeolum_RightEye BIT =NULL,    
 @Hordeolum_LeftEye BIT =NULL,    
 @SurgeryOthers_RightEye BIT =NULL,    
 @SurgeryOthers_LeftEye BIT =NULL,    
 @RightPupilDefects BIT =NULL,    
 @LeftPupilDefects BIT =NULL,    
 @RightSquint_Surgery BIT =NULL,    
 @LeftSquint_Surgery BIT =NULL,    
    
 @PublicSpacesAutoId INT=NULL,    
    @UserId nvarchar(250)= NULL,    
    @EntDate datetime= NULL,    
    @EntOperation nvarchar(100)= NULL,    
    @EntryTerminal nvarchar(200)= NULL,    
    @EntryTerminalIP nvarchar(200)= NULL,    
    @ExtraOccularMuscleRemarks_LeftEye nvarchar(200)= NULL,    
    @ApplicationID nvarchar(20)= NULL,    
    @FormId nvarchar(250)= NULL,    
    @UserEmpId int= NULL,    
    @UserEmpName nvarchar(250)= NULL,    
    @UserEmpCode nvarchar(10)= NULL,    
    @VisualAcuity_RightEye int= NULL,    
    @VisualAcuity_LeftEye int= NULL,    
 @LeftSquint_VA BIT= NULL,    
 @RightSquint_VA BIT= NULL,    
 @LeftAmblyopic_VA BIT= NULL,    
 @RightAmblyopic_VA BIT= NULL,    
 @LeftAmblyopia BIT=NULL,    
 @RightAmblyopia BIT=NULL,    
 @Operation VARCHAR(50)=NULL,    
 @SearchText VARCHAR(1000)=null    
 )    
AS     
    IF @operation = 'Save'    
 BEGIN    
  SELECT @PublicSpacesAutoId=PublicSpacesAutoId FROM tblPublicSpacesResident  cw WHERE cw.ResidentAutoId=@ResidentAutoId    
  INSERT INTO dbo.tblPublicSpacesOptometristResident (OptometristResidentTransDate,AutoRefResidentId , ResidentAutoId,PublicSpacesAutoId, HasChiefComplain, ChiefComplainRemarks, HasOccularHistory,    
             OccularHistoryRemarks, HasMedicalHistory, MedicalHistoryRemarks, DistanceVision_RightEye_Unaided,    
             DistanceVision_RightEye_WithGlasses, DistanceVision_RightEye_PinHole, NearVision_RightEye,    
             NeedCycloRefraction_RightEye, NeedCycloRefractionRemarks_RightEye, DistanceVision_LeftEye_Unaided,    
             DistanceVision_LeftEye_WithGlasses, DistanceVision_LeftEye_PinHole, NearVision_LeftEye,    
             NeedCycloRefraction_LeftEye, NeedCycloRefractionRemarks_LeftEye, Right_Spherical_Status,     
             Right_Spherical_Points, Right_Cyclinderical_Status, Right_Cyclinderical_Points, Right_Axix_From, Right_Axix_To,    
             Right_Near_Status, Right_Near_Points, Left_Spherical_Status, Left_Spherical_Points,     
             Left_Cyclinderical_Status, Left_Cyclinderical_Points, Left_Axix_From, Left_Axix_To, Left_Near_Status, Left_Near_Points,     
             Douchrome, Achromatopsia, RetinoScopy_RightEye,  Condition_RightEye, Meridian1_RightEye,     
             Meridian2_RightEye, FinalPrescription_RightEye, RetinoScopy_LeftEye,  Condition_LeftEye,    
             Meridian1_LeftEye, Meridian2_LeftEye, FinalPrescription_LeftEye, Hirchberg_Distance, Hirchberg_Near, Ophthalmoscope_RightEye,     
             PupillaryReactions_RightEye, CoverUncovertTest_RightEye, CoverUncovertTestRemarks_RightEye, ExtraOccularMuscleRemarks_RightEye,    
             Ophthalmoscope_LeftEye, PupillaryReactions_LeftEye, CoverUncovertTest_LeftEye, CoverUncovertTestRemarks_LeftEye,    
             CycloplegicRefraction_RightEye , CycloplegicRefraction_LeftEye , Conjunctivitis_RightEye , Conjunctivitis_LeftEye ,    
                            Scleritis_RightEye , Scleritis_LeftEye , Nystagmus_RightEye , Nystagmus_LeftEye , CornealDefect_RightEye ,CornealDefect_LeftEye , Cataract_RightEye ,    
             Cataract_LeftEye , Keratoconus_RightEye , Keratoconus_LeftEye , Ptosis_RightEye , Ptosis_LeftEye , LowVision_RightEye , LowVision_LeftEye ,    
             Pterygium_RightEye , Pterygium_LeftEye , ColorBlindness_RightEye , ColorBlindness_LeftEye , Others_RightEye , Others_LeftEye , Fundoscopy_RightEye ,    
             Fundoscopy_LeftEye , Surgery_RightEye , Surgery_LeftEye , CataractSurgery_RightEye , CataractSurgery_LeftEye , SurgeryPterygium_RightEye ,    
             SurgeryPterygium_LeftEye , SurgeryCornealDefect_RightEye , SurgeryCornealDefect_LeftEye , SurgeryPtosis_RightEye , SurgeryPtosis_LeftEye ,    
             SurgeryKeratoconus_RightEye , SurgeryKeratoconus_LeftEye , Chalazion_RightEye , Chalazion_LeftEye , Hordeolum_RightEye , Hordeolum_LeftEye ,    
             SurgeryOthers_RightEye , SurgeryOthers_LeftEye,RightPupilDefects,LeftPupilDefects,UserId, EntDate,    
             EntOperation, EntTerminal, EntTerminalIP, ExtraOccularMuscleRemarks_LeftEye, ApplicationID, FormId, UserEmpId, UserEmpName, UserEmpCode,    
             VisualAcuity_RightEye, VisualAcuity_LeftEye, LeftSquint_VA ,RightSquint_VA,LeftAmblyopic_VA  ,RightAmblyopic_VA ,    
             RightAmblyopia,LeftAmblyopia,LeftSquint_Surgery,RightSquint_Surgery    
             )    
    
  SELECT  @OptometristResidentTransDate,@AutoRefResidentId , @ResidentAutoId, @PublicSpacesAutoId,@HasChiefComplain, @ChiefComplainRemarks,     
      @HasOccularHistory, @OccularHistoryRemarks, @HasMedicalHistory, @MedicalHistoryRemarks, @DistanceVision_RightEye_Unaided,     
      @DistanceVision_RightEye_WithGlasses, @DistanceVision_RightEye_PinHole, @NearVision_RightEye,     
      @NeedCycloRefraction_RightEye, @NeedCycloRefractionRemarks_RightEye, @DistanceVision_LeftEye_Unaided,     
      @DistanceVision_LeftEye_WithGlasses, @DistanceVision_LeftEye_PinHole, @NearVision_LeftEye,     
      @NeedCycloRefraction_LeftEye, @NeedCycloRefractionRemarks_LeftEye, @Right_Spherical_Status,     
      @Right_Spherical_Points, @Right_Cyclinderical_Status, @Right_Cyclinderical_Points, @Right_Axix_From,     
      @Right_Axix_To, @Right_Near_Status, @Right_Near_Points, @Left_Spherical_Status, @Left_Spherical_Points,     
      @Left_Cyclinderical_Status, @Left_Cyclinderical_Points, @Left_Axix_From, @Left_Axix_To, @Left_Near_Status,     
      @Left_Near_Points, @Douchrome, @Achromatopsia, @RetinoScopy_RightEye,      
      @Condition_RightEye, @Meridian1_RightEye, @Meridian2_RightEye, @FinalPrescription_RightEye,     
      @RetinoScopy_LeftEye, @Condition_LeftEye, @Meridian1_LeftEye,     
      @Meridian2_LeftEye, @FinalPrescription_LeftEye, @Hirchberg_Distance, @Hirchberg_Near, @Ophthalmoscope_RightEye,     
      @PupillaryReactions_RightEye, @CoverUncovertTest_RightEye, @CoverUncovertTestRemarks_RightEye,     
      @ExtraOccularMuscleRemarks_RightEye, @Ophthalmoscope_LeftEye, @PupillaryReactions_LeftEye,     
      @CoverUncovertTest_LeftEye, @CoverUncovertTestRemarks_LeftEye,     
      @CycloplegicRefraction_RightEye , @CycloplegicRefraction_LeftEye , @Conjunctivitis_RightEye , @Conjunctivitis_LeftEye ,    
    @Scleritis_RightEye ,@Scleritis_LeftEye , @Nystagmus_RightEye , @Nystagmus_LeftEye , @CornealDefect_RightEye , @CornealDefect_LeftEye ,    
    @Cataract_RightEye , @Cataract_LeftEye , @Keratoconus_RightEye , @Keratoconus_LeftEye , @Ptosis_RightEye , @Ptosis_LeftEye ,    
    @LowVision_RightEye , @LowVision_LeftEye , @Pterygium_RightEye , @Pterygium_LeftEye , @ColorBlindness_RightEye , @ColorBlindness_LeftEye ,    
    @Others_RightEye , @Others_LeftEye , @Fundoscopy_RightEye , @Fundoscopy_LeftEye , @Surgery_RightEye , @Surgery_LeftEye ,    
    @CataractSurgery_RightEye , @CataractSurgery_LeftEye , @SurgeryPterygium_RightEye , @SurgeryPterygium_LeftEye , @SurgeryCornealDefect_RightEye ,    
    @SurgeryCornealDefect_LeftEye , @SurgeryPtosis_RightEye , @SurgeryPtosis_LeftEye , @SurgeryKeratoconus_RightEye , @SurgeryKeratoconus_LeftEye ,    
    @Chalazion_RightEye , @Chalazion_LeftEye , @Hordeolum_RightEye , @Hordeolum_LeftEye , @SurgeryOthers_RightEye , @SurgeryOthers_LeftEye ,    
      @RightPupilDefects,@LeftPupilDefects,@UserId, GETDATE(), 'INSERT',     
      @EntryTerminal, @EntryTerminalIP, @ExtraOccularMuscleRemarks_LeftEye, @ApplicationID, @FormId,     
      @UserEmpId, @UserEmpName, @UserEmpCode, @VisualAcuity_RightEye, @VisualAcuity_LeftEye, @LeftSquint_VA ,@RightSquint_VA,@LeftAmblyopic_VA      
      ,@RightAmblyopic_VA ,@RightAmblyopia,@LeftAmblyopia,@LeftSquint_Surgery,@RightSquint_Surgery    
    
      SET @OptometristPublicSpacesResidentId=SCOPE_IDENTITY()    
    
  SELECT @OptometristPublicSpacesResidentId AS OptometristPublicSpacesResidentId,'Successfully Saved' AS RESULT    
 END    
 ELSE IF @operation='Update'    
 BEGIN    
 UPDATE dbo.tblPublicSpacesOptometristResident    
  SET    HasChiefComplain = @HasChiefComplain, ChiefComplainRemarks = @ChiefComplainRemarks, HasOccularHistory = @HasOccularHistory,     
           PublicSpacesAutoId=@PublicSpacesAutoId,OccularHistoryRemarks = @OccularHistoryRemarks, HasMedicalHistory = @HasMedicalHistory, MedicalHistoryRemarks = @MedicalHistoryRemarks,     
           DistanceVision_RightEye_Unaided = @DistanceVision_RightEye_Unaided, DistanceVision_RightEye_WithGlasses = @DistanceVision_RightEye_WithGlasses,     
           DistanceVision_RightEye_PinHole = @DistanceVision_RightEye_PinHole, NearVision_RightEye = @NearVision_RightEye,     
           NeedCycloRefraction_RightEye = @NeedCycloRefraction_RightEye, NeedCycloRefractionRemarks_RightEye = @NeedCycloRefractionRemarks_RightEye,     
           DistanceVision_LeftEye_Unaided = @DistanceVision_LeftEye_Unaided, DistanceVision_LeftEye_WithGlasses = @DistanceVision_LeftEye_WithGlasses,     
           DistanceVision_LeftEye_PinHole = @DistanceVision_LeftEye_PinHole, NearVision_LeftEye = @NearVision_LeftEye,     
           NeedCycloRefraction_LeftEye = @NeedCycloRefraction_LeftEye, NeedCycloRefractionRemarks_LeftEye = @NeedCycloRefractionRemarks_LeftEye,     
           Right_Spherical_Status = @Right_Spherical_Status, Right_Spherical_Points = @Right_Spherical_Points,     
           Right_Cyclinderical_Status = @Right_Cyclinderical_Status, Right_Cyclinderical_Points = @Right_Cyclinderical_Points,     
           Right_Axix_From = @Right_Axix_From, Right_Axix_To = @Right_Axix_To, Right_Near_Status = @Right_Near_Status,     
           Right_Near_Points = @Right_Near_Points, Left_Spherical_Status = @Left_Spherical_Status, Left_Spherical_Points = @Left_Spherical_Points,     
           Left_Cyclinderical_Status = @Left_Cyclinderical_Status, Left_Cyclinderical_Points = @Left_Cyclinderical_Points,     
           Left_Axix_From = @Left_Axix_From, Left_Axix_To = @Left_Axix_To, Left_Near_Status = @Left_Near_Status,     
           Left_Near_Points = @Left_Near_Points, Douchrome = @Douchrome, Achromatopsia = @Achromatopsia,     
           RetinoScopy_RightEye = @RetinoScopy_RightEye, CycloplegicRefraction_RightEye = @CycloplegicRefraction_RightEye,     
           Condition_RightEye = @Condition_RightEye, Meridian1_RightEye = @Meridian1_RightEye, Meridian2_RightEye = @Meridian2_RightEye,     
           FinalPrescription_RightEye = @FinalPrescription_RightEye, RetinoScopy_LeftEye = @RetinoScopy_LeftEye,     
            Condition_LeftEye = @Condition_LeftEye,     
           Meridian1_LeftEye = @Meridian1_LeftEye, Meridian2_LeftEye = @Meridian2_LeftEye, FinalPrescription_LeftEye = @FinalPrescription_LeftEye,     
           Hirchberg_Distance = @Hirchberg_Distance, Hirchberg_Near = @Hirchberg_Near, Ophthalmoscope_RightEye = @Ophthalmoscope_RightEye,     
           PupillaryReactions_RightEye = @PupillaryReactions_RightEye, CoverUncovertTest_RightEye = @CoverUncovertTest_RightEye,     
           CoverUncovertTestRemarks_RightEye = @CoverUncovertTestRemarks_RightEye, ExtraOccularMuscleRemarks_RightEye = @ExtraOccularMuscleRemarks_RightEye,     
           Ophthalmoscope_LeftEye = @Ophthalmoscope_LeftEye, PupillaryReactions_LeftEye = @PupillaryReactions_LeftEye,     
           CoverUncovertTest_LeftEye = @CoverUncovertTest_LeftEye, CoverUncovertTestRemarks_LeftEye = @CoverUncovertTestRemarks_LeftEye,    
      CycloplegicRefraction_LeftEye = @CycloplegicRefraction_LeftEye      
      ,Conjunctivitis_RightEye =  @Conjunctivitis_RightEye    
      ,Conjunctivitis_LeftEye =  @Conjunctivitis_LeftEye    
      ,Scleritis_RightEye =   @Scleritis_RightEye    
      ,Scleritis_LeftEye =   @Scleritis_LeftEye    
      ,Nystagmus_RightEye =   @Nystagmus_RightEye    
      ,Nystagmus_LeftEye =   @Nystagmus_LeftEye    
      ,CornealDefect_RightEye =  @CornealDefect_RightEye    
      ,CornealDefect_LeftEye =  @CornealDefect_LeftEye    
      ,Cataract_RightEye =   @Cataract_RightEye    
      ,Cataract_LeftEye =    @Cataract_LeftEye    
      ,Keratoconus_RightEye =   @Keratoconus_RightEye    
      ,Keratoconus_LeftEye =    @Keratoconus_LeftEye    
      ,Ptosis_RightEye =    @Ptosis_RightEye    
      ,Ptosis_LeftEye =    @Ptosis_LeftEye    
      ,LowVision_RightEye =   @LowVision_RightEye    
      ,LowVision_LeftEye =   @LowVision_LeftEye    
      ,Pterygium_RightEye =   @Pterygium_RightEye    
      ,Pterygium_LeftEye =   @Pterygium_LeftEye    
      ,ColorBlindness_RightEye =  @ColorBlindness_RightEye    
      ,ColorBlindness_LeftEye =  @ColorBlindness_LeftEye    
      ,Others_RightEye =    @Others_RightEye    
   ,RightPupilDefects =    @RightPupilDefects,    
   LeftPupilDefects  =    @LeftPupilDefects    
      ,Others_LeftEye =    @Others_LeftEye    
      ,Fundoscopy_RightEye =   @Fundoscopy_RightEye    
      ,Fundoscopy_LeftEye =    @Fundoscopy_LeftEye    
      ,Surgery_RightEye =    @Surgery_RightEye    
      ,Surgery_LeftEye =    @Surgery_LeftEye    
      ,CataractSurgery_RightEye =  @CataractSurgery_RightEye    
      ,CataractSurgery_LeftEye =  @CataractSurgery_LeftEye    
      ,SurgeryPterygium_RightEye = @SurgeryPterygium_RightEye    
      ,SurgeryPterygium_LeftEye =  @SurgeryPterygium_LeftEye    
      ,SurgeryCornealDefect_RightEye =@SurgeryCornealDefect_RightEye    
      ,SurgeryCornealDefect_LeftEye = @SurgeryCornealDefect_LeftEye    
      ,SurgeryPtosis_RightEye =  @SurgeryPtosis_RightEye    
      ,SurgeryPtosis_LeftEye =  @SurgeryPtosis_LeftEye    
      ,SurgeryKeratoconus_RightEye = @SurgeryKeratoconus_RightEye    
      ,SurgeryKeratoconus_LeftEye = @SurgeryKeratoconus_LeftEye    
      ,Chalazion_RightEye =   @Chalazion_RightEye    
      ,Chalazion_LeftEye =   @Chalazion_LeftEye    
      ,Hordeolum_RightEye =   @Hordeolum_RightEye    
      ,Hordeolum_LeftEye =   @Hordeolum_LeftEye    
   ,LeftAmblyopia =    @LeftAmblyopia    
   ,RightAmblyopia =    @RightAmblyopia    
      ,SurgeryOthers_RightEye =  @SurgeryOthers_RightEye    
      ,SurgeryOthers_LeftEye =  @SurgeryOthers_LeftEye,    
   LeftSquint_Surgery =  @LeftSquint_Surgery,    
   RightSquint_Surgery =  @RightSquint_Surgery,    
           UserId = @UserId ,ExtraOccularMuscleRemarks_LeftEye = @ExtraOccularMuscleRemarks_LeftEye,     
           ApplicationID = @ApplicationID, FormId = @FormId, UserEmpId = @UserEmpId, UserEmpName = @UserEmpName,     
           UserEmpCode = @UserEmpCode, VisualAcuity_RightEye = @VisualAcuity_RightEye, VisualAcuity_LeftEye = @VisualAcuity_LeftEye,     
           LeftSquint_VA =@LeftSquint_VA ,RightSquint_VA=@RightSquint_VA,LeftAmblyopic_VA =@LeftAmblyopic_VA,RightAmblyopic_VA =@RightAmblyopic_VA,    
     EntOperation='UPDATE'    
  WHERE  OptometristPublicSpacesResidentId = @OptometristPublicSpacesResidentId     
    
   SELECT @OptometristPublicSpacesResidentId  AS OptometristPublicSpacesResidentId,'Successfully Updated' AS RESULT    
    
 END    
    
 ELSE IF @operation='GetById'    
 BEGIN    
         
SELECT ISNULL(OptometristPublicSpacesResidentId,'0')OptometristPublicSpacesResidentId    
      ,ISNULL(OptometristResidentTransDate,GETDATE())OptometristResidentTransDate    
      ,ISNULL(ResidentAutoId,'0')ResidentAutoId    
      ,ISNULL(PublicSpacesAutoId,'0')PublicSpacesAutoId    
      ,ISNULL(HasChiefComplain,'0')HasChiefComplain    
      ,ISNULL(ChiefComplainRemarks,'')ChiefComplainRemarks    
      ,ISNULL(HasOccularHistory,'0')HasOccularHistory    
      ,ISNULL(OccularHistoryRemarks,'')OccularHistoryRemarks    
      ,ISNULL(HasMedicalHistory,'0')HasMedicalHistory    
      ,ISNULL(MedicalHistoryRemarks,'')MedicalHistoryRemarks    
      ,ISNULL(DistanceVision_RightEye_Unaided,'0')DistanceVision_RightEye_Unaided    
      ,ISNULL(DistanceVision_RightEye_WithGlasses,'0')DistanceVision_RightEye_WithGlasses    
      ,ISNULL(DistanceVision_RightEye_PinHole,'0')DistanceVision_RightEye_PinHole    
      ,ISNULL(NearVision_RightEye,'0')NearVision_RightEye    
      ,ISNULL(NeedCycloRefraction_RightEye,'0')NeedCycloRefraction_RightEye    
      ,ISNULL(NeedCycloRefractionRemarks_RightEye,'')NeedCycloRefractionRemarks_RightEye    
      ,ISNULL(DistanceVision_LeftEye_Unaided,'0')DistanceVision_LeftEye_Unaided    
      ,ISNULL(DistanceVision_LeftEye_WithGlasses,'0')DistanceVision_LeftEye_WithGlasses    
      ,ISNULL(DistanceVision_LeftEye_PinHole,'0')DistanceVision_LeftEye_PinHole    
      ,ISNULL(NearVision_LeftEye,'0')NearVision_LeftEye    
      ,ISNULL(NeedCycloRefraction_LeftEye,'0')NeedCycloRefraction_LeftEye    
      ,ISNULL(NeedCycloRefractionRemarks_LeftEye,'')NeedCycloRefractionRemarks_LeftEye    
      ,ISNULL(Right_Spherical_Status,'0')Right_Spherical_Status    
      ,ISNULL(Right_Spherical_Points,'0')Right_Spherical_Points    
      ,ISNULL(Right_Cyclinderical_Status,'0')Right_Cyclinderical_Status    
      ,ISNULL(Right_Cyclinderical_Points,'0')Right_Cyclinderical_Points    
      ,ISNULL(Right_Axix_From,'0')Right_Axix_From    
      ,ISNULL(Right_Axix_To,'0')Right_Axix_To    
      ,ISNULL(Right_Near_Status,'P')Right_Near_Status    
      ,ISNULL(Right_Near_Points,'0')Right_Near_Points    
      ,ISNULL(Left_Spherical_Status,'P')Left_Spherical_Status    
      ,ISNULL(Left_Spherical_Points,'0')Left_Spherical_Points    
      ,ISNULL(Left_Cyclinderical_Status,'P')Left_Cyclinderical_Status    
      ,ISNULL(Left_Cyclinderical_Points,'0')Left_Cyclinderical_Points    
      ,ISNULL(Left_Axix_From,'0')Left_Axix_From    
      ,ISNULL(Left_Axix_To,'0')Left_Axix_To    
      ,ISNULL(Left_Near_Status,'')Left_Near_Status    
      ,ISNULL(Left_Near_Points,'0')Left_Near_Points    
      ,ISNULL(VisualAcuity_RightEye,'0')VisualAcuity_RightEye    
      ,ISNULL(VisualAcuity_LeftEye,'0')VisualAcuity_LeftEye    
      ,ISNULL(LeftSquint_VA,'0')LeftSquint_VA    
      ,ISNULL(RightSquint_VA,'0')RightSquint_VA    
      ,ISNULL(LeftAmblyopic_VA,'0')LeftAmblyopic_VA    
      ,ISNULL(RightAmblyopic_VA,'0')RightAmblyopic_VA    
      ,ISNULL(AutoRefResidentId,'0')AutoRefResidentId    
      ,ISNULL(Hirchberg_Distance,'0')Hirchberg_Distance    
      ,ISNULL(Hirchberg_Near,'0')Hirchberg_Near    
      ,ISNULL(Ophthalmoscope_RightEye,'0')Ophthalmoscope_RightEye    
      ,ISNULL(Ophthalmoscope_LeftEye,'0')Ophthalmoscope_LeftEye    
      ,ISNULL(PupillaryReactions_RightEye,'0')PupillaryReactions_RightEye    
      ,ISNULL(CoverUncovertTest_RightEye,'0')CoverUncovertTest_RightEye    
      ,ISNULL(CoverUncovertTestRemarks_RightEye,'')CoverUncovertTestRemarks_RightEye    
      ,ISNULL(ExtraOccularMuscleRemarks_RightEye,'')ExtraOccularMuscleRemarks_RightEye    
      ,ISNULL(PupillaryReactions_LeftEye,'0')PupillaryReactions_LeftEye    
      ,ISNULL(CoverUncovertTest_LeftEye,'0')CoverUncovertTest_LeftEye    
      ,ISNULL(CoverUncovertTestRemarks_LeftEye,'0')CoverUncovertTestRemarks_LeftEye    
      ,ISNULL(CycloplegicRefraction_RightEye,'0')CycloplegicRefraction_RightEye    
      ,ISNULL(CycloplegicRefraction_LeftEye,'0')CycloplegicRefraction_LeftEye    
      ,ISNULL(Conjunctivitis_RightEye,'0')Conjunctivitis_RightEye    
      ,ISNULL(Conjunctivitis_LeftEye,'0')Conjunctivitis_LeftEye    
      ,ISNULL(Scleritis_RightEye,'0')Scleritis_RightEye    
      ,ISNULL(Scleritis_LeftEye,'0')Scleritis_LeftEye    
      ,ISNULL(Nystagmus_RightEye,'0')Nystagmus_RightEye    
      ,ISNULL(Nystagmus_LeftEye,'0')Nystagmus_LeftEye    
      ,ISNULL(CornealDefect_RightEye,'0')CornealDefect_RightEye    
      ,ISNULL(CornealDefect_LeftEye,'0')CornealDefect_LeftEye    
      ,ISNULL(Cataract_RightEye,'0')Cataract_RightEye    
      ,ISNULL(Cataract_LeftEye,'0')Cataract_LeftEye    
      ,ISNULL(Keratoconus_RightEye,'0')Keratoconus_RightEye    
      ,ISNULL(Keratoconus_LeftEye,'0')Keratoconus_LeftEye    
      ,ISNULL(Ptosis_RightEye,'0')Ptosis_RightEye    
      ,ISNULL(Ptosis_LeftEye,'0')Ptosis_LeftEye    
      ,ISNULL(LowVision_RightEye,'0')LowVision_RightEye    
      ,ISNULL(LowVision_LeftEye,'0')LowVision_LeftEye    
      ,ISNULL(Pterygium_RightEye,'0')Pterygium_RightEye    
      ,ISNULL(Pterygium_LeftEye,'0')Pterygium_LeftEye    
      ,ISNULL(ColorBlindness_RightEye,'0')ColorBlindness_RightEye    
      ,ISNULL(ColorBlindness_LeftEye,'0')ColorBlindness_LeftEye    
      ,ISNULL(Others_RightEye,'0')Others_RightEye    
      ,ISNULL(Others_LeftEye,'0')Others_LeftEye    
      ,ISNULL(Fundoscopy_RightEye,'0')Fundoscopy_RightEye    
      ,ISNULL(Fundoscopy_LeftEye,'0')Fundoscopy_LeftEye    
      ,ISNULL(Surgery_RightEye,'0')Surgery_RightEye    
      ,ISNULL(Surgery_LeftEye,'0')Surgery_LeftEye    
      ,ISNULL(CataractSurgery_RightEye,'0')CataractSurgery_RightEye    
      ,ISNULL(CataractSurgery_LeftEye,'0')CataractSurgery_LeftEye    
      ,ISNULL(SurgeryPterygium_RightEye,'0')SurgeryPterygium_RightEye    
      ,ISNULL(SurgeryPterygium_LeftEye,'0')SurgeryPterygium_LeftEye    
      ,ISNULL(SurgeryCornealDefect_RightEye,'0')SurgeryCornealDefect_RightEye    
      ,ISNULL(SurgeryCornealDefect_LeftEye,'0')SurgeryCornealDefect_LeftEye    
      ,ISNULL(SurgeryPtosis_RightEye,'0')SurgeryPtosis_RightEye    
      ,ISNULL(SurgeryPtosis_LeftEye,'0')SurgeryPtosis_LeftEye    
      ,ISNULL(SurgeryKeratoconus_RightEye,'0')SurgeryKeratoconus_RightEye    
      ,ISNULL(SurgeryKeratoconus_LeftEye,'0')SurgeryKeratoconus_LeftEye    
      ,ISNULL(Chalazion_RightEye,'0')Chalazion_RightEye    
      ,ISNULL(Chalazion_LeftEye,'0')Chalazion_LeftEye    
      ,ISNULL(Hordeolum_RightEye,'0')Hordeolum_RightEye    
      ,ISNULL(Hordeolum_LeftEye,'0')Hordeolum_LeftEye    
      ,ISNULL(SurgeryOthers_RightEye,'0')SurgeryOthers_RightEye    
      ,ISNULL(SurgeryOthers_LeftEye,'0')SurgeryOthers_LeftEye    
      ,ISNULL(Douchrome,'0')Douchrome    
      ,ISNULL(Achromatopsia,'0')Achromatopsia    
      ,ISNULL(RetinoScopy_RightEye,'0')RetinoScopy_RightEye    
      ,ISNULL(Condition_RightEye,'0')Condition_RightEye    
      ,ISNULL(Meridian1_RightEye,'')Meridian1_RightEye    
      ,ISNULL(Meridian2_RightEye,'')Meridian2_RightEye    
      ,ISNULL(FinalPrescription_RightEye,'')FinalPrescription_RightEye    
      ,ISNULL(RetinoScopy_LeftEye,'0')RetinoScopy_LeftEye    
      ,ISNULL(Condition_LeftEye,'')Condition_LeftEye    
      ,ISNULL(Meridian1_LeftEye,'')Meridian1_LeftEye    
      ,ISNULL(Meridian2_LeftEye,'')Meridian2_LeftEye    
      ,ISNULL(FinalPrescription_LeftEye,'')FinalPrescription_LeftEye    
   ,ISNULL(RightPupilDefects,0)RightPupilDefects    
   ,ISNULL(LeftPupilDefects,0)LeftPupilDefects    
   ,ISNULL(LeftSquint_Surgery,0)LeftSquint_Surgery    
   ,ISNULL(RightSquint_Surgery,0)RightSquint_Surgery    
      ,ISNULL(UserId,'0')UserId    
      ,ISNULL(EntDate,Getdate())    
      ,ISNULL(EntOperation,'0')    
      ,ISNULL(EntTerminal,'0')    
      ,ISNULL(EntTerminalIP,'0')    
      ,ISNULL(ExtraOccularMuscleRemarks_LeftEye,'')ExtraOccularMuscleRemarks_LeftEye,    
   ISNULL(RightAmblyopia,0)RightAmblyopia,ISNULL(LeftAmblyopia,0)LeftAmblyopia    
      ,ISNULL(ApplicationID,'0')    
      ,ISNULL(FormId,'0')    
      ,ISNULL(UserEmpId,'0')    
      ,ISNULL(UserEmpName,'0')    
      ,ISNULL(UserEmpCode,'0')    
    FROM   dbo.tblPublicSpacesOptometristResident    
    WHERE  OptometristPublicSpacesResidentId = @OptometristPublicSpacesResidentId     
 END    
    
 ELSE IF @operation = 'GetListForPublicSpacesResidentOptometrist'    
 BEGIN    
 SELECT DISTINCT TOP 10 cw.ResidentAutoId,cw.ResidentCode,cw.ResidentName,c.PublicSpacesName,cw.CNIC,cw.MobileNo    
 FROM tblPublicSpaces  c    
 INNER JOIN tblPublicSpacesResident    cw ON c.PublicSpacesAutoId=cw.PublicSpacesAutoId    
 INNER JOIN tblPublicSpacesAutoRefTestResident  artw ON cw.ResidentAutoId=artw.ResidentAutoId    
 ORDER BY cw.ResidentAutoId desc    
 END    
      
  ELSE IF @operation = 'Search'    
 BEGIN    
 SELECT DISTINCT TOP 10 c.ResidentAutoId,c.ResidentCode,c.ResidentName,A.PublicSpacesName,c.CNIC,c.MobileNo    
     FROM tblPublicSpaces A INNER JOIN tblPublicSpacesResident c ON A.PublicSpacesAutoId = c.PublicSpacesAutoId    
     INNER JOIN tblPublicSpacesAutoRefTestResident artw ON c.ResidentAutoId=artw.ResidentAutoId    
   WHERE    
   (    
   CAST(c.ResidentCode AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR    
   CAST(A.PublicSpacesName AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR    
   CAST(c.ResidentName AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR    
   CAST(c.CNIC AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'   OR    
   CAST(c.MobileNo AS VARCHAR) LIKE '%'+ isnull(@SearchText,'%' )+ '%'        
   )    
   ORDER BY c.ResidentAutoId desc    
 END    
    
     
ELSE IF @operation='NewResidentForOpt'    
BEGIN    
 SELECT DISTINCT TOP 5 cw.ResidentAutoId Id,cw.ResidentName+' | '+cw.ResidentCode text,cw.ResidentCode Code FROM tblPublicSpacesResident cw    
 INNER JOIN tblPublicSpacesAutoRefTestResident  artr ON cw.ResidentAutoId = artr.ResidentAutoId  
 WHERE     
 cw.PublicSpacesAutoId=@PublicSpacesAutoId AND     
 cw.ResidentAutoId NOT IN (SELECT artw.ResidentAutoId FROM tblPublicSpacesOptometristResident artw WITH (NOLOCK) WHERE CAST(artw.OptometristResidentTransDate  AS DATE) = CAST(@OptometristResidentTransDate AS DATE))    
 AND  (cw.ResidentName LIKE '%'+ISNULL('','')+ '%' OR    
    cw.ResidentCode LIKE '%'+ISNULL('','')+ '%' )    
 ORDER BY cw.ResidentName+' | '+cw.ResidentCode      
END    
    
ELSE IF @operation='EditResidentForOpt'    
BEGIN    
 SELECT DISTINCT TOP 5 cw.ResidentAutoId Id,cw.ResidentName+' | '+cw.ResidentCode text,cw.ResidentCode Code     
 FROM tblPublicSpacesResident cw INNER JOIN tblPublicSpacesOptometristResident artw ON cw.ResidentAutoId=artw.ResidentAutoId    
 WHERE     
 cw.PublicSpacesAutoId=@PublicSpacesAutoId     
 AND  (cw.ResidentName LIKE '%'+ISNULL('','')+ '%' OR    
    cw.ResidentCode LIKE '%'+ISNULL('','')+ '%' )    
 ORDER BY cw.ResidentName+' | '+cw.ResidentCode    
END    
    
 ELSE IF @operation= 'GetDatesofResident'    
 BEGIN    
  SELECT CAST(FORMAT(artw.OptometristResidentTransDate ,'dd | MMM | yyyy') AS VARCHAR) Text,    
  artw.OptometristPublicSpacesResidentId AS Id    
  FROM tblPublicSpacesOptometristResident artw    
  WHERE artw.ResidentAutoId=@ResidentAutoId    
  ORDER BY artw.OptometristPublicSpacesResidentId desc    
 END    
    
 ELSE IF @operation= 'DeleteOptometristById'    
 BEGIN    
  DELETE FROM tblPublicSpacesOptometristResident WHERE OptometristPublicSpacesResidentId=@OptometristPublicSpacesResidentId    
  SELECT @OptometristPublicSpacesResidentId AS OptometristPublicSpacesResidentId,'Successfully Deleted' AS RESULT    
 END    
    
    
      
GO


 
     
CREATE OR ALTER PROC [dbo].[Sp_PublicSpacesGlassDispenseResident]            
    @PublicSpacesGlassDispenseResidentId INT = NULL,            
  @OptometristPublicSpacesResidentId INT =NULL,        
    @GlassDispenseResidentTransDate DATETIME   = NULL,            
    @ResidentAutoId INT   = NULL,            
    @VisionwithGlasses_RightEye INT= NULL,            
    @VisionwithGlasses_LeftEye int= NULL,            
 @NearVA_RightEye INT=NULL,          
 @NearVA_LeftEye INT=NULL,          
    @ResidentSatisficaion int= NULL,            
    @Unsatisfied int= NULL,            
    @Unsatisfied_Remarks nvarchar(250)= NULL,            
    @Unsatisfied_Reason int= NULL,            
 @PublicSpacesAutoId INT =NULL,          
    @UserId nvarchar(250)= NULL,            
    @EntDate datetime= NULL,            
    @EntOperation nvarchar(100)= NULL,            
    @EntryTerminal  nvarchar(200)= NULL,            
    @EntryTerminalIP nvarchar(200) =NULL,            
 @UserEmpName NVARCHAR(200)=NULL,          
 @SearchText NVARCHAR(200)=NULL,          
 @Operation VARCHAR(50)=NULL            
AS             
 IF @operation='Save'            
BEGIN            
IF NOT EXISTS(SELECT 1 FROM tblPublicSpacesGlassDispenseResident artw WHERE artw.ResidentAutoId=@ResidentAutoId AND CAST(artw.GlassDispenseResidentTransDate AS DATE)=CAST(@GlassDispenseResidentTransDate AS date))            
 BEGIN            
            
INSERT INTO [tblPublicSpacesGlassDispenseResident] (        
           OptometristPublicSpacesResidentId,GlassDispenseResidentTransDate, ResidentAutoId,         
           VisionwithGlasses_RightEye, VisionwithGlasses_LeftEye,NearVA_RightEye, NearVA_LeftEye,        
           ResidentSatisficaion, Unsatisfied, Unsatisfied_Remarks, Unsatisfied_Reason,        
           UserId, EntDate, EntOperation, EntTerminal, EntTerminalIP)        
 SELECT @OptometristPublicSpacesResidentId,@GlassDispenseResidentTransDate, @ResidentAutoId, @VisionwithGlasses_RightEye,             
   @VisionwithGlasses_LeftEye, @NearVA_RightEye,@NearVA_LeftEye,@ResidentSatisficaion,         
   @Unsatisfied, @Unsatisfied_Remarks, @Unsatisfied_Reason,                
   @UserId, GETDATE(), @EntOperation, @EntryTerminal ,             
   @EntryTerminalIP            
            
      SET @PublicSpacesGlassDispenseResidentId = SCOPE_IDENTITY()            
     SELECT @PublicSpacesGlassDispenseResidentId  AS PublicSpacesGlassDispenseResidentId,'Successfully Saved' AS RESULT            
 END            
 ELSE            
    SELECT @PublicSpacesGlassDispenseResidentId  AS PublicSpacesGlassDispenseResidentId,'Resident Glass Dispense Already Exists in selected Date' AS RESULT            
              
END            
ELSE IF @operation='UPDATE'            
BEGIN            
   UPDATE dbo.tblPublicSpacesGlassDispenseResident            
  SET              
      VisionwithGlasses_RightEye = @VisionwithGlasses_RightEye, VisionwithGlasses_LeftEye = @VisionwithGlasses_LeftEye,           
    NearVA_RightEye=@NearVA_RightEye,NearVA_LeftEye=@NearVA_LeftEye,          
      ResidentSatisficaion = @ResidentSatisficaion, Unsatisfied = @Unsatisfied, Unsatisfied_Remarks = @Unsatisfied_Remarks,             
      Unsatisfied_Reason = @Unsatisfied_Reason,        
      UserId = @UserId,  EntOperation = @EntOperation, EntTerminal = @EntryTerminal ,             
      EntTerminalIP = @EntryTerminalIP            
    WHERE  PublicSpacesGlassDispenseResidentId = @PublicSpacesGlassDispenseResidentId            
  SELECT @PublicSpacesGlassDispenseResidentId AS PublicSpacesGlassDispenseResidentId,'Successfully Updated' AS RESULT           
END            
            
ELSE IF @operation='GetById'            
BEGIN            
            
    SELECT a.OptometristPublicSpacesResidentId,        
     PublicSpacesGlassDispenseResidentId,GlassDispenseResidentTransDate, a.ResidentAutoId,         
     VisionwithGlasses_RightEye, VisionwithGlasses_LeftEye,           
   NearVA_RightEye,NearVA_LeftEye,          
           ResidentSatisficaion, Unsatisfied, Unsatisfied_Remarks, Unsatisfied_Reason,        
     a.UserId, a.EntDate, a.EntOperation, a.EntTerminal, a.EntTerminalIP,            
     cw.Distance,cw.Near,cw.WearGlasses            
    FROM   dbo.tblPublicSpacesGlassDispenseResident a WITH (NOLOCK)            
 INNER JOIN tblPublicSpacesResident cw ON a.ResidentAutoId = cw.ResidentAutoId            
    
    WHERE  PublicSpacesGlassDispenseResidentId = @PublicSpacesGlassDispenseResidentId            
END            
            
ELSE IF @operation='DeleteById'            
BEGIN            
 DELETE FROM tblPublicSpacesGlassDispenseResident WHERE PublicSpacesGlassDispenseResidentId=@PublicSpacesGlassDispenseResidentId            
  SELECT @PublicSpacesGlassDispenseResidentId  AS PublicSpacesGlassDispenseResidentId,'Successfully Deleted' AS RESULT            
END             
          
          
ELSE IF @Operation ='GetDatesofGlassDispenseResident'            
BEGIN            
 SELECT CAST(FORMAT(artw.GlassDispenseResidentTransDate,'dd | MMM | yyyy') AS VARCHAR) Text,            
   artw.PublicSpacesGlassDispenseResidentId AS Id            
   FROM tblPublicSpacesGlassDispenseResident artw            
   WHERE artw.ResidentAutoId=@ResidentAutoId            
   ORDER BY artw.PublicSpacesGlassDispenseResidentId desc            
END            
          
          
ELSE IF @operation='NewResidentForGlassDispense'            
BEGIN            
 SELECT DISTINCT top 5 cw.ResidentAutoId Id, cw.ResidentName +' | '+cw.ResidentCode  text,cw.ResidentCode Code 
 FROM tblPublicSpacesResident cw            
 INNER JOIN tblPublicSpacesOptometristResident  ow ON cw.ResidentAutoId = ow.ResidentAutoId  AND ow.OptometristResidentTransDate <= CAST(@GlassDispenseResidentTransDate AS DATE)
 WHERE             
 cw.PublicSpacesAutoId=@PublicSpacesAutoId AND             
 cw.ResidentAutoId NOT IN           
   (SELECT artw.ResidentAutoId FROM tblPublicSpacesGlassDispenseResident  artw WITH (NOLOCK) WHERE CAST(artw.GlassDispenseResidentTransDate AS DATE) = CAST(@GlassDispenseResidentTransDate AS DATE))            
 AND             
 (cw.ResidentCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR            
 cw.ResidentName LIKE '%'+ISNULL(@SearchText,'')+ '%' )            
 ORDER BY  cw.ResidentName +' | '+cw.ResidentCode             
END            
            
ELSE IF @operation='EditResidentForGlassDispense'            
BEGIN            
 SELECT DISTINCT TOP 5 cw.ResidentAutoId Id, cw.ResidentName +' | '+cw.ResidentCode  text,cw.ResidentCode Code             
 FROM tblPublicSpacesResident cw INNER JOIN tblPublicSpacesGlassDispenseResident artw ON cw.ResidentAutoId=artw.ResidentAutoId            
 WHERE             
 cw.PublicSpacesAutoId=@PublicSpacesAutoId             
 AND (cw.ResidentCode LIKE '%'+ISNULL(@SearchText,'')+ '%' OR            
 cw.ResidentName LIKE '%'+ISNULL(@SearchText,'')+ '%' )            
 ORDER BY  cw.ResidentName +' | '+cw.ResidentCode             
END            
        
ELSE IF @operation='GetGlassDispenseHistoryByResidentId '        
 BEGIN        
  SELECT TOP 1 artw.OptometristPublicSpacesResidentId,CAST(FORMAT(artw.OptometristResidentTransDate,'dd | MMM | yyyy') AS VARCHAR) AS Last_Visit_Date,        
   CASE WHEN artw.Right_Spherical_Status ='P' AND artw.Right_Spherical_Points>0 THEN '+' +CAST(Right_Spherical_Points AS VARCHAR)        
   WHEN artw.Right_Spherical_Status='N'AND artw.Right_Spherical_Points>0 THEN '-' +CAST(Right_Spherical_Points AS VARCHAR)        
   WHEN artw.Right_Spherical_Status='E' THEN 'Error '+CAST(ISNULL(Right_Spherical_Points,'') AS VARCHAR)        
   WHEN artw.Right_Spherical_Status='l' THEN 'Plano '+CAST(ISNULL(Right_Spherical_Points,'') AS VARCHAR)        
   ELSE CAST(Right_Spherical_Points AS VARCHAR) END 'Right Spherical',        
        
   CASE WHEN artw.Left_Spherical_Status ='P' AND artw.Left_Spherical_Points>0 THEN '+' +CAST(Left_Spherical_Points AS VARCHAR)        
   WHEN artw.Left_Spherical_Status='N'AND artw.Left_Spherical_Points>0 THEN '-' +CAST(Left_Spherical_Points AS VARCHAR)        
    WHEN artw.Left_Spherical_Status='E' THEN 'Error '+CAST(ISNULL(Left_Spherical_Points,'') AS VARCHAR)        
   WHEN artw.Left_Spherical_Status='l' THEN 'Plano '+CAST(ISNULL(Left_Spherical_Points,'') AS VARCHAR)        
   ELSE CAST(Left_Spherical_Points AS VARCHAR) END 'Left Spherical',        
        
   CASE WHEN artw.Right_Spherical_Status ='P' AND artw.Left_Spherical_Points>0 THEN '+' +CAST(Left_Spherical_Points AS VARCHAR)        
   WHEN artw.Right_Spherical_Status='N'AND artw.Right_Spherical_Points>0 THEN '-' +CAST(Right_Spherical_Points AS VARCHAR)        
   ELSE CAST(Right_Spherical_Points AS VARCHAR) END 'Right Spherical',        
        
   CASE WHEN artw.Right_Cyclinderical_Status ='P' AND artw.Right_Cyclinderical_Points>0 THEN '+' +CAST(Right_Cyclinderical_Points AS VARCHAR)        
   WHEN artw.Right_Cyclinderical_Status='N'AND artw.Right_Cyclinderical_Points>0 THEN '-' +CAST(Right_Cyclinderical_Points AS VARCHAR)        
   ELSE CAST(Right_Cyclinderical_Points AS VARCHAR) END 'Right Cyclinderical',         
        
           
   CASE WHEN artw.Left_Cyclinderical_Status ='P' AND artw.Left_Cyclinderical_Points>0 THEN '+' +CAST(Left_Cyclinderical_Points AS VARCHAR)        
   WHEN artw.Left_Cyclinderical_Status='N'AND artw.Left_Cyclinderical_Points>0 THEN '-' +CAST(Left_Cyclinderical_Points AS VARCHAR)        
   ELSE CAST(artw.Left_Cyclinderical_Points AS VARCHAR) END 'Left Cyclinderical',         
   artw.Right_Axix_From 'Right Axis' ,        
    artw.Left_Axix_From 'Left Axis' ,ISNULL(cw.WearGlasses,0)WearGlasses,ISNULL(cw.Near,0)Near,        
    ISNULL(cw.Distance,0) Distance,  ISNULL(artw.IPD,0)IPD,CASE WHEN cw.GenderAutoId=1 THEN 'Male' ELSE 'Female' END Gender,        
    cw.Age        
        
  FROM tblPublicSpacesOptometristResident    artw        
  INNER JOIN tblPublicSpacesResident   cw ON artw.ResidentAutoId = cw.ResidentAutoId        
  WHERE  artw.ResidentAutoId=@ResidentAutoId AND        
  artw.OptometristResidentTransDate <= CAST(@GlassDispenseResidentTransDate AS DATE)         
  ORDER BY artw.OptometristPublicSpacesResidentId desc        
 END        
            
    /*            
    -- Begin Return row code block            
            
    SELECT GlassDispenseResidentTransDate, ResidentAutoId, VisionwithGlasses_RightEye, VisionwithGlasses_LeftEye,             
           ResidentSatisficaion, Unsatisfied, Unsatisfied_Remarks, Unsatisfied_Reason, Right_Spherical_Status,             
           Right_Spherical_Points, Right_Cyclinderical_Status, Right_Cyclinderical_Points, Right_Axix_From,             
           Right_Axix_To, Right_Near_Status, Right_Near_Points, Left_Spherical_Status, Left_Spherical_Points,             
           Left_Cyclinderical_Status, Left_Cyclinderical_Points, Left_Axix_From, Left_Axix_To, Left_Near_Status,             
           Left_Near_Points, FollowupRequired, TreatmentId, Medicines, Prescription, ProvideGlasses,             
           ReferToHospital, UserId, EntDate, EntOperation, EntTerminal, EntTerminalIP            
    FROM   dbo.tblPublicSpacesGlassDispenseResident            
    WHERE  PublicSpacesGlassDispenseResidentId = @PublicSpacesGlassDispenseResidentId            
            
    -- End Return row code block            
            
    */ 
GO




