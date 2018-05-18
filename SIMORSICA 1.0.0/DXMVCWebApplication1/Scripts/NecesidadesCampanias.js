/*****************************************************************************************
    Suma las cantidades mensuales para mostrar el total anual
*****************************************************************************************/
function calculaCantidadesMensuales(s, e) {
    var sumaCantidadesMensuales = ((Cant_Ene.number * 100) + (Cant_Feb.number * 100) + (Cant_Mar.number * 100) + (Cant_Abr.number * 100) + (Cant_May.number * 100) + (Cant_Jun.number * 100)
                                + (Cant_Jul.number * 100) + (Cant_Ago.number * 100) + (Cant_Sep.number * 100) + (Cant_Oct.number * 100) + (Cant_Nov.number * 100) + (Cant_Dic.number * 100)) / 100;

    document.getElementById('SumaCantidadesTotales_I').value = sumaCantidadesMensuales;
}

/*****************************************************************************************
    Verifica que la suma de los importes mensuales no supere el presupuesto otorgado
*****************************************************************************************/
function calculaImportesMensuales(s, e) {
    var sumaImportesMensuales = ((imp_Ene.number * 100) + (imp_Feb.number * 100) + (imp_Mar.number * 100) + (imp_Abr.number * 100) + (imp_May.number * 100) + (imp_Jun.number * 100)
                                + (imp_Jul.number * 100) + (imp_Ago.number * 100) + (imp_Sep.number * 100) + (imp_Oct.number * 100) + (imp_Nov.number * 100) + (imp_Dic.number * 100)) / 100;

    document.getElementById('PresupuestoTotal_I').value = sumaImportesMensuales;
}