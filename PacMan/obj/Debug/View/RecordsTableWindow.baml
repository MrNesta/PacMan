   M S B A M L   `   `   `  ����A  =PacMan, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:PacMan.ViewModelPacMan.ViewModel  5   	   $    PacMan.View.RecordsTableWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:PacMan.ViewModel     5   	     � 
ResizeMode$  NoResize=�5       � WindowStartupLocation$ CenterScreen=�5   	   
������� � Title$ Records Table��5	   	   $	��480��6   $	��468��6,    � Icon$3 -/PacMan;component/Resources/pacmanwindow3.png��68   .��5
      � $��0,0,0,0q�6    � ColumnDefinitions 5   
   �� 5      $
��187* �6   �� 5      $
��105* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ Table��5      $��Center=�6"   $	��Top=�5      $
��Auto��6+   $
��Auto��69      AutoGenerateColumns .  $��0,0,0,0q�5      $��2��6$   $��Comic Sans MS)�5      $��16(�6.   
�������
���   �$
��Bold"�5         CanUserResizeColumns .     CanUserResizeRows .  	   
IsReadOnly	 . ��5      �� 
 �� Path$
 Players�   ColumnHeaderStyle 5      �� 5       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5      ��  �� Property � �� �� Value
 �Gc��6;   �� 5       � ��$ Center=�6C   �� 5       � ��
 �   �64   �� 5       � ��$	 0.7q�68   5         RowStyle 5       �� 5!      )  #System.Windows.Controls.DataGridRow"  6   .�5"      ��  � ��
 �����63   �� 5#       �@��TextAlignment$ Center=�6@    �� Triggers 5$      P� 5%       P� Property �@J�IsMouseOver P� Value . . �5&      ��  � ��
 �   �6;   �� 5'       � ��
 ��� �6;   5(      5*         Columns 5,      0  *System.Windows.Controls.DataGridTextColumn  5-      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 6%      Header$	 №��61   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5.   %   �� $
 PlayerId�  5/      $	 100 6%   $
 Name��62    50   %   �� $

 Name�  51      $ 80 6%   $ Score��60    52   %   �� $
 Score�  53      $	 200 6%   $
 Date��61    54   %   �� $

 Date� �� StringFormat$ {0:dd.MM.yy HH:mm:ss}��56      57      58      