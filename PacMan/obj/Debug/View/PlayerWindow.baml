   M S B A M L   `   `   `  ����A  =PacMan, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:PacMan.ViewModelPacMan.ViewModel  5   	   =#clr-namespace:PacMan.InfrastructurePacMan.Infrastructure  5   	       PacMan.View.PlayerWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:PacMan.ViewModel     5   	   2xc#clr-namespace:PacMan.Infrastructure     5   	     � Title$  Registration��5	   	   $	��220��6   $	��300��6+   
������� � WindowStartupLocation$ CenterScreen=�5
   	    � 
ResizeMode$ NoResize=�6.    � Icon$2 ,/PacMan;component/Resources/pacmanwindow.png��5   ?   (   "PacMan.Infrastructure.DialogCloser   DialogResult 6	   �� *DialogResult+.��5      � $	��190��6   $	��Top=�6   .+�5   
   �� -   5         ��Name$
 Info��5      $
��Left=�6"    �� TextAlignment$ Center=�5      $��	61,21,0,0q�6+   $
��Wrap=�5      $	��Top=�6(   $	��173��5      $��Verdana)�6    
�������
����� �$��18(�5   &   $
��Bold"�64   Hi! Please, Enter your name!5      �� 5   
   -   5         ��Name$ NameTextBox��5      $��40��5      $	��Top=�6   $��
10,73,10,0q�66   $��Verdana)�5      $��22(�6'   {�. |�. $��Normal$�5   #    �� TextAlignment$ Center=�5      ��5   '   �� 	 �� Path$	 CurrentPlayer�
 �� Mode$
 TwoWay=�5      �� 5   
   -   5         ��Name$ OkName��5      $��42��6?   $	��200��5      $��Center=�6   $��Bottom=�5      $��0,0,0,10q�6,   $��OK��6>   $��Comic Sans MS)�5       � Cursor$ Arrow��6,   
���   �$��22(�5       �� 	IsDefault . $
��Bold"�61   ��5   !   �� *	AddPlayer+��5      �� 5       �� 
TargetType" ��6"   .R�5       � .+�5!      �� 
����� ����	#FFF4FF08 �� RadiusX$ 15��6D    �� RadiusY$ 15��5"   %   $��0��62   5#      �� 5$      $��Center=�6+   $��Center=�5%   !   5&      5'      5)      5*      5+      