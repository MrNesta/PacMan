   M S B A M L   `   `   `  ���� A  =PacMan, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:PacMan.InfrastructurePacMan.Infrastructure  3clr-namespace:PacMan.ViewModelPacMan.ViewModel      PacMan.View.PluginWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     2xc#clr-namespace:PacMan.Infrastructure     0localclr-namespace:PacMan.ViewModel       � 
ResizeMode$  NoResize=� � WindowStartupLocation$ CenterScreen=�
������� � Title$ Plugins��$	��350��$	��300�� � Icon$3 -/PacMan;component/Resources/pacmanwindow4.png��(   "PacMan.Infrastructure.DialogCloser   DialogResult �� *DialogResult+.��� .+��� $��Center=�$��0,10,0,0q�$
��Wrap=�$	��Top=�$��20(�
����� �$��Verdana)� �� TextAlignment$ Center=��� $6�0Please select the module of the behavior enemies���� $��0�� �� SelectedIndex$ 0��$��	0,75,0,78q�
���   �$��Center=����� *EnemyAlgorithms+ �� SelectedItem �� *SelectedAlgorithm+���� .R��� 
����� �$��Verdana)�$��16(�$
��Bold"����� *Name+�� -    ��Name$ OkName��$��42��$	��200��$��Center=�|�.  $��Bottom=�$��0,0,0,20q�$��OK��$��Comic Sans MS)�	 � Cursor$	 Arrow��
���   �$��22(�
 �� 	IsDefault
 . $
��Bold"����� *ChoosePluginCommand+����  �� 
TargetType" ��.R�� .+��� 
����� ����	#FFF4FF08 �� RadiusX$ 15�� �� RadiusY$ 15��$��0���� $��Center=�$��Center=�