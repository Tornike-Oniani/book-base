   M S B A M L   `   `   `  ����C  ?BookBase, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:BookBase.UtilsBookBase.Utils  5      "    BookBase.Utils.CollectionBox   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:BookBase.Utils     5        � 	Resources  5	         ItemTemplate �� Padding � ColumnDefinitions ��Name ��Name  SecondaryButton � Cursor �� RelativeSource �� AncestorType �� Path	 �� ElementName
 �� Triggers P� Property P� Value �� 
TargetName �� Property �� Value  CollectionItemViewTemplate �� 
TargetType ��Name ��Name �� Triggers��%�  &	        7  &	 �    �� 5
   
   .R�5      �� $ 4 2 4 2q�6   
������ ._�5      �  5      �� 5      $��* �6+   �� 5      $
��Auto �6+   .+�5      �� $ ItemText��6    $��Center=�5       $��0��5       ��5       �� 5       �� 5      $ DeleteButton��6   $��-��5      8��  $y�Hidden=�5       $
 Hand��5!      $��1��5"      ��5      ��  �� *FindAncestor+" >�$ RemoveItemFromListCommand���5      �� $	 ItemText��$
 Text�5"      5#      5$      
 5%      P� 5&       �@J�IsMouseOver . . �5'      �� $ DeleteButton��6    � y�$ Visible=�6M   5(      5*      �� 5+   
   " ��6=   .R�5,      �� $ wrapper��6   ���#ffffff$ 5 0 5 0q�5.      $��0 0 0 4q�5/      ._�50      � .+�51      �� $ content��6'   52      53       54      P� 55       �@��
IsSelected . . �56      �� $ wrapper��6    � �� �#e5e6e66H   57      59      5:      .��5;      �� .+�5<   
   ��  
 Header#��[ 5=      ��5<      �� *Text+ �� *FindAncestor+" >� �� FallbackValue$ 
SampleText��5=      �� 5>   
     GenericTextBox#��[ 5?      $��0 0 0 10q�5@      ��5>      �� *Item+ �� UpdateSourceTrigger$ PropertyChanged=� �� *FindAncestor+" >�.��5@   $    J� InputBindings 5A      �� 5B      
 �� Key$ Enter��6    �� Command 6)   �� *AddItemToListCommand+ �� *FindAncestor+" >�.��5C   %   5D      �� 5E   
   -   5         ��Name$
 Test��5E        ItemsViewTemplate#��[ 5G      $��Stretch=�5I      ��5F      �� $ ItemsSource� �� *FindAncestor+" >���5H      �� *	BoxHeight+ �� *FindAncestor+" >���5J      �� 5K       �� 
TargetType" ��6   .�5L      ��  � |� .  62   �� 5M       � ��# [ 61   �� 5N       � ��# [  68   5O      5Q      5R      5S      