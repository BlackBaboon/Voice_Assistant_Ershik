use VoiceAssistant_Ershik
go
--������� �������
exec Insert_Profile '����','Vova','Vova2004'
exec Insert_Profile '����','',''

--������� �������
exec Insert_Script '����','�����',''
exec Insert_Script '����','�����','������ �� �������� ����'

--������� �����
exec Insert_Phrase '����','������ ���� ������',''
exec Insert_Phrase '����','������ ���� ������','��������� ����'

--������� �������
exec Insert_Command '1','net user'

--���������� ������� �����
exec Bind_Phrase_Script '1','1'


go
--�������� �����
exec Delete_Phrase '����','������ ���� ������'

--�������� �������
exec Delete_Script '����','�����'

--�������� �������
exec Delete_Command '����','�����','1'

--������ �������� ������� � �����
exec UnBind_Phrase_Script '����','������ ���� ������','�����'

--�������� �������

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
exec Insert_Phrase 'Test_profile','������ ����',''
exec Insert_Phrase 'Test_profile','������ ��',''
exec Insert_Phrase 'Test_profile','�������� � ������',''
exec Insert_Phrase 'Test_profile','��� �����',''