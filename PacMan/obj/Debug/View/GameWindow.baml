   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5   	   f bGalaSoft.MvvmLight.Platform, Version=5.3.0.19032, Culture=neutral, PublicKeyToken=5f873c45e98af8a1lMclr-namespace:GalaSoft.MvvmLight.Command;assembly=GalaSoft.MvvmLight.PlatformGalaSoft.MvvmLight.Command 5   	   A =PacMan, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:PacMan.ViewModelPacMan.ViewModel 5   	   +clr-namespace:PacMan.ModelPacMan.Model 5	   	   =#clr-namespace:PacMan.InfrastructurePacMan.Infrastructure 5
   	      PacMan.View.GameWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5   	   ]cmdMclr-namespace:GalaSoft.MvvmLight.Command;assembly=GalaSoft.MvvmLight.Platform     5   	   0localclr-namespace:PacMan.ViewModel     5   	   *modclr-namespace:PacMan.Model     5	   	   2xc#clr-namespace:PacMan.Infrastructure     5
   	     � 
ResizeMode$  NoResize=�5       � WindowStartupLocation$ CenterScreen=�5   	    � Title$ Pac-Man��5   	   $	��535��6   $	��730��6&    � Icon$3 -/PacMan;component/Resources/pacmanwindow2.png��5   ?   (  "PacMan.Infrastructure.DialogCloser   DialogResult 6	   �� *DialogResult+ � 	Resources 5         BigFrontButtonStyle �� Property �� Value  BlackBigFrontButtonStyle �� BasedOn	 �� 
TargetType
 �� RadiusX �� RadiusY �� Triggers P� Property P� Value �� 
TargetName  TomatoBigFrontButtonStyle  TextBoxStyle  CanvasStyle �� Triggers �� Binding �� Path �� Value �� ElementName��%	  &	        &	 H    7   &	 �    7   &	 �    &	 N    �� 5   
   .�5      ��  � ��$ Comic Sans MS)�63   �� 5       � ��$ 20(�61   �� 5       � ��$
 Bold"�63   �� 5       � |� .  62   �� 5       � ��$ 60��6/   �� 5       � ��$ 75��6.   �� 5       � ��$
 Left=�6<   �� 5       � ��$	 Top=�6:   5      �� 5   
   8   .�5      ��  � ��
 ��� �63   �� 5       � �� 5      �� 5      "	 ��6&   .R�5       � .+�5!      �� $��	Rectangle��6(   
���   ����	#FFF4FF08$
 15��5"   ;   $ 15��5#   (   $��0��65   5$      �� 5%      $��Center=�6/   $��Center=�5&   !   5'       5(      P� 5)       �@J�IsMouseOver . . �5*   "   �� $ 	Rectangle��6)    � ��
 �iii�5+   )   5,      P� 5-       � �� . . �5.   "   �� $ 	Rectangle��6)    � ��
 �����5/   )   50      52      54      55      �� 56   
   8   .�58      ��  � ��
 �   �63   �� 59       � �� 5:      �� 5;      "	 ��6&   .R�5<      � .+�5=      �� $��
TomatoRect��6)   
���Gc�����	#FFF4FF08$
 15��5>   <   $ 15��5?   )   $��0��66   5@      �� 5A      $��Center=�6/   $��Center=�5B   !   5C       5D      P� 5E       �@J�IsMouseOver . . �5F   "   �� $ 
TomatoRect��6)    � ��
 �  ��5G   )   5H      P� 5I       � �� . . �5J   "   �� $ 
TomatoRect��6)    � ��
 �����5K   )   5L      5N      5P      5Q      �� 5R   
   .�5S      ��  � ��$ Verdana)�63   �� 5T       � ��$
 Bold"�63   �� 5U       � ��$ 22(�61   �� 5V       � |� .  62   �� 5W       � {� .  62   �� 5X       � ��$
 Left=�6<   �� 5Y       � ��$	 Top=�6:   �� 5Z       � ��
 �Gc��63   �� 5[       �@��TextWrapping$
 Wrap=�65   5\      �� 5]   
   .�5^      ��  � ��
 �����62   �� 5_       � |� . 61   �� 5`       � ��$	 400��6.   �� 5a       � ��$	 400��6-    5b      �� 5c       6   �� $ EnabledDifficalty�	 False6I   .a�5d      ��  � ��
 �   �6:   �� 5e       � {� . 69   �� 5f       �@+�FocusedElement 6D   �� $ 	GameField��5g      5i      5j      .��5k      � -   5         �Name$ 
LayoutRoot��5k       � ShowGridLines .  
�������$��0,0,0,0q�5l      .+�5m   
   o� $��25��6   $	��Top=�6   $��Verdana)�5n      .@�5o      m� $
��Game��6   |�.  .@�5p      m� $��New Game��5q       m� Command 5p      �� *StartNewGameCommand+5q      m� 5r       6   �� *PauseGameCommand+��5s      �� *	PauseText+m� 5t      $
��Save��5u       5t      �� *SaveGameResultsCommand+5u      �� 5v      m� 5w      $
��Exit��6    6)   �� *ExitCommand+5x      m� 5y      $��Records Table��6   |�.   5z      �� *RecordsTableCommand+m� 5{      $��Plugins��6   |�.   5|      �� *PluginsCommand+5}      �� 5~   
   -   5         ��Name$ InfoTextBlock��5~      #��[ 6+   $��Center=�5      $��252,49,253,0q�61   $��43��5�      $	��219��6    ���#D82C34 �� TextAlignment$ Center=�5�      ��6+   �� $ InfoText��� 5�   
   -   5        $ Score��5�      #��[ 6#   $��Right=�5�      $��	0,34,28,0q�60   $��33��5�      $	��153��6    
����� ��� 5�      $�Score ��6    �� 5�      �6   �� $ CurrentPlayer.Score�5�      �� 5�   
   -   5        $ Lives��5�      #��[ 6#   $��Right=�5�      $��
0,132,10,0q�60   $��48��5�      $	��142��6    
����� ��� 5�      $�Lives ��6    �� 5�      �6   �� $ CurrentPlayer.Lives�5�      �� 5�   
   -   5        $ 
PlayerName��5�      #��[ 6(   $��	10,33,0,0q�5�      $��34��6'   $	��204��63   $��16(�6?   �� 5�      $�Player: ��6    �� 5�      �6   �� $ CurrentPlayer.Name�5�      �� 5�   
   -   5        $ Levels��5�      #��[ 6%   $��
10,146,0,0q�5�      $��34��6(   $��74��64   $��15(�6?   �� 5�      $�Level ��6    �� 5�      �6   �� $, &CurrentPlayer.CurrentLevel.LevelNumber�5�      �� 5�   
   -   5         ��Name$ label��5�      $��
Difficulty��6   #��[ 64   
���Gc��$��14(�5�   %   $��2,89,0,0q�63   �� 5�   
   $��	78,89,0,0q�6   #��[ 6'   
�������$��68��5�   )   $��27��64   $��14(�6@    �� SelectedIndex$ 1��5�       �� DisplayMemberPath$ DifficultyName��5�       �� SelectedValuePath$ Id��66    �� SelectedValue 5�   %   �� $ SelectedDifficulty���5�      �� $ LevelsOfDifficulty�{�5�      �� *EnabledDifficalty+5�      �� 5�   
   -   5         ��Name$ Start��5�      $��GO��6    $��
40,246,0,0q�6-   #��[ 5�      ��5�      �� *StartNewGameCommand+5�      �� 5�   
   -	   5        $ Pause��5�      $��
40,359,0,0q�6    #��[ 5�      ��5�   4   �� *	PauseText+��5�      �� *PauseGameCommand+5�      �� 5�   
   -
   5        $
 Save��5�      $
��Save��6   $��606,246,0,0q�6.   #��[ 5�      ��5�      �� *SaveGameResultsCommand+5�      �� 5�   
   -   5        $ Records��5�      $��Records��6"   $��606,359,0,0q�64   #��[ 5�      ��5�      �� *RecordsTableCommand+5�      �� 5�   
   
����� �$��5q�6$     �� CornerRadius$  6��68   $	��410��5�      $	��410��6   $��Center=�6*   $��Bottom=�5�      ._�5�      �� -   5        ! ��Name$! 	GameField��5�      #��[ 6(   + %PacMan.Infrastructure.CanvasAssistant"   BoundChildren" 5�      �� *BoundChildrens+.   (System.Windows.Interactivity.Interaction#   Triggers# 5�      /   )System.Windows.Interactivity.EventTrigger  5�      $   	EventName$$ KeyDown��6%   .   (System.Windows.Interactivity.TriggerBase%   Actions.% 5�      /  )GalaSoft.MvvmLight.Command.EventToCommand  &   PassEventArgsToCommand& . '   Command' 6-   �� ( �� Mode$( OneWay=�$ KeyboardPressCommand�5�      5�      5�      5�      5�      5�      