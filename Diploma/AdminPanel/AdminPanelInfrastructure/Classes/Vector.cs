namespace AdminPanelInfrastructure.Classes
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.IO;
    using System.Globalization;
    /// <summary>
    /// Класс реализующий вектора и операции над ними
    /// </summary>
    [Serializable]
    public class Vector
    {
   
        #region Поля и свойства
        double[] _vector;
        int _n;


        /// <summary>
        /// Массив типа double содержащий отсчеты вектора
        /// </summary>
        public Double[] DataInVector
        {
            get { return _vector; }
            set
            {
                _vector = value;
                _n = _vector.Length;
            }
        }


        /// <summary>
        /// Размерность вектора
        /// </summary>
        public Int32 N
        {
            get { return _n; }
        }

        /// <summary>
        /// Доступ по индексу
        /// </summary>
        /// <param name="i">Индекс</param>
        /// <returns>Значение вектора</returns>
        public double this[int i]
        {
            get { return _vector[i]; }
            set { _vector[i] = value; }
        }



        #endregion

        #region Конструкторы
        /// <summary>
        /// Создает вектор с нулями размерности 3
        /// </summary>
        public Vector()
        {
            _vector = new double[3];
            _n = 3;
        }


        /// <summary>
        /// Создает вектор размерности 1, со значением a
        /// </summary>
        /// <param name="a">Значение нулевой ячейки</param>
        public Vector(double a)
        {
            _vector = new double[1];
            _vector[0] = a;
            _n = 1;
        }



        /// <summary>
        /// Создает вектор с нулями размерности n
        /// </summary>
        public Vector(int n)
        {
            _vector = new double[n];
            _n = n;
        }



        /// <summary>
        /// Создает вектор на основе массива
        /// </summary>
        public Vector(double[] vector)
        {
            _vector = vector;
            _n = _vector.Length;
        }

        /// <summary>
		/// Создает вектор на основе массива
		/// </summary>
        public Vector(float[] vector)
        {
            _vector = new double[vector.Length];
            _n = vector.Length;

            for (int i = 0; i < _n; i++)
            {
                _vector[i] = (double)vector[i];
            }
        }

        public static List<Double> DirectTransform(List<Double> SourceList)
        {
            if (SourceList.Count == 1)
                return SourceList;

            List<Double> RetVal = new List<Double>();
            List<Double> TmpArr = new List<Double>();

            for (int j = 0; j < SourceList.Count - 1; j += 2)
            {
                RetVal.Add((SourceList[j] - SourceList[j + 1]) / 2.0);
                TmpArr.Add((SourceList[j] + SourceList[j + 1]) / 2.0);
            }

            RetVal.AddRange(DirectTransform(TmpArr));

            return RetVal;
        }

        #endregion

        #region Функции

        /// <summary>
		/// Следующая степень числа 2
		/// </summary>
		/// <param name="n">входное число</param>
		/// <returns></returns>
		public static int NextPow2(int n)
        {
            int pow = 0;

            for (int i = 1; i < 40; i++)
            {
                pow = (int)Math.Pow(2, i);
                if (n <= pow) return pow;
            }

            return -1;
        }

        /// <summary>
        /// Дополнение нулями или обрезание до нужного размера 
        /// вектора.
        /// </summary>
        /// <param name="n">Новый размер</param>
        public Vector CutAndZero(int n)
        {
            double[] newVect = new double[n];

            if (n > _n)
            {
                for (int i = 0; i < _n; i++)
                    newVect[i] = _vector[i];
                for (int i = _n; i < n; i++)
                    newVect[i] = 0;
            }
            else
            {
                for (int i = 0; i < n; i++)
                    newVect[i] = _vector[i];
            }

            return new Vector(newVect);
        }

        #endregion
    }

}

