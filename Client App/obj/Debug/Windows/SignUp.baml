   M S B A M L   `   `   `  ����  
Client App    Client_App.Windows.SignUp   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   V RMaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5   	     � Title$  Sign Up��5   	   $	��450��6   $	��800��6&   .��5	      � ���	#FF3F4562 � ColumnDefinitions 5
   
   �� 5      �� 5      $	��500 �6   �� 5      .+�5   
   � $��1��6   .+�5      �� 
�������$	��450��6*    �� RadiusX$ 15��66    �� RadiusY$ 15��6C   $��25,137,25,90q�6P   �� 5      $	��400��6   $��49,146,51,56q�6%   .+�5      �� -   5         ��Name$ Nickname��5      $
��0,15q�6,   $��20(�6:    J� Opacity$	 0.8Q�6H   $��DemiBold"�6V   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Nickname��6l    %  !MaterialDesignFloatingHintTextBox#��[  6�   �� 5      -   5        $ Email��5      $
��0,15q�6)   $��20(�67   $	 0.8Q�6E   $��DemiBold"�6S   $ Email��6i   #��[  6�   �� 5      -   5         ��Name$ FinishSignUp��5      ���	#FF373B52$��10q�6x   $��35��6�   $��OK��6�   $��DemiBold"�6�   $��15(�6�   $	��100��6�   ��6a   Z� 6�   5      A� 5      -   5        $	��150��5      $	��150��6"   $	��Top=�6/   $��10q�6G   ��5      �� 5      -   5        
ProfileImg�� j� Stretch$ UniformToFill=�5   5   	 �� ImageSource$F	 @/Client App;component/Sourse Images/stickeroid_5bf54b1f18d07.png��6M   5      5      9� 5   
   $��Settings��6   
�������$��2��68   $	��150��6H   $	��Top=�6U   ���	#FF3F4562.��5      �� .+�5      �� -   5        $ 	IpAddress��5      $��10.2.118.14��6-   $ 
IP address��6@   #��[  6l   $��DemiBold"�6�   
�������$��2q�6�   �� 5      -   5        $
 Port��5      $��50000��6(   $
 Port��65   #��[  6[   $��DemiBold"�6�   
�������$��2q�6�   5       5!      5"      5#      