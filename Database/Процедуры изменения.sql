USE VoiceAssistant_Ershik
go

--Процедура изменения профиля
create proc Change_Profile @profile_name nvarchar(16), @login nvarchar(24), @password nvarchar(24)
as
	select 1
go

--Процедура изменения фразы
create proc Change_Phrase @profile nvarchar(16), @old_phrase Nvarchar(24), @new_phrase Nvarchar(24), @desc nvarchar(200)
as
	update Phrase set Phrase = @new_phrase, Phrase_desc = @desc where Phrase like @old_phrase and @profile like Profile_name
go

--Процедура изменения скрипта
create proc Change_Script @profile nvarchar(16), @old_caption Nvarchar(24), @new_caption Nvarchar(24), @desc nvarchar(200)
as
	update Script set Script_caption = @new_caption, Script_desc = @desc where Script_caption like @old_caption and Profile_name like @profile
go

select * from Phrase
select * from Script


exec Change_Phrase 'Test_Profile', 'Открыл доту - оф пк','Открой доту и выключи ПК',''
exec Change_Script'Test_Profile', 'Выключи ПК','Отключи ПК',''


select * from Phrase
select * from Script

