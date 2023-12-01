USE VoiceAssistant_Ershik;
GO
IF NOT EXISTS(
    SELECT *
    FROM sys.symmetric_keys
    WHERE name = '##MS_ServiceMasterKey##')
BEGIN
    CREATE MASTER KEY ENCRYPTION BY 
    PASSWORD = 'MSSQLSerivceMasterKey'
END;

IF NOT EXISTS (SELECT * 
                FROM sys.symmetric_keys 
                WHERE name LIKE '%MS_DatabaseMasterKey%')
BEGIN        
    CREATE MASTER KEY ENCRYPTION BY PASSWORD = '14881488';
END;

IF NOT EXISTS (SELECT * 
                FROM sys.asymmetric_keys 
                WHERE name = 'LoginInformationKey')
BEGIN
    CREATE ASYMMETRIC KEY LoginInformationKey
	WITH ALGORITHM = RSA_2048
	ENCRYPTION BY PASSWORD = '14881488';
    ;
END

select * from Profile
insert into Profile values('0','0','00000')

ALTER TABLE Profile 
ADD Password_E varbinary(MAX) NULL
GO
UPDATE E
SET Password_E = ENCRYPTBYASYMKEY(ASYMKEY_ID('LoginInformationKey'), Password)
FROM Profile AS E;
GO

SELECT * FROM Profile

SELECT 
    *,
    Password_Enc = CONVERT(nvarchar(24), DECRYPTBYASYMKEY(ASYMKEY_ID('LoginInformationKey'), Password_E, N'14881488'))
FROM Profile;
GO

INSERT INTO Profile
VALUES ('00', '0', '0', ENCRYPTBYASYMKEY( ASYMKEY_ID('LoginInformationKey'), N'00000'));  

select * from Profile
SELECT 
    *,
    Password_Enc = CONVERT(nvarchar(24), DECRYPTBYASYMKEY(ASYMKEY_ID('LoginInformationKey'), Password_E, N'14881488'))
FROM Profile;
GO

ALTER TABLE Profile
DROP COLUMN Password;

EXEC sp_rename 'Profile.Password_E', 'Password', 'COLUMN';

SELECT *, Password_Enc = CONVERT(nvarchar(24), DECRYPTBYASYMKEY(ASYMKEY_ID('LoginInformationKey'), Password, N'14881488'))
FROM Profile;
GO

select * from Profile