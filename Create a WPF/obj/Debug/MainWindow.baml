   M S B A M L   `   `   `  ����  Create a WPF    Create_a_WPF.MainWindow   5   	   e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��600��6)   
���   ���65   Z� .��5      � -   5        $��grid1��5      ���	#FFFDF9F9 � ColumnDefinitions 5	   
   �� 5
      $
��0.7* �6   �� 5      $��4* �6   �� 5       � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��4* �6   �� 5      $
��1.5* �6   .+�5   
   +  %System.Windows.Controls.Ribbon.Ribbon  $��0��6   $��3��6   .@�5      .  (System.Windows.Controls.Ribbon.RibbonTab  $
��Home��6   .@�5      0  *System.Windows.Controls.Ribbon.RibbonGroup  .@�5      1  +System.Windows.Controls.Ribbon.RibbonButton  -   5        $��btnStart��5   #   $
��Left=�64   $	��Top=�6O      SmallImageSource$ /Icon/play.png��6g      Label$ Start��6�   $��71��6�   $	��106��6�   $��Center=�6�   $��20(�6�   6�   5      5      5      ]� 5   
   $��2��6   $��3��6!   .+�5      �� -   5        $��tabCtrlOutPut��5      ���	#FF5D6B99$��5,5,5,5q�6F   .@�5      �� $��Output��6   .��5      �� -   5         ��Name$ 	txtOutput��5       �� 
IsReadOnly . " �� VerticalScrollBarVisibility$
 Auto=�6C   5      5      5       5!      5"      