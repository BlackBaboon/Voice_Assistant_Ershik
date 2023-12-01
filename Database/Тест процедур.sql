use VoiceAssistant_Ershik
go
--Вставка профиля
exec Insert_Profile 'Вова','Vova','Vova2004'
exec Insert_Profile 'Олег','',''

--Вставка скрипта
exec Insert_Script 'Олег','Дотка',''
exec Insert_Script 'Вова','Дотка','Скрипт по открытию доты'

--Вставка фразы
exec Insert_Phrase 'Олег','Открой доту вторую',''
exec Insert_Phrase 'Вова','Открой доту вторую','Открывает доту'

--Вставка команды
exec Insert_Command '1','net user'

--Назначения скрипта фразе
exec Bind_Phrase_Script '1','1'


go
--Удаление фразы
exec Delete_Phrase 'Олег','Открой доту вторую'

--Удаление скрипта
exec Delete_Script 'Олег','Дотка'

--Удаление команды
exec Delete_Command 'Олег','Дотка','1'

--Снятие привязки скрипта и фразы
exec UnBind_Phrase_Script 'Олег','Открой доту вторую','Дотка'

--Удаление профиля

select * from Profile
select Profile_name,Login, Password = CONVERT(nvarchar(24), DECRYPTBYASYMKEY(ASYMKEY_ID('LoginInformationKey'), Password, N'14881488')) from Profile;

select * from Phrase
select * from Script
select * from Command_sequence
select * from Script_bind

delete from Script
delete from Profile



exec Insert_Phrase 'Test_profile','5-6',''
exec Insert_Phrase 'Test_profile','7-8',''
exec Insert_Phrase 'Test_profile','Открой доту',''
exec Insert_Phrase 'Test_profile','Открой кс',''
exec Insert_Phrase 'Test_profile','Запишись в вагнер',''
exec Insert_Phrase 'Test_profile','Иди нахуй',''