   M S B A M L   `   `   `  ����J  FQRCodeGenerator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:QRCodeGeneratorQRCodeGenerator  5   	        QRCodeGenerator.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:QRCodeGenerator     5   	     � Title$	  QR ��5   	   $	��480��6   $	��800��6"    � 
ResizeMode$ NoResize=�5	   	   .��5
      � .+�5   
   �� .+�5      o� $��20��6   $��Center=�6   .@�5      m� $��Файл��6   .@�5      m� -   5        $��
SaveButton��5      $��Сохранить��62   6E   m� 5      -   5        $��SaveAsButton��5      $"��Сохранить как...��64   6N   5      m� 5      -   5        $��
InfoButton��5      $��Информация��6-   6A   5      5      � 5   
    � ColumnDefinitions 5      �� 5      6%   �� 5      6%   .+�5      � $��0��6   $��0,45,3.667,280q�6#    � RowDefinitions 5      �� 5      6&   �� 5      6&   .+�5      �� $��0��6   $��15, 0, 0, 0q�6S    �� Target 6%   ��  �� ElementName$ VersionTextBox��.��6h   Версия6p   �� 5      $��0��6   $��	145,0,0,0q�6P    6%   �� $ ECCComboBox��.��6c   #!Уровень коррекции6v   �� 5       $��1��6   $��15,0,0,0q�6[    6%   �� $ EncodementTypeComboBox��.��6m   %#Способ кодирования6�   �� 5!      -   5        $��VersionTextBox��5!      $��0��61   $	��100��6>   $��20��6J   $
��Left=�6V   $��Center=�6q   $��20, 0, 0, 0q�6�    �� 	MaxLength$ 2��6�    �� MaxLines$ 1��6�    � ToolTip$/ )Версия QR кода (от 1 до 40)��6�   .��6  ��5"      �� 5#      	 �� Path$	 Version�6"   
 �� UpdateSourceTrigger$
 PropertyChanged=�62    �� Mode$ OneWayToSource=�6X    �� ValidationRules 5$      !  QRCodeGenerator.VersionRule  5%   "   
   Min$ 1��64   
   Max$ 40��6<   5'      .��5(   $   5)      �� 5*      -   5        $��ECCComboBox��5*      $��0��6/   $	��100��6<   $��20��6H   $
��Left=�6T   $��Center=�6o   $��150, 0, 0, 0q�6�   $� �Уровень коррекции отвечает за то, какое количество информации может быть утеряно��6&   �� SelectedIndex 6�   �� $		 ECC�$
 PropertyChanged=�$ TwoWay=�.@�5+      �� $��L��6    6+   �� 5,      $��M��6    6+   �� 5-      $��Q��6    6+   �� 5.      $��H��6    6+   5/      �� 50      -   5        $��EncodementTypeComboBox��50      $��1��6:   $	��100��6G   $��20��6S   $
��Left=�6_   $��Center=�6z   $��20, 0, 0, 0q�6�   $< 6Метод кодирования информации��6F   6�   �� $	 EncodementType�$
 PropertyChanged=�$ TwoWay=�.@�51      �� $��Числовое��6    $ Цифры 0-9��60   �� 52      $'��!Буквенно-цифровое��6    �� 53      $��Побайтовое��6    $9 3Использование кодировки UTF-8��62   54      �� 55      -   5        $��	TimeCheck��55      $��2��6,   $!��Добавить время��69   $
��Left=�6R   $��Center=�6m   $��	135,0,0,0q�6�   6�   �� 56      -   5        $��	DateCheck��56      $��2��6,   $��Добавить дату��69   $
��Left=�6Q   $��Center=�6l   $��	260,0,0,0q�6�   6�   57      �� 58      $��0��6   $
��Left=�6O   $��Center=�6j   $��15,-130,0,0q�6�    6$   �� $ DataTextBox��.��6�   Информация6�   �� 59      -	   5        $��DataTextBox��59      $��0��6*   $
��Left=�6:   $��Center=�6U   $	��200��6p   $	��360��6}   $��	20,90,0,0q�6�    �� TextWrapping$
 Wrap=�6�   $X RИнформация, которую необходимо закодировать��6�   ��6�   �� $
	 Data�$
 PropertyChanged=�$ TwoWay=�66  �� 5:      -
   5        $��ReadFromFileButtom��5:      $��0��60   $	��170��6@   $��20��6L   $
��Left=�6X   $��Center=�6s   $��210,340,0,0q�6�   .��6�   1/Прочитать данные из файла6�   (� 5;      $��1��6   $
��Left=�6&   $	��380��6A   $��1,25,0,0q�6N   $	��Top=�6`   $	��380��6x   .��5<      �� 
�������$��2q�61   ._�5=      �� -   5        $��QRImage��5=      $	��380��6,   $	��380��69   ��6E   �� $	 QRImage�$
 PropertyChanged=�$ OneWay=�5>      5?      �� 5@      -   5        $��GenerateButton��5@      $��1��6+   $��20��6;   $	��100��6G   $ ��Сгенерировать��6S   $��Bottom=�6k   $��Center=�6�   $��0, 0, 0, 10q�6�   {�.  6�   5A      5B      5C      