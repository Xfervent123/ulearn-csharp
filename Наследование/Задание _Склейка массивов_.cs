/*
╔════════════════════════════════════════════════════════════════════╗
║                         Склейка массивов                           ║
╚════════════════════════════════════════════════════════════════════╝

Реализуйте метод Combine, который возвращает массив, собранный из переданных массивов.
Для того, чтобы создать новый массив, используйте статический метод Array.CreateInstance, принимающий тип элемента массива.
Для того, чтобы узнать тип элементов в переданном массиве, используйте myArray.GetType().GetElementType().
Проверьте, что типы элементов совпадают во всех переданных массивах!
Если результирующий массив создать нельзя, возвращайте null.

*/


public static Array Combine(params Array[] arrays)
{
    if (arrays.Length == 0) return null;
    var elementType = arrays[0].GetType().GetElementType();
    
    var totalLength = 0;
    foreach (var array in arrays)
    {
        if (array.GetType().GetElementType() != elementType)
            return null;
            
        totalLength += array.Length;
    }

    var result = Array.CreateInstance(elementType, totalLength);
    var index = 0;
    foreach (var array in arrays)
    {
        for (int i = 0; i < array.Length; i++)
        {
            var value = array.GetValue(i);
            result.SetValue(value, index);
            index++;
        }
    }

    return result;
}
