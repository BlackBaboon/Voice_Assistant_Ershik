   M S B A M L   `   `   `  ���� A  =Ershik, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ErshikErshik      Ershik.Command_Editor   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     &localclr-namespace:Ershik       � Title$'  !Редактор скриптов��$	��720��$
��1280��.��� ���	#FF2B0061.+��� -    ��Name$ Add_Command��   RoundedButtonStyle#���   $'��!Сохранить команду��$
��Left=�$��
10,361,0,0q�$	��Top=�$��47��$	��274��$��20(��� -    ��Name$ Current_Command�� �� AcceptsReturn . $
��Left=�$��	10,10,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��708��$	��342��$��20(����	#FF212121
��������� -    ��Name$ Commands��$
��Left=�$��
736,12,0,0q�$	��Top=�$	��466��$��71��
���   ��� -   $ Delete_Command��#���   $#��Удалить команду��$
��Left=�$��416,361,0,0q�$	��Top=�$��47��$	��274��$��20(��� -   $ Up��$�� ��$
��Left=�$��1207,12,0,0q�$	��Top=�$��33��$��42������  �� ImageSource$ /Images/Up_Arrow.png�� j� Stretch$ Uniform=�����  �� 
TargetType" ��.R��� $��Border��#��z��#��z��#��z��._��� #��z��#��z��#��z��	 �� Triggers	 P� 
 P� Property
 �@J�	IsFocused P� Value . �� -   $
 Down��$�� ��$
��Left=�$��1207,50,0,0q�$	��Top=�$��33��$��42������ $ /Images/Down_Arrow.png��$ Uniform=����� " ��.R��� $��Border��#��z��#��z��#��z��._��� #��z��#��z��#��z��	 P� 
 �@J�	IsFocused . �� -   $ Discard_Changes��#���   $��Отмена��$
��Left=�$��1028,102,0,0q�$	��Top=�$��48��$	��158��$��20(��� -   $ Save_Changes_Copy��#���   $+��%Сохранить изменения��$
��Left=�$��736,102,0,0q�$	��Top=�$��48��$	��233��$��20(��� -	   $ LaunchScript��$-��'     Запуск
приложения��#���   $
��Left=�$��
10,495,0,0q�$	��Top=�$	��134��$	��140��$��Center=��� $��20(�
�������$��Шаблоны:��$
��Left=�$��
10,434,0,0q�$	��Top=�$��43��$	��174���� -
   $ CloseScript��$(��"Закрытие
процесса��#���   $
��Left=�$��224,495,0,0q�$	��Top=�$	��134��$	��140��$��Center=�