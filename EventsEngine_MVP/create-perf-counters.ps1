$categoryName = "Cascade Data Access COM"
$categoryHelp = "Cascade Data Access COM visible DLL consumed by classic ASP pages to comunicate with the databases"
$categoryType = [System.Diagnostics.PerformanceCounterCategoryType]::MultiInstance

$categoryExists = [System.Diagnostics.PerformanceCounterCategory]::Exists($categoryName)

If (-Not $categoryExists) {
    $objCCDC = New-Object System.Diagnostics.CounterCreationDataCollection

    $objCounter = New-Object System.Diagnostics.CounterCreationData
    $objCounter.CounterName = "SendMessage execution Time in ms"
    $objCounter.CounterType = "NumberOfItems32"
    $objCounter.CounterHelp = "Number of ms executing the method SendMessage"
    $objCCDC.Add($objCounter) | Out-Null

    [System.Diagnostics.PerformanceCounterCategory]::Create($categoryName, $categoryHelp, $categoryType, $objCCDC) | Out-Null
}