   M S B A M L   `   `   `  ���� A  =PacMan, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:PacMan.ViewModelPacMan.ViewModel  =#clr-namespace:PacMan.InfrastructurePacMan.Infrastructure      PacMan.View.PlayerWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:PacMan.ViewModel     2xc#clr-namespace:PacMan.Infrastructure       � Title$  Registration��$	��220��$	��300��
������� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=� � Icon$2 ,/PacMan;component/Resources/pacmanwindow.png��(   "PacMan.Infrastructure.DialogCloser   DialogResult �� *DialogResult+.��� $	��190��$	��Top=�.+��� -    ��Name$
 Info��$
��Left=� �� TextAlignment$ Center=�$��	61,21,0,0q�$
��Wrap=�$	��Top=�$	��173��$��Verdana)�
�������
����� �$��18(�$
��Bold"�Hi! Please, Enter your name!�� -    ��Name$ NameTextBox��$��40��$	��Top=�$��
10,73,10,0q�$��Verdana)�$��22(�{�. |�. $��Normal$� �� TextAlignment$ Center=����� 	 �� Path$	 CurrentPlayer�
 �� Mode$
 TwoWay=��� -    ��Name$ OkName��$��42��$	��200��$��Center=�$��Bottom=�$��0,0,0,10q�$��OK��$��Comic Sans MS)� � Cursor$ Arrow��
���   �$��22(� �� 	IsDefault . $
��Bold"����� *	AddPlayer+����  �� 
TargetType" ��.R�� .+��� 
����� ����	#FFF4FF08 �� RadiusX$ 15�� �� RadiusY$ 15��$��0���� $��Center=�$��Center=�