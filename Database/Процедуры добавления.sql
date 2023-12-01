USE VoiceAssistant_Ershik
go

--Процедура добавления профиля
create proc Insert_Profile @profile_name nvarchar(16), @login Nvarchar(24), @password Nvarchar(24)
as
	INSERT INTO Profile
	VALUES (@profile_name, @login, ENCRYPTBYASYMKEY( ASYMKEY_ID('LoginInformationKey'), @password));
go

--Процедура добавления фразы
create proc Insert_Phrase @profile_name nvarchar(16), @phrase Nvarchar(24), @descryption Nvarchar(24)
as
	insert into Phrase values (@profile_name, @phrase, @descryption)
go

--Процедура добавления скрипта
create proc Insert_Script @profile_name nvarchar(16), @script_caption nvarchar(24), @descryption Nvarchar(24)
as
	insert into Script values (@profile_name, @script_caption, @descryption)
go

--Процедура добавления команд скрипта
alter proc Insert_Command @profile_name nvarchar(16), @script_caption nvarchar(24), @command nvarchar(500)
as
	insert into Command_sequence values 
	(@profile_name, @script_caption,
	(select case when max(Command_sequence_number) is null then 1 else max(Command_sequence_number)+1 end
	from Command_sequence where Profile_name = @profile_name and Script_caption = @script_caption),
	@command)
go

--Процедура привязки скрипта к фразе
alter proc Bind_Phrase_Script @profile_name nvarchar(16), @phrase nvarchar(24), @script nvarchar(24)
as
	insert into Script_bind values 
	(
	@profile_name,	@phrase, @script, 
	(select case when max(Script_sequence_number) is null then 1 else max(Script_sequence_number)+1 end
	from Script_bind where Profile_name = @profile_name and Phrase = @phrase)
	)
go
