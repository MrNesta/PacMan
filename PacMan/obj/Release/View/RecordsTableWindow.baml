   M S B A M L   `   `   `  ���� A  =PacMan, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:PacMan.ViewModelPacMan.ViewModel  $    PacMan.View.RecordsTableWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:PacMan.ViewModel       � 
ResizeMode$  NoResize=� � WindowStartupLocation$ CenterScreen=�
������� � Title$ Records Table��$	��480��$	��468�� � Icon$3 -/PacMan;component/Resources/pacmanwindow3.png��.��� $��0,0,0,0q� � ColumnDefinitions �� $
��187* ��� $
��105* �.+�&   System.Windows.Controls.DataGrid  -     Name$ Table��$��Center=�$	��Top=�$
��Auto��$
��Auto��   AutoGenerateColumns .  $��0,0,0,0q�$��2��$��Comic Sans MS)�$��16(�
�������
���   �$
��Bold"�   CanUserResizeColumns .     CanUserResizeRows .  	   
IsReadOnly	 . ���� 
 �� Path$
 Players�   ColumnHeaderStyle ��  �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  �� Property � �� �� Value
 �Gc����  � ��$ Center=���  � ��
 �   ���  � ��$	 0.7q�   RowStyle �� )  #System.Windows.Controls.DataGridRow"  .���  � ��
 �������  �@��TextAlignment$ Center=� �� Triggers P�  P� Property �@J�IsMouseOver P� Value . . ���  � ��
 �   ���  � ��
 ��� �   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 50    Header$	 №��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� $
 PlayerId�  $	 100 $
 Name�� �� $

 Name�  $ 80 $ Score�� �� $
 Score�  $	 200 $
 Date�� �� $

 Date� �� StringFormat$ {0:dd.MM.yy HH:mm:ss}��