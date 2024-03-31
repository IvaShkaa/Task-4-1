using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Задание_4_1
{
    public class OneD_Array<T> : IEnumerable
        where T : IComparable<T>
    {
        private T[] _array;
        int capacity = 7;
        int size;

        public OneD_Array(int length = 0, Func<T> valueprovider = null)
        {
            if (valueprovider == null && length > 0)
            {
                throw new ArgumentNullException(nameof(valueprovider));
            }
            while (capacity < length)
            {
                capacity = capacity * 2 + 1;
            }
            _array = new T[capacity];
            size = length;
            for (int i = 0; i < size; i++)
            {
                _array[i] = valueprovider();
            }
        }

        public T this[int index]
        {
            get
            {
                if (index > size)
                {
                    throw new IndexOutOfRangeException(nameof(index));
                }
                return _array[index];
            }

            set
            {
                if (index > size)
                {
                    throw new IndexOutOfRangeException(nameof(index));
                }
                _array[index] = value;
            }
        }

        public void AddValue(T value)
        {
            if (capacity <= size)
            {
                capacity = capacity * 2 + 1;
                T[] new_arr = new T[capacity];
                _array.CopyTo(new_arr, 0);
                _array = new_arr;
            }
            _array[size] = value;
            size++;
        }

        public void DelValue()
        {
            size--;
            if (size * 2 + 1 < capacity)
            {
                capacity = (capacity - 1) / 2;
                Array.Resize(ref _array, capacity);
            }
        }

        public void SortArray()
        {
            Array.Sort( _array, 0, size);
        }

        public int Len()
        {
            return _array.Length;
        }

        public int CountValues(Func<T, bool> func)
        {
            int counter = 0;
            for (int i = 0; i < size; i++)
            {
                if (func(_array[i]))
                {
                    counter++;
                }
            }
            return counter;
        }

        public bool IsThereSuitableValue(Func<T, bool> func)
        {
            for (int i = 0; i < size; i++)
            {
                if (func(_array[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public bool DoesAllValuesAreSuitable(Func<T, bool> func)
        {
            for (int i = 0; i < size; i++)
            {
                if (!func(_array[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public bool Contain(T value)
        {
            for (int i = 0; i < size; i++)
            {
                if (value.CompareTo(_array[i]) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void SuitableValue(Func<T, bool> func)
        {
            for (int i = 0; i < size; i++)
            {
                if (func(_array[i]))
                {
                    Console.WriteLine(_array[i]);
                    return;
                }
            }
            Console.WriteLine("Ни одно значение в массиве не подходит под ваше условие!!!");
            return;
        }

        public void DoSomthingWithAllValues(Action<T> something)
        {
            for (int i = 0; i < size; i++)
            {
                something(_array[i]);
            }
        }

        public T[] GetSuitableValues(Func<T, bool> func)
        {
            int new_arr_len = 0;
            for (int i = 0; i < size; i++)
            {
                if (func(_array[i]))
                {
                    new_arr_len++;
                }
            }
            T[] new_arr = new T[new_arr_len];
            int j = 0;
            for (int i = 0; i < size; i++)
            {
                if (func(_array[i]))
                {
                    j++;
                    new_arr[j] = _array[i];
                }
            }
            return new_arr;
        }

        public T[] ReverseArray()
        {
            T[] new_arr = new T[size];
            int j = 0;
            for (int i = size - 1; i >= 0; i--)
            {
                new_arr[j] = _array[i];
                j++;
            }
            return new_arr;
        }

        public T MinVal()
        {
            T min = _array[0];
            for (int i = 0; i < size; i++)
            {
                if (min.CompareTo(_array[i]) > 0)
                {
                    min = _array[i];
                }
            }
            return min;
        }

        public T MaxVal()
        {
            T max = _array[0];
            for (int i = 0; i < size; i++)
            {
                if (max.CompareTo(_array[i]) < 0)
                {
                    max = _array[i];
                }
            }
            return max;
        }

        public Tproj ProectionMaxVal<Tproj>(Func<T, Tproj> func)
            where Tproj : IComparable<Tproj>
        {
            Tproj max = func(_array[0]);
            for (int i = 0; i < size; i++)
            {
                if (max.CompareTo(func(_array[i])) < 0)
                {
                    max = func(_array[i]);
                }
            }
            return max;
        }

        public Tproj ProectionMinVal<Tproj>(Func<T, Tproj> func)
            where Tproj : IComparable<Tproj>
        {
            Tproj min = func(_array[0]);
            for (int i = 0; i < size; i++)
            {
                if (min.CompareTo(func(_array[i])) > 0)
                {
                    min = func(_array[i]);
                }
            }
            return min;
        }

        public OneD_Array<TPro> Project<TPro>(Func<T, TPro> selector)
            where TPro : IComparable<TPro>
        {
            int i = -1;
            TPro GetProjectValue()
            {
                i++;
                return selector(this[i]);
            }
            return new OneD_Array<TPro>(size, GetProjectValue);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public OneDEnum<T> GetEnumerator()
        {
            return new OneDEnum<T>(this);
        }

        public class OneDEnum<Tenum> : IEnumerator
            where Tenum : IComparable<Tenum>
        {
            public OneD_Array<Tenum> _arr = new OneD_Array<Tenum>();

            int position = -1;

            public OneDEnum(OneD_Array<Tenum> arr)
            {
                _arr = arr;
            }

            public bool MoveNext()
            {
                position++;
                return (position < _arr.Len());
            }

            public void Reset()
            {
                position = -1;
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public Tenum Current
            {
                get
                {
                    try
                    {
                        return _arr[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }

    }
}
