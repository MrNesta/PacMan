   M S B A M L   `   `   `  ����A  =PacMan, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:PacMan.InfrastructurePacMan.Infrastructure  5   	   3clr-namespace:PacMan.ViewModelPacMan.ViewModel  5   	       PacMan.View.PluginWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2xc#clr-namespace:PacMan.Infrastructure     5   	   0localclr-namespace:PacMan.ViewModel     5   	     � 
ResizeMode$  NoResize=�5       � WindowStartupLocation$ CenterScreen=�5	   	   
������� � Title$ Plugins��5
   	   $	��350��6   $	��300��6&    � Icon$3 -/PacMan;component/Resources/pacmanwindow4.png��5   ?   (   "PacMan.Infrastructure.DialogCloser   DialogResult 6	   �� *DialogResult+.��5      � .+�5   
   �� $��Center=�6   $��0,10,0,0q�61   $
��Wrap=�5      $	��Top=�6(   $��20(�5      
����� �$��Verdana)�64    �� TextAlignment$ Center=�5      �� 5      $6�0Please select the module of the behavior enemies��6   5      �� 5   
   $��0��6    �� SelectedIndex$ 0��5      $��	0,75,0,78q�6%   
���   �$��Center=�5      ��5       �� *EnemyAlgorithms+ �� SelectedItem 5      �� *SelectedAlgorithm+��5      �� 5      .R�5      �� 
����� �$��Verdana)�62   $��16(�5       $
��Bold"�6.   ��5       �� *Name+5      5      �� 5   
   -   5         ��Name$ OkName��5      $��42��6I   $	��200��5       $��Center=�6   |�.  $��Bottom=�5!      $��0,0,0,20q�6,   $��OK��6>   $��Comic Sans MS)�5"      	 � Cursor$	 Arrow��6,   
���   �$��22(�5#      
 �� 	IsDefault
 . $
��Bold"�61   ��5   !   �� *ChoosePluginCommand+��5$      �� 5%       �� 
TargetType" ��6"   .R�5&      � .+�5'      �� 
����� ����	#FFF4FF08 �� RadiusX$ 15��6D    �� RadiusY$ 15��5(   %   $��0��62   5)      �� 5*      $��Center=�6+   $��Center=�5+   !   5,      5-      5/      50      51      