:: @ECHO OFF

SET rootPath=%~dp0
SET xsdPath="C:\Program\Microsoft Visual Studio 8\SDK\v2.0\Bin\xsd.exe"
SET fileRoot=C:\Documents and Settings\cberg\Skrivbord\SFTI_Basic_Invoice20081227\SFTI Basic Invoice_1.0\fsv\xsd
SET file2="%fileRoot%\codelist\UBL-CodeList-SubstitutionStatusCode-1.0.xsd"
SET file3="%fileRoot%\codelist\UBL-CodeList-PaymentMeansCode-1.0.xsd"
SET file4="%fileRoot%\codelist\UBL-CodeList-OperatorCode-1.0.xsd"
SET file5="%fileRoot%\codelist\UBL-CodeList-LongitudeDirectionCode-1.0.xsd"
SET file6="%fileRoot%\codelist\UBL-CodeList-LineStatusCode-1.0.xsd"
SET file7="%fileRoot%\codelist\UBL-CodeList-LatitudeDirectionCode-1.0.xsd"
SET file8="%fileRoot%\codelist\UBL-CodeList-DocumentStatusCode-1.0.xsd"
SET file9="%fileRoot%\codelist\UBL-CodeList-CurrencyCode-1.0.xsd"
SET file10="%fileRoot%\codelist\UBL-CodeList-CountryIdentificationCode-1.0.xsd"
SET file11="%fileRoot%\codelist\UBL-CodeList-ChipCode-1.0.xsd"
SET file12="%fileRoot%\codelist\UBL-CodeList-ChannelCode-1.0.xsd"
SET file13="%fileRoot%\codelist\UBL-CodeList-AllowanceChargeReasonCode-1.0.xsd"
SET file14="%fileRoot%\codelist\UBL-CodeList-AcknowledgementResponseCode-1.0.xsd"
SET file15="%fileRoot%\common\UBL-SpecializedDatatypes-1.0.xsd"
SET file16="%fileRoot%\common\UBL-UnspecializedDatatypes-1.0.xsd"
SET file17="%fileRoot%\common\SFTI-CommonAggregateComponents-1.0.xsd"
SET file18="%fileRoot%\common\UBL-CommonAggregateComponents-1.0.xsd"
SET file19="%fileRoot%\common\UBL-CommonBasicComponents-1.0.xsd"
SET file20="%fileRoot%\common\UBL-CoreComponentParameters-1.0.xsd"
SET file21="%fileRoot%\common\UBL-CoreComponentTypes-1.0.xsd"
%xsdPath% /c %file1% %file2% %file3% %file4% %file5% %file6% %file7% %file8% %file9% %file10% %file11% %file12% %file13% %file14% %file15% %file16% %file17% %file18% %file19% %file20% %file21%
PAUSE



