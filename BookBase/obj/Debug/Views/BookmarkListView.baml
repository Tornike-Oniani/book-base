   M S B A M L   `   `   `  ����C  ?BookBase, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:BookBase.ViewsBookBase.Views  5      /clr-namespace:BookBase.UtilsBookBase.Utils  5      %    BookBase.Views.BookmarkListView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      m iMahApps.Metro.IconPacks.BootstrapIcons, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a?	iconPacks-http://metro.mahapps.com/winfx/xaml/iconpacks   5      *localclr-namespace:BookBase.Views   5      *utilsclr-namespace:BookBase.Utils   5        � 	Resources  5         bookmarkDisplayMode � ColumnDefinitions �� TextDecorations �� Command �� RelativeSource �� AncestorType �� CommandParameter  LinkText �� Orientation  
IconButton5  /MahApps.Metro.IconPacks.BootstrapIconsExtension   Kind  bookmarkEditMode	 �� Padding  EditTextBox  BackgroundLineColorBrush  
EditButton  SecondaryButton��%�  &	        7  7  7  &	 �    7  7  7  7  �� 5   
   .R�5      �  5      �� 5      $��* �6'   �� 5      $��* �6'   �� 5      $
��Auto �6'   .+�5      �� $��0��6   $��Center=�5      �� 5      $
 None{�6    ��5       Z�  5       �� *!DataContext.openBookmarkCommand+ �� *FindAncestor+" >� 5       �� .�6=   .�5      �� 8��  $��0��5    $   ��5   $   �� *name+5    $   5!      5"      �� 5%      $��Center=�5&      $��1��5'      ��5%      �� *
booksCount+5'      �� 5*      $ 
Horizontal=�6   $��Center=�5+      $��70��5,      $��2��5-      .+�5.      �� 8�� $��0 0 5 0q�50      ��5.        $ Pencil=���51      �� *" DataContext.enterEditModeCommand+ �� *FindAncestor+" >���52      �� �� 53      8�� ��6     $ Trash=���55      �� *#!DataContext.deleteBookmarkCommand+ �� *FindAncestor+" >���56      �� 57      58      59      �� 5:   
   .R�5;      �� .+�5<      �� $	 
25 10 25 0q�6   ._�5=      �� 8��  $	��140��5?      $
��Auto��5@      ��5=      �� *name+5@      5A      �� 5E      8�� $��1 20 1 1q�5F      ._�5G      �� $ 
Horizontal=�6!   $��Right=�5H   !   $��
0 10 10 10q�5I   !   .+�5J      �� $��Update��6!   8�� $��0 0 5 0q�5L   !   ��5M   !   �� *#!DataContext.updateBookmarkCommand+ �� *FindAncestor+" >���5N   !   �� �� 5O      $��Cancel��6!   8�� ��5Q   !   �� *!DataContext.exitEditModeCommand+ �� *FindAncestor+" >���5R   !   �� 5S      5T      5U      5V      5W      .��5Y      � 
 � RowDefinitions
 5Z   
   �� 5\      $
��Auto �6   �� 5^      $��* �6   .+�5b   
   �� ���#c6c7c8$��0 0 0 3q�5c      $		 0 5q�5d      $��0 0 0 10q�5e      $��0��5f      ._�5g      � .+�5i      �� $ 
Horizontal=�6   $
��Left=�5j      $��Center=�5k      y�5l      �� *creatingBookmark+  BoolToVisibilityInverse �� 	Converter# [ .+�5m      �� $��Add bookmark��6    	 PrimaryButton#��[	 5n      ��5o      �� *addBookmarkCommand+5p      �� 5s      $ 
Horizontal=�6   $
��Left=�5t      $��Center=�5u      y�5v      �� *creatingBookmark+ 
 BoolToVisibility# [
 .+�5w      ��   GenericTextBox#��[ 5y      $	��200��5z      $��0 0 5 0q�5{      ��5w      �� *bookmarkName+ �� UpdateSourceTrigger$ PropertyChanged=�#   BookBase.Utils.FocusExtension   	IsFocused 5x      �� *bookmarkNameFocus+.��5{   /    J� InputBindings 5|      �� 5}      
 �� Key$ Enter��6S    �� Command 6)   �� *createBookmarkCommand+6S   .��5~   1   5      �� 5�      $��Create��6   #��[	 5�      ��5�      �� *createBookmarkCommand+5�      5�      5�      �� 5�   
   $��1��5�      ��5�      �� *	bookmarks+��5�      �� 5�       �� 
TargetType" ��6"   .R�5�      �   BackgroundColorBrush#��[ 6   
 5�      �� 5�      $
��Auto �6,   �� 5�      $��* �6,   .+�5�      � $��0��6   $��35 17 35 17q�5�       5�      �� 5�   "   $��* �63   �� 5�   "   $��* �63   �� 5�   "   $
��Auto �63   .+�5�      �� $��Bookmark Name��6(     	LabelText#��[ 5�   (   $��0��5�   (   �� 5�      $��Books��6(   #��[ 5�   (   $��1��5�   (   �� 5�      $�� ��6(   $��72��5�   (   $��2��5�   (   5�      �� 5�      $��	10 5 10 5q�6'   $��1��5�   '   .��5�      �� 5�      5�      5�      ��5�      �� 5�       �� 
TargetType" ��6   .�5�      ��  �� Property � |� �� Value .  62   �� 5�       � ��  ItemViewTemplate# [ 61   �� 5�       � ��# [  68    �� Triggers 5�      �� 5�       �� Binding 6&   �� *
editMode+ �� Value True6C   .a�5�      ��  � ��# [ 6@   �� 5�       � ��  ItemViewTemplateEdit# [ 69   5�      5�      5�      5�      5�      