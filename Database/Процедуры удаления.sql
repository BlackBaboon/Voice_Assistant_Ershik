USE VoiceAssistant_Ershik
go

--Процедура удаления профиля
create proc Delete_Profile @profile_name nvarchar(16)
as
	Delete from Script where @profile_name = Profile_name
	Delete from Profile where @profile_name = Profile_name
go

--Процедура удаления фразы
create proc Delete_Phrase @profile_name nvarchar(16), @phrase Nvarchar(24)
as
	Delete from Phrase where @profile_name = Profile_name and @phrase = Phrase
go

--Процедура удаления скрипта
create proc Delete_Script @profile_name nvarchar(16), @script_caption nvarchar(24)
as
	Delete from Script where @profile_name = Profile_name and @script_caption = Script_caption
go

--Процедура удаления команд скрипта
create proc Delete_Commands @profile_name nvarchar(16), @script_caption nvarchar(24)
as
	Delete from Command_sequence where 
	Profile_name = @profile_name and 
	Script_caption = @script_caption
go

--Процедура отвязки скрипта к фразе
create proc UnBind_Phrase_Script @profile nvarchar(16), @phrase nvarchar(24),  @script nvarchar(24), @number int
as
	Delete from Script_bind where 
	Profile_name = @profile and Phrase = @phrase
go