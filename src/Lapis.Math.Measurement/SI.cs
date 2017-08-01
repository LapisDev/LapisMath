/********************************************************************************
 * Module      : Lapis.Math.Measurement
 * Class       : SI
 * Description : Provides units in the International System of Units.
 * Created     : 2015/5/22
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lapis.Math.Measurement
{
    /// <summary>
    /// Provides units in the International System of Units.
    /// </summary>
    public static class SI
    {      
        #region Base

        /// <summary>
        /// Gets a <see cref="Unit"/> object that represents the length unit metre (m).
        /// </summary>
        /// <value>The <see cref="Unit"/> object that represents the length unit metre (m).</value>
        public static Unit m
        {
            get
            {
                if (_m == null)
                    lock (syncRoot)
                        if (_m == null)
                            _m = Unit.Base("m");
                return _m;
            }
        }
        private static Unit _m;

        /// <summary>
        /// Gets a <see cref="Unit"/> object that represents the mass unit kilogram (kg).
        /// </summary>
        /// <value>The <see cref="Unit"/> object that represents the mass unit kilogram (kg).</value>
        public static Unit kg
        {
            get
            {
                if (_kg == null)
                    lock (syncRoot)
                        if (_kg == null)
                            _kg = Unit.Base("kg");
                return _kg;
            }
        }
        private static Unit _kg;

        /// <summary>
        /// Gets a <see cref="Unit"/> object that represents the time unit second (s).
        /// </summary>
        /// <value>The <see cref="Unit"/> object that represents the time unit second (s).</value>
        public static Unit s
        {
            get
            {
                if (_s == null)
                    lock (syncRoot)
                        if (_s == null)
                            _s = Unit.Base("s");
                return _s;
            }
        }
        private static Unit _s;

        /// <summary>
        /// Gets a <see cref="Unit"/> object that represents the electric current unit ampere (A).
        /// </summary>
        /// <value>The <see cref="Unit"/> object that represents the electric current unit ampere (A).</value>
        public static Unit A
        {
            get
            {
                if (_A == null)
                    lock (syncRoot)
                        if (_A == null)
                            _A = Unit.Base("A");
                return _A;
            }
        }
        private static Unit _A;

        /// <summary>
        /// Gets a <see cref="Unit"/> object that represents the temperature unit kelvin (K).
        /// </summary>
        /// <value>The <see cref="Unit"/> object that represents the temperature unit kelvin (K).</value>
        public static Unit K
        {
            get
            {
                if (_K == null)
                    lock (syncRoot)
                        if (_K == null)
                            _K = Unit.Base("K");
                return _K;
            }
        }
        private static Unit _K;

        /// <summary>
        /// Gets a <see cref="Unit"/> object that represents the unit of amount of substance, mole (mol).
        /// </summary>
        /// <value>The <see cref="Unit"/> object that represents the unit of amount of substance, mole (mol).</value>
        public static Unit mol
        {
            get
            {
                if (_mol == null)
                    lock (syncRoot)
                        if (_mol == null)
                            _mol = Unit.Base("mol");
                return _mol;
            }
        }
        private static Unit _mol;

        /// <summary>
        /// Gets a <see cref="Unit"/> object that represents the unit of luminous intensity, candela (cd).
        /// </summary>
        /// <value>The <see cref="Unit"/> object that represents the unit of luminous intensity, candela (cd).</value>
        public static Unit cd
        {
            get
            {
                if (_cd == null)
                    lock (syncRoot)
                        if (_cd == null)
                            _cd = Unit.Base("cd");
                return _cd;
            }
        }
        private static Unit _cd;

        #endregion

      
        #region Length

        /// <summary>
        /// Gets a <see cref="Unit"/> object that represents the length unit centimetre (cm).
        /// </summary>
        /// <value>The <see cref="Unit"/> object that represents the length unit centimetre (cm).</value>
        public static Unit cm
        {
            get
            {
                if (_cm == null)
                    lock (syncRoot)
                        if (_cm == null)
                            _cm = Unit.Create("cm", 0.01 * m);
                return _cm;
            }
        }
        private static Unit _cm;

        /// <summary>
        /// Gets a <see cref="Unit"/> object that represents the length unit millimetre (mm).
        /// </summary>
        /// <value>The <see cref="Unit"/> object that represents the length unit millimetre (mm).</value>
        public static Unit mm
        {
            get
            {
                if (_mm == null)
                    lock (syncRoot)
                        if (_mm == null)
                            _mm = Unit.Create("mm", 0.001 * m);
                return _mm;
            }
        }
        private static Unit _mm;

        /// <summary>
        /// Gets a <see cref="Unit"/> object that represents the length unit kilometre (km).
        /// </summary>
        /// <value>The <see cref="Unit"/> object that represents the length unit kilometre (km).</value>
        public static Unit km
        {
            get
            {
                if (_km == null)
                    lock (syncRoot)
                        if (_km == null)
                            _km = Unit.Create("km", 1000 * m);
                return _km;
            }
        }
        private static Unit _km;           

        #endregion

        #region Mass

        /// <summary>
        /// Gets a <see cref="Unit"/> object that represents the mass unit gram (g).
        /// </summary>
        /// <value>The <see cref="Unit"/> object that represents the mass unit gram (g).</value>
        public static Unit g
        {
            get
            {
                if (_g == null)
                    lock (syncRoot)
                        if (_g == null)
                            _g = Unit.Create("g", 0.001 * kg);
                return _g;
            }
        }
        private static Unit _g;

        /// <summary>
        /// Gets a <see cref="Unit"/> object that represents the mass unit milligram (mg).
        /// </summary>
        /// <value>The <see cref="Unit"/> object that represents the mass unit milligram (mg).</value>
        public static Unit mg
        {
            get
            {
                if (_mg == null)
                    lock (syncRoot)
                        if (_mg == null)
                            _mg = Unit.Create("mg", kg / 1000000);
                return _mg;
            }
        }
        private static Unit _mg;        

        #endregion

        #region Time

        /// <summary>
        /// Gets a <see cref="Unit"/> object that represents the time unit minute (min).
        /// </summary>
        /// <value>The <see cref="Unit"/> object that represents the time unit minute (min).</value>
        public static Unit min
        {
            get
            {
                if (_min == null)
                    lock (syncRoot)
                        if (_min == null)
                            _min = Unit.Create("min", 60 * s);
                return _min;
            }
        }
        private static Unit _min;

        /// <summary>
        /// Gets a <see cref="Unit"/> object that represents the time unit hour (h).
        /// </summary>
        /// <value>The <see cref="Unit"/> object that represents the time unit hour (h).</value>
        public static Unit h
        {
            get
            {
                if (_h == null)
                    lock (syncRoot)
                        if (_h == null)
                            _h = Unit.Create("h", 60 * min);
                return _h;
            }
        }
        private static Unit _h;

        #endregion

        #region Volume

        /// <summary>
        /// Gets a <see cref="Unit"/> object that represents the 体积单位升（L）.
        /// </summary>
        /// <value>The <see cref="Unit"/> object that represents the 体积单位升（L）.</value>
        public static Unit L
        {
            get
            {
                if (_L == null)
                    lock (syncRoot)
                        if (_L == null)
                            _L = Unit.Create("L", 0.001 * Unit.Pow(m, 3));
                return _L;
            }
        }
        private static Unit _L;

        /// <summary>
        /// Gets a <see cref="Unit"/> object that represents the volume unit litre (L).
        /// </summary>
        /// <value>The <see cref="Unit"/> object that represents the volume unit millilitre (mL).</value>
        public static Unit mL
        {
            get
            {
                if (_mL == null)
                    lock (syncRoot)
                        if (_mL == null)
                            _mL = Unit.Create("mL", 0.001 * L);
                return _mL;
            }
        }
        private static Unit _mL;

        #endregion

        #region Frequency

        /// <summary>
        /// Gets a <see cref="Unit"/> object that represents the frequency unit hertz (Hz).
        /// </summary>
        /// <value>The <see cref="Unit"/> object that represents the frequency unit hertz (Hz).</value>
        public static Unit Hz
        {
            get
            {
                if (_Hz == null)
                    lock (syncRoot)
                        if (_Hz == null)
                            _Hz = Unit.Create("Hz", Unit.Pow(s, -1));
                return _Hz;
            }
        }
        private static Unit _Hz;       

        #endregion

        #region Force

        /// <summary>
        /// Gets a <see cref="Unit"/> object that represents the force unit newton (N).
        /// </summary>
        /// <value>The <see cref="Unit"/> object that represents the force unit newton (N).</value>
        public static Unit N
        {
            get
            {
                if (_N == null)
                    lock (syncRoot)
                        if (_N == null)
                            _N = Unit.Create("N", kg * m * Unit.Pow(s, -2));
                return _N;
            }
        }
        private static Unit _N;

        #endregion

        #region Pressure

        /// <summary>
        /// Gets a <see cref="Unit"/> object that represents the pressure unit pascal(Pa).
        /// </summary>
        /// <value>The <see cref="Unit"/> object that represents the pressure unit pascal(Pa).</value>
        public static Unit Pa
        {
            get
            {
                if (_Pa == null)
                    lock (syncRoot)
                        if (_Pa == null)
                            _Pa = Unit.Create("Pa", N * Unit.Pow(m, -2));
                return _Pa;
            }
        }
        private static Unit _Pa;

        #endregion

        #region Energy

        /// <summary>
        /// Gets a <see cref="Unit"/> object that represents the energy unit joule (J).
        /// </summary>
        /// <value>The <see cref="Unit"/> object that represents the energy unit joule (J).</value>
        public static Unit J
        {
            get
            {
                if (_J == null)
                    lock (syncRoot)
                        if (_J == null)
                            _J = Unit.Create("J", N * m);
                return _J;
            }
        }
        private static Unit _J;

        /// <summary>
        /// Gets a <see cref="Unit"/> object that represents the energy unit kilojoule (kJ).
        /// </summary>
        /// <value>The <see cref="Unit"/> object that represents the energy unit kilojoule (kJ).</value>
        public static Unit kJ
        {
            get
            {
                if (_kJ == null)
                    lock (syncRoot)
                        if (_kJ == null)
                            _kJ = Unit.Create("kJ", 1000 * J);
                return _kJ;
            }
        }
        private static Unit _kJ;   

        #endregion

        #region Power

        /// <summary>
        /// Gets a <see cref="Unit"/> object that represents the power unit watt (W).
        /// </summary>
        /// <value>The <see cref="Unit"/> object that represents the power unit watt (W).</value>
        public static Unit W
        {
            get
            {
                if (_W == null)
                    lock (syncRoot)
                        if (_W == null)
                            _W = Unit.Create("W", J / s);
                return _W;
            }
        }
        private static Unit _W;

        /// <summary>
        /// Gets a <see cref="Unit"/> object that represents the power unit kilowatt (kW).
        /// </summary>
        /// <value>The <see cref="Unit"/> object that represents the power unit kilowatt (kW).</value>
        public static Unit kW
        {
            get
            {
                if (_kW == null)
                    lock (syncRoot)
                        if (_kW== null)
                            _kW = Unit.Create("kW", 1000 * W);
                return _kW;
            }
        }
        private static Unit _kW;

        #endregion

        #region Charge

        /// <summary>
        /// Gets a <see cref="Unit"/> object that represents the electric charge unit coulomb (C).
        /// </summary>
        /// <value>The <see cref="Unit"/> object that represents the electric charge unit coulomb (C).</value>
        public static Unit C
        {
            get
            {
                if (_C == null)
                    lock (syncRoot)
                        if (_C == null)
                            _C = Unit.Create("C", A * s);
                return _C;
            }
        }
        private static Unit _C;     

        #endregion

        #region Voltage

        /// <summary>
        /// Gets a <see cref="Unit"/> object that represents the voltage unit volt (V).
        /// </summary>
        /// <value>The <see cref="Unit"/> object that represents the voltage unit volt (V).</value>
        public static Unit V
        {
            get
            {
                if (_V == null)
                    lock (syncRoot)
                        if (_V == null)
                            _V = Unit.Create("V", W / A);
                return _V;
            }
        }
        private static Unit _V;      

        #endregion

        #region Capacity

        /// <summary>
        /// Gets a <see cref="Unit"/> object that represents the electric capacity unit farad (F).
        /// </summary>
        /// <value>The <see cref="Unit"/> object that represents the electric capacity unit farad (F).</value>
        public static Unit F
        {
            get
            {
                if (_F == null)
                    lock (syncRoot)
                        if (_F == null)
                            _F = Unit.Create("F", C / V);
                return _F;
            }
        }
        private static Unit _F;

        #endregion

        #region Resistence

        /// <summary>
        /// Gets a <see cref="Unit"/> object that represents the electrical resistance unit ohm (Ω).
        /// </summary>
        /// <value>The <see cref="Unit"/> object that represents the electrical resistance unit ohm (Ω).</value>
        public static Unit Ohm 
        {
            get
            {
                if (_Ohm == null)
                    lock (syncRoot)
                        if (_Ohm == null)
                            _Ohm = Unit.Create("Ohm", V / A);
                return _Ohm;
            }
        }
        private static Unit _Ohm;

        #endregion

        #region Magnetic flux density

        /// <summary>
        /// Gets a <see cref="Unit"/> object that represents the magnetic field unit tesla (T).
        /// </summary>
        /// <value>The <see cref="Unit"/> object that represents the magnetic field unit tesla (T).</value>
        public static Unit T
        {
            get
            {
                if (_T == null)
                    lock (syncRoot)
                        if (_T == null)
                            _T = Unit.Create("T", Wb * Unit.Pow(m, -2));
                return _T;
            }
        }
        private static Unit _T;

        #endregion

        #region Magnetic flux

        /// <summary>
        /// Gets a <see cref="Unit"/> object that represents the magnetic flux unit weber (Wb).
        /// </summary>
        /// <value>The <see cref="Unit"/> object that represents the magnetic flux unit weber (Wb).</value>
        public static Unit Wb
        {
            get
            {
                if (_Wb == null)
                    lock (syncRoot)
                        if (_Wb == null)
                            _Wb = Unit.Create("Wb", V * s);
                return _Wb;
            }
        }
        private static Unit _Wb;

        #endregion

        private static readonly object syncRoot = new object();
    }
}
