   M S B A M L   `   `   `  ���� c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  f bGalaSoft.MvvmLight.Platform, Version=5.3.0.19032, Culture=neutral, PublicKeyToken=5f873c45e98af8a1lMclr-namespace:GalaSoft.MvvmLight.Command;assembly=GalaSoft.MvvmLight.PlatformGalaSoft.MvvmLight.Command A =PacMan, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:PacMan.ViewModelPacMan.ViewModel +clr-namespace:PacMan.ModelPacMan.Model =#clr-namespace:PacMan.InfrastructurePacMan.Infrastructure    PacMan.View.GameWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     ]cmdMclr-namespace:GalaSoft.MvvmLight.Command;assembly=GalaSoft.MvvmLight.Platform     0localclr-namespace:PacMan.ViewModel     *modclr-namespace:PacMan.Model     2xc#clr-namespace:PacMan.Infrastructure       � 
ResizeMode$  NoResize=� � WindowStartupLocation$ CenterScreen=� � Title$ Pac-Man��$	��535��$	��730�� � Icon$3 -/PacMan;component/Resources/pacmanwindow2.png��(  "PacMan.Infrastructure.DialogCloser   DialogResult �� *DialogResult+ � 	Resources    BigFrontButtonStyle �� Property �� Value  BlackBigFrontButtonStyle �� BasedOn	 �� 
TargetType
 �� RadiusX �� RadiusY �� Triggers P� Property P� Value �� 
TargetName  TomatoBigFrontButtonStyle  TextBoxStyle  CanvasStyle �� Triggers �� Binding �� Path �� Value �� ElementName��%h  &	        &	 �     7   &	 	    7   &	 O    &	 4    �� .���  � ��$ Comic Sans MS)���  � ��$ 20(���  � ��$
 Bold"���  � |� .  ��  � ��$ 60����  � ��$ 75����  � ��$
 Left=���  � ��$	 Top=��� 8   .���  � ��
 ��� ���  � �� �� "	 ��.R�� .+��� $��	Rectangle��
���   ����	#FFF4FF08$
 15��$ 15��$��0���� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ��� $ 	Rectangle�� � ��
 �iii�P�  � �� . . ��� $ 	Rectangle�� � ��
 ������� 8   .���  � ��
 �   ���  � �� �� "	 ��.R�� .+��� $��
TomatoRect��
���Gc�����	#FFF4FF08$
 15��$ 15��$��0���� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ��� $ 
TomatoRect�� � ��
 �  ��P�  � �� . . ��� $ 
TomatoRect�� � ��
 ������� .���  � ��$ Verdana)���  � ��$
 Bold"���  � ��$ 22(���  � |� .  ��  � {� .  ��  � ��$
 Left=���  � ��$	 Top=���  � ��
 �Gc����  �@��TextWrapping$
 Wrap=��� .���  � ��
 �������  � |� . ��  � ��$	 400����  � ��$	 400�� ��  �� $ EnabledDifficalty�	 False.a���  � ��
 �   ���  � {� . ��  �@+�FocusedElement �� $ 	GameField��.��� -    �Name$ 
LayoutRoot�� � ShowGridLines .  
�������$��0,0,0,0q�.+�o� $��25��$	��Top=�$��Verdana)�.@�m� $
��Game��|�.  .@�m� $��New Game�� m� Command �� *StartNewGameCommand+m�  �� *PauseGameCommand+���� *	PauseText+m� $
��Save�� �� *SaveGameResultsCommand+�� m� $
��Exit�� �� *ExitCommand+m� $��Records Table��|�.   �� *RecordsTableCommand+m� $��Plugins��|�.   �� *PluginsCommand+�� -    ��Name$ InfoTextBlock��#��[ $��Center=�$��252,49,253,0q�$��43��$	��219�����#D82C34 �� TextAlignment$ Center=����� $ InfoText��� -   $ Score��#��[ $��Right=�$��	0,34,28,0q�$��33��$	��153��
����� ��� $�Score �� �� ��� $ CurrentPlayer.Score��� -   $ Lives��#��[ $��Right=�$��
0,132,10,0q�$��48��$	��142��
����� ��� $�Lives �� �� ��� $ CurrentPlayer.Lives��� -   $ 
PlayerName��#��[ $��	10,33,0,0q�$��34��$	��204��$��16(��� $�Player: �� �� ��� $ CurrentPlayer.Name��� -   $ Levels��#��[ $��
10,146,0,0q�$��34��$��74��$��15(��� $�Level �� �� ��� $, &CurrentPlayer.CurrentLevel.LevelNumber��� -    ��Name$ label��$��
Difficulty��#��[ 
���Gc��$��14(�$��2,89,0,0q��� $��	78,89,0,0q�#��[ 
�������$��68��$��27��$��14(� �� SelectedIndex$ 1�� �� DisplayMemberPath$ DifficultyName�� �� SelectedValuePath$ Id�� �� SelectedValue �� $ SelectedDifficulty����� $ LevelsOfDifficulty�{��� *EnabledDifficalty+�� -    ��Name$ Start��$��GO��$��
40,246,0,0q�#��[ ���� *StartNewGameCommand+�� -	   $ Pause��$��
40,359,0,0q�#��[ ���� *	PauseText+���� *PauseGameCommand+�� -
   $
 Save��$
��Save��$��606,246,0,0q�#��[ ���� *SaveGameResultsCommand+�� -   $ Records��$��Records��$��606,359,0,0q�#��[ ���� *RecordsTableCommand+�� 
����� �$��5q�  �� CornerRadius$  6��$	��410��$	��410��$��Center=�$��Bottom=�._��� -   ! ��Name$! 	GameField��#��[ + %PacMan.Infrastructure.CanvasAssistant"   BoundChildren" �� *BoundChildrens+.   (System.Windows.Interactivity.Interaction#   Triggers# /   )System.Windows.Interactivity.EventTrigger  $   	EventName$$ KeyDown��.   (System.Windows.Interactivity.TriggerBase%   Actions.% /  )GalaSoft.MvvmLight.Command.EventToCommand  &   PassEventArgsToCommand& . '   Command' �� ( �� Mode$( OneWay=�$ KeyboardPressCommand�