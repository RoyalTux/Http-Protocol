   M S B A M L   `   `   `  ����?  ;http, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:httphttp  5   	       http.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:http     5   	     � Title$  Http download site��5   	   $	��147��6$   $	��477��61    � WindowStartupLocation$ CenterScreen=�6=   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �  � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      .+�5      �� $��
4,16,5.6,0q�6   $��Center=�6,    J� RenderTransformOrigin$ 0.402,1.214&�6G   URL:6q   �� 5      -   5        $	��URL��5      $��5.4,6,5.4,0.2q�6!   $��1��68   5      � 5   
   $��1��6    5      �� 5      $
��Auto �6#   �� 5      �� 5      $
��Auto �6#   .+�5      �� $��Center=�6   $��3,20,7,-2.4q�63   Path:6O   �� 5      -   5        $
��Path��5   )    �� 
IsReadOnly .  $��1��65   $��2��6E   $��0,6,81.4,-0.2q�6Y   �� 5      -   5        $��2��5   (   $
��Auto��68   $��10.2,8,10.4,-2.2q�6F   .��6`   Open6f   5       � 5!   
   $��2��6    5"      �� 5#      $
��Auto �6#   �� 5$      .+�5&      �� -   5        $��	Recursion��5&   J   $��Center=�6   $��10,19,0.2,-3.2q�62   .��6[   	Recursion6f   �� 5'      -   5        $��1��5'   &   $
��Auto��66   $��120.8,14,10.4,-7.6q�6D   .��6`   
Download6j   5(      5)      5*      