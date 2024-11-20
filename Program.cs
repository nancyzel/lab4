namespace task1
{
    internal class Program
    {
        static void PrintArray(int[] basicArray)
        {
            for (int i = 0; i < basicArray.Length; i++)
            {
                Console.Write(basicArray[i] + " ");
            }
            Console.WriteLine();
        }

        static void GetArrayUsingRandomElements(int[] basicArray, int arrayLength)
        {
            Random randomElement = new Random();
            int minimum = Int32.MaxValue;
            int maximum = Int32.MinValue;
            bool isAppropriate;
            do
            {
                try
                {
                    Console.WriteLine("Введите целое число - минимальное возможное значение для элементов массива:");
                    minimum = int.Parse(Console.ReadLine());
                    isAppropriate = true;
                }
                catch (FormatException)
                {
                    isAppropriate = false;
                    Console.WriteLine("Введённое вами значение не является целым числом");
                }
                catch (OverflowException)
                {
                    isAppropriate = false;
                    Console.WriteLine("Введённое вами значение выходит за границы типа Int32 (меньше -2147483648 или больше 2147483647)");
                }
            } while (!isAppropriate);
            do
            {
                try
                {
                    Console.WriteLine("Введите целое число - максимальное возможное значение для элементов массива:");
                    maximum = int.Parse(Console.ReadLine());
                    if (maximum < minimum)
                    {
                        isAppropriate = false;
                        Console.WriteLine("Ошибка: введенное вами максимальное значение меньше введенного минимального значения");
                    }
                    else
                    {
                        isAppropriate = true;
                    }
                }
                catch (FormatException)
                {
                    isAppropriate = false;
                    Console.WriteLine("Введённое вами значение не является целым числом");
                }
                catch (OverflowException)
                {
                    isAppropriate = false;
                    Console.WriteLine("Введённое вами значение выходит за границы типа Int32 (меньше -2147483648 или больше 2147483647)");
                }
            } while (!isAppropriate);
            for (int i = 0; i < arrayLength; i++)
            {
                basicArray[i] = randomElement.Next(minimum, maximum);
            }
        }

        static void GetArrayUsingInput(int[] basicArray, int arrayLength)
        {
            bool isAppropriate;
            for (int i = 0; i < arrayLength; i++)
            {
                do
                {
                    try
                    {
                        Console.WriteLine("Введите целое число - очередной элемент массива:");
                        basicArray[i] = int.Parse(Console.ReadLine());
                        isAppropriate = true;
                    }
                    catch (FormatException)
                    {
                        isAppropriate = false;
                        Console.WriteLine("Введённое вами значение не является целым числом");
                    }
                    catch (OverflowException)
                    {
                        isAppropriate = false;
                        Console.WriteLine("Введённое вами значение выходит за границы типа Int32 (меньше -2147483648 или больше 2147483647)");
                    }
                } while (!isAppropriate);
            }
            Console.WriteLine("Исходный массив:");
            PrintArray(basicArray);
        }

        static (int[], bool) AddNewArrayElements(int[] basicArray, bool isArrayEmpty, int arrayLength)
        {
            bool isAppropriateSubMenuPoint;
            int subMenuPoint = 3;
            do
            {
                try
                {
                    Console.WriteLine();
                    Console.WriteLine("1. Добавить в массив элементы с помощью датчика случайных чисел");
                    Console.WriteLine("2. Добавить в массив элементы, введённые с клавиатуры");
                    Console.WriteLine();
                    Console.WriteLine("Выберите способ добавления элементов в массив");
                    subMenuPoint = int.Parse(Console.ReadLine());
                    if (subMenuPoint > 0 && subMenuPoint < 3)
                    {
                        isAppropriateSubMenuPoint = true;
                    }
                    else
                    {
                        isAppropriateSubMenuPoint = false;
                        Console.WriteLine("Введенное значение не соответствует ни одному пункту меню");
                    }
                }
                catch (FormatException)
                {
                    isAppropriateSubMenuPoint = false;
                    Console.WriteLine("Введенное значение не является целым числом");
                }
                catch (OverflowException)
                {
                    isAppropriateSubMenuPoint = false;
                    Console.WriteLine("Введенное значение не соответствует ни одному пункту меню");
                }
            } while (!isAppropriateSubMenuPoint);
            switch (subMenuPoint)
            {
                case 1:
                    Console.WriteLine("Элементы будут добавлены в массив с помощью датчика случайных чисел");
                    GetArrayUsingRandomElements(basicArray, arrayLength);
                    isArrayEmpty = false;
                    break;
                case 2:
                    Console.WriteLine("Элементы будут добавлены в массив вводом с клавиатуры");
                    GetArrayUsingInput(basicArray, arrayLength);
                    isArrayEmpty = false;
                    break;
            }
            return (basicArray, isArrayEmpty);
        }

        static (int[], bool) RemoveMaxElements(int[] basicArray, bool isArrayEmpty)
        {
            int[] temporaryArray = new int[0];
            int temporaryArrayIndex = 0;
            int maxElement = basicArray[0];
            int maxElementsQuantity = 0;
            for (int i = 0; i < basicArray.Length; i++)
            {
                if (basicArray[i] > maxElement)
                {
                    maxElement = basicArray[i];
                    maxElementsQuantity = 0;
                }
                if (basicArray[i] == maxElement)
                {
                    maxElementsQuantity++;
                }
            }
            if (maxElementsQuantity == basicArray.Length)
            {
                Console.WriteLine("Все элементы в массиве равны, поэтому они все являются максимумами. \nВсе элементы будут удалены из массива.");
                isArrayEmpty = true;
            }
            else
            {
                Console.WriteLine($"Максимальное значение среди элементов: {maxElement}; количество элементов с максимальным значением: {maxElementsQuantity}. \nЭти элементы будут удалены.");
                temporaryArray = new int[basicArray.Length - maxElementsQuantity];
                for (int i = 0; i < basicArray.Length; i++)
                {
                    if (basicArray[i] != maxElement)
                    {
                        temporaryArray[temporaryArrayIndex++] = basicArray[i];
                    }
                }
                Console.WriteLine("Состав массива после удаления максимальных элементов:");
                PrintArray(temporaryArray);
            }
            basicArray = temporaryArray;
            return (basicArray, isArrayEmpty);
        }

        static (int[], bool) AddElementsArrayEnd(int[] basicArray, bool isArrayEmpty)
        {
            int addedElementsQuantity = 0;
            bool isAppropriateAddedElementsQuantity;
            int[] temporaryArray;
            int[] extraTemporaryArray = new int[0];
            int extraTemporaryArrayIndex = 0;
            do
            {
                try
                {
                    Console.WriteLine("Введите неотрицательное целое число - количество элементов, которое вы хотите добавить в конец массива:");
                    addedElementsQuantity = int.Parse(Console.ReadLine());
                    if (addedElementsQuantity < 0)
                    {
                        isAppropriateAddedElementsQuantity = false;
                        Console.WriteLine("Количество элементов, которые нужно добавить в массив, не должно быть отрицательным");
                    }
                    else
                    {
                        isAppropriateAddedElementsQuantity = true;
                    }
                }
                catch (FormatException)
                {
                    isAppropriateAddedElementsQuantity = false;
                    Console.WriteLine("Введённое вами значение не является целым числом");
                }
                catch (OverflowException)
                {
                    isAppropriateAddedElementsQuantity = false;
                    Console.WriteLine("Введённое вами значение выходит за границы типа Int32 (меньше -2147483648 или больше 2147483647)");
                }
            } while (!isAppropriateAddedElementsQuantity);
            if (addedElementsQuantity == 0)
            {
                Console.WriteLine("Количество элементов, которое нужно добавить, равно 0, поэтому состав массива не изменится");
            }
            else
            {
                temporaryArray = new int[addedElementsQuantity];
                (temporaryArray, isArrayEmpty) = AddNewArrayElements(temporaryArray, isArrayEmpty, addedElementsQuantity);
                if (basicArray.Length == 0 || isArrayEmpty)
                {
                    basicArray = temporaryArray;
                }
                else
                {
                    extraTemporaryArray = new int[basicArray.Length + addedElementsQuantity];
                    for (int i = 0; i < basicArray.Length; i++)
                    {
                        extraTemporaryArray[extraTemporaryArrayIndex++] = basicArray[i];
                    }
                    for (int i = 0; i < addedElementsQuantity; i++)
                    {
                        extraTemporaryArray[extraTemporaryArrayIndex++] = temporaryArray[i];
                    }
                    basicArray = extraTemporaryArray;
                    Console.WriteLine("Состав массива на данный момент:");
                    PrintArray(basicArray);
                }
            }
            return (basicArray, isArrayEmpty);
        }

        static int[] ReplaceElementsCyclicalWay(int[] basicArray)
        {
            int replaceStep = 0;
            bool isAppropriateReplaceStep;
            int[] temporaryArray = new int[basicArray.Length];
            do
            {
                try
                {
                    Console.WriteLine("Введите неотрицательное целое число - на сколько элементов влево вы хотите сдвинуть каждое значение в массиве:");
                    replaceStep = int.Parse(Console.ReadLine()) % basicArray.Length;
                    if (replaceStep < 0)
                    {
                        isAppropriateReplaceStep = false;
                        Console.WriteLine("Количество элементов, на которое нужно сместить массив, не должно быть отрицательным");
                    }
                    else
                    {
                        isAppropriateReplaceStep = true;
                    }
                }
                catch (FormatException)
                {
                    isAppropriateReplaceStep = false;
                    Console.WriteLine("Введённое вами значение не является целым числом");
                }
                catch (OverflowException)
                {
                    isAppropriateReplaceStep = false;
                    Console.WriteLine("Введённое вами значение выходит за границы типа Int32 (меньше -2147483648 или больше 2147483647)");
                }
            } while (!isAppropriateReplaceStep);
            if (replaceStep == 0)
            {
                Console.WriteLine("Количество элементов, на которое нужно сместить массив, равно 0, поэтому порядок элементов в массиве не изменится.");
            }
            else
            {
                for (int i = 0; i < basicArray.Length; i++)
                {
                    temporaryArray[i] = basicArray[(i + replaceStep) % basicArray.Length];
                }
                basicArray = temporaryArray;
                Console.WriteLine("Состав массива на данный момент:");
                PrintArray(basicArray);
            }
            return basicArray;
        }

        static void FindFirstNegativeElement(int[] basicArray)
        {
            int comparisonQuantity = 0;
            int arrayIndex = 0;
            bool isNegativeElementFound = false;
            do
            {
                if (basicArray[arrayIndex++] < 0)
                {
                    isNegativeElementFound = true;
                }
                comparisonQuantity++;
            } while (!isNegativeElementFound || arrayIndex >= basicArray.Length);
            if (isNegativeElementFound)
            {
                Console.WriteLine($"Первый отрицательный элемент в массиве: {basicArray[--arrayIndex]}; количество сравнений, необходимых для поиска этого элемента: {comparisonQuantity}.");
            }
            else
            {
                Console.WriteLine("В массиве не содержится отрицательных элементов.");
            }
        }

        static int[] SortOutArraySelection(int[] basicArray)
        {
            for (int i = 1; i < basicArray.Length; i++)
            {
                for (int j = basicArray.Length - 1; j >= i; j--)
                {
                    if (basicArray[j] < basicArray[j - 1])
                    {
                        (basicArray[j], basicArray[j - 1]) = (basicArray[j - 1], basicArray[j]);
                    }
                }
            }
            Console.WriteLine("Состав отсортированного массива:");
            PrintArray(basicArray);
            return basicArray;
        }

        static void FindArrayElement(int[] basicArray)
        {
            int comparisonQuantity = 0;
            int targetElement = Int32.MinValue;
            int leftMarker = 0;
            int rightMarker = basicArray.Length - 1;
            int middleMarker;

            bool isAppropriateTargetElement;
            basicArray = SortOutArraySelection(basicArray);
            do
            {
                try
                {
                    Console.WriteLine("Введите целое число - искомый элемент:");
                    targetElement = int.Parse(Console.ReadLine());
                    isAppropriateTargetElement = true;
                }
                catch (FormatException)
                {
                    isAppropriateTargetElement = false;
                    Console.WriteLine("Введённое вами значение не является целым числом");
                }
                catch (OverflowException)
                {
                    isAppropriateTargetElement = false;
                    Console.WriteLine("Введённое вами значение выходит за границы типа Int32 (меньше -2147483648 или больше 2147483647)");
                }
            } while (!isAppropriateTargetElement);
            do
            {
                middleMarker = (leftMarker + rightMarker) / 2;
                if (basicArray[middleMarker] < targetElement)
                {
                    leftMarker = middleMarker + 1;
                }
                else
                {
                    rightMarker = middleMarker;
                }
                comparisonQuantity++;
            } while (leftMarker != rightMarker);
            if (basicArray[leftMarker] == targetElement)
            {
                while ((leftMarker - 1) > 0 && basicArray[leftMarker - 1] == targetElement)
                {
                    leftMarker--;
                }
                Console.WriteLine($"Элемент найден. Позиция первого элемента с искомым значением в отсортированном массиве: {leftMarker + 1};  количество сравнений, необходимых для поиска этого элемента: {comparisonQuantity}.");
            }
            else
            {
                Console.WriteLine("Искомый элемент отсутствует в списке.");
            }
        }

        static int[] SortOutArrayShaker(int[] basicArray)
        {
            int leftMarker = 0;
            int rightMarker = basicArray.Length - 1;
            while (leftMarker <= rightMarker)
            {
                for (int i = rightMarker; i > leftMarker; i--)
                {
                    if (basicArray[i - 1] > basicArray[i])
                    {
                        (basicArray[i - 1], basicArray[i]) = (basicArray[i], basicArray[i - 1]);
                    }
                }
                leftMarker++;
                for (int i = leftMarker; i < rightMarker; i++)
                {
                    if (basicArray[i] > basicArray[i + 1])
                    {
                        (basicArray[i], basicArray[i + 1]) = (basicArray[i + 1], basicArray[i]);
                    }
                }
                rightMarker--;
            }
            Console.WriteLine("Состав отсортированного массива:");
            PrintArray(basicArray);
            return basicArray;
        }

        static void Main(string[] args)
        {
            bool isAppropriate;
            bool isAppropriateMenuPoint;
            bool isArrayEmpty = true;

            int menuPoint = 9;
            int arrayLength = 0;
            int[] basicArray = new int[0];

            #region menu
            Console.WriteLine("Добро пожаловать! Данная программа поможет вам обработать массив целых чисел.");
            do
            {
                do
                {
                    try
                    {
                        Console.WriteLine();
                        Console.WriteLine("1. Сформировать массив");
                        Console.WriteLine("2. Вывести массив в консоль");
                        Console.WriteLine("3. Выполнить удаление всех элементов с максимальным значением");
                        Console.WriteLine("4. Выполнить добавление заданного числа элементов в конец массива");
                        Console.WriteLine("5. Переставить элементы в массиве циклически на заданное число элементов влево");
                        Console.WriteLine("6. Выполнить поиск первого отрицательного элемента в массиве и подсчитать количество сравнений, необходимых для поиска этого элемента");
                        Console.WriteLine("7. Выполнить сортировку массива (методом простого обмена)");
                        Console.WriteLine("8. Выполнить поиск элемента, который вводит пользователь с клавиатуры, в отсортированном массиве (бинарный поиск) и подсчитать количество сравнений, необходимых для поиска нужного элемента");
                        Console.WriteLine("9. Выполнить шейкер-сортировку массива");
                        Console.WriteLine("10. Завершить работу");
                        Console.WriteLine();
                        Console.WriteLine("Выберите один из пунктов меню (введите нужную цифру):");
                        menuPoint = int.Parse(Console.ReadLine());
                        if (menuPoint > 0 && menuPoint < 11)
                        {
                            isAppropriateMenuPoint = true;
                        }
                        else
                        {
                            isAppropriateMenuPoint = false;
                            Console.WriteLine("Введенное значение не соответствует ни одному пункту меню");
                        }
                    }
                    catch (FormatException)
                    {
                        isAppropriateMenuPoint = false;
                        Console.WriteLine("Введенное значение не является целым числом");
                    }
                    catch (OverflowException)
                    {
                        isAppropriateMenuPoint = false;
                        Console.WriteLine("Введенное значение не соответствует ни одному пункту меню");
                    }
                } while (!isAppropriateMenuPoint);
                switch (menuPoint)
                {
                    case 1:
                        Console.WriteLine("Вы выбрали: сформировать массив");
                        do
                        {
                            try
                            {
                                Console.WriteLine("Введите положительное целое число, большее 0 - длину массива, который вы хотите обработать:");
                                arrayLength = int.Parse(Console.ReadLine());
                                if (arrayLength == 0)
                                {
                                    isAppropriate = false;
                                    Console.WriteLine("Для корректной обработки массив не должен быть пустым");
                                }
                                else
                                {
                                    if (arrayLength < 0)
                                    {
                                        isAppropriate = false;
                                        Console.WriteLine("Количество элементов в массиве не может быть отрицательным");
                                    }
                                    else
                                    {
                                        isAppropriate = true;
                                    }
                                }
                            }
                            catch (FormatException)
                            {
                                isAppropriate = false;
                                Console.WriteLine("Введённое вами значение не является целым числом больше 0");
                            }
                            catch (OverflowException)
                            {
                                isAppropriate = false;
                                Console.WriteLine("Введённое вами значение является слишком большим (больше 2147483647)");
                            }
                        } while (!isAppropriate);
                        basicArray = new int[arrayLength];
                        (basicArray, isArrayEmpty) = AddNewArrayElements(basicArray, isArrayEmpty, arrayLength);
                        Console.WriteLine("Исходный массив:");
                        PrintArray(basicArray);
                        break;
                    case 2:
                        Console.WriteLine("Вы выбрали: вывести массив в консоль");
                        if (!isArrayEmpty && basicArray.Length != 0)
                        {
                            Console.WriteLine("Состав массива на данный момент:");
                            PrintArray(basicArray);
                        }
                        else
                        {
                            Console.WriteLine("Выбранное действие совершить невозможно, так как массив пустой.");
                        }
                        break;
                    case 3:
                        Console.WriteLine("Вы выбрали: выполнить удаление всех элементов с максимальным значением");
                        if (!isArrayEmpty && basicArray.Length != 0)
                        {
                            (basicArray, isArrayEmpty) = RemoveMaxElements(basicArray, isArrayEmpty);
                        }
                        else
                        {
                            Console.WriteLine("Выбранное действие совершить невозможно, так как массив пустой.");
                        }
                        break;
                    case 4:
                        Console.WriteLine("Вы выбрали: выполнить добавление заданного числа элементов в конец массива");
                        (basicArray, isArrayEmpty) = AddElementsArrayEnd(basicArray, isArrayEmpty);
                        break;
                    case 5:
                        Console.WriteLine("Вы выбрали: переставить элементы в массиве циклически на заданное число элементов влево");
                        if (!isArrayEmpty && arrayLength != 0)
                        {
                            basicArray = ReplaceElementsCyclicalWay(basicArray);
                        }
                        else
                        {
                            Console.WriteLine("Выбранное действие совершить невозможно, так как массив пустой.");
                        }
                        break;
                    case 6:
                        Console.WriteLine("Вы выбрали: выполнить поиск первого отрицательного элемента в массиве и подсчитать количество сравнений, необходимых для поиска этого элемента");
                        if (!isArrayEmpty && arrayLength != 0)
                        {
                            FindFirstNegativeElement(basicArray);
                        }
                        else
                        {
                            Console.WriteLine("Выбранное действие совершить невозможно, так как массив пустой.");
                        }
                        break;
                    case 7:
                        Console.WriteLine("Вы выбрали: выполнить сортировку массива (методом простого обмена)");
                        if (!isArrayEmpty && arrayLength != 0)
                        {
                            basicArray = SortOutArraySelection(basicArray);
                        }
                        else
                        {
                            Console.WriteLine("Выбранное действие совершить невозможно, так как массив пустой.");
                        }
                        break;
                    case 8:
                        Console.WriteLine("Вы выбрали: выполнить поиск элемента, который вводит пользователь с клавиатуры, в отсортированном массиве (бинарный поиск) и подсчитать количество сравнений, необходимых для поиска нужного элемента");
                        if (!isArrayEmpty && arrayLength != 0)
                        {
                            FindArrayElement(basicArray);
                        }
                        else
                        {
                            Console.WriteLine("Выбранное действие совершить невозможно, так как массив пустой.");
                        }
                        break;
                    case 9:
                        Console.WriteLine("Вы выбрали: выполнить сортировку массива (методом простого обмена)");
                        if (!isArrayEmpty && arrayLength != 0)
                        {
                            basicArray = SortOutArrayShaker(basicArray);
                        }
                        else
                        {
                            Console.WriteLine("Выбранное действие совершить невозможно, так как массив пустой.");
                        }
                        break;
                    default:
                        Console.WriteLine("Вы выбрали: завершить работу");
                        break;
                }
            } while (menuPoint!=10);
            #endregion
        }
    }
}

