/********************************************************************************
 * Module      : Lapis.Math.Measurement
 * Class       : PhysicalConstants
 * Description : Provides physical constants.
 * Created     : 2015/5/22
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lapis.Math.Measurement
{
    /// <summary>
    /// Provides physical constants.
    /// </summary>
    public static class PhysicalConstants
    {
        /// <summary>
        /// Gets the <see cref="Quantity"/> that represents the speed of light in vacuum (c).
        /// </summary>
        /// <value>The <see cref="Quantity"/> that represents the speed of light in vacuum (c), that is, 2.99792458e8 m/s.</value>
        public static Quantity c
        {
            get
            {
                if (_c == null)
                    lock (syncRoot)
                        if (_c == null)
                            _c = 2.99792458e8.Unit(SI.m / SI.s);
                return _c;
            }
        }
        private static Quantity _c;

        /// <summary>
        /// Gets the <see cref="Quantity"/> that represents the gravitational constant (G).
        /// </summary>
        /// <value>The <see cref="Quantity"/> that represents the gravitational constant (G), that is, 6.67384e-11 m^3*kg^-1*s^-2.</value>
        public static Quantity G
        {
            get
            {
                if (_G == null)
                    lock (syncRoot)
                        if (_G == null)
                            _G = 6.67384e-11.Unit(Unit.Pow(SI.m, 3) / SI.kg / Unit.Pow(SI.s, 2));
                return _G;
            }
        }
        private static Quantity _G;

        /// <summary>
        /// Gets the <see cref="Quantity"/> that represents the elementary charge (e).
        /// </summary>
        /// <value>The <see cref="Quantity"/> that represents the elementary charge (e), that is, 1.602176565e-19 C.</value>
        public static Quantity e
        {
            get
            {
                if (_e == null)
                    lock (syncRoot)
                        if (_e == null)
                            _e = 1.602176565e-19.Unit(SI.C);
                return _e;
            }
        }
        private static Quantity _e;

        /// <summary>
        /// Gets the <see cref="Quantity"/> that represents the Planck constant (h).
        /// </summary>
        /// <value>The <see cref="Quantity"/> that represents the Planck constant (h), that is, 6.62606957e-34 J*s.</value>
        public static Quantity h
        {
            get
            {
                if (_h == null)
                    lock (syncRoot)
                        if (_h == null)
                            _h = 6.62606957e-34.Unit(SI.J * SI.s);
                return _h;
            }
        }
        private static Quantity _h;

        /// <summary>
        /// Gets the <see cref="Quantity"/> that represents the Avogadro constant (NA).
        /// </summary>
        /// <value>The <see cref="Quantity"/> that represents the Avogadro constant (NA), that is, 6.02214129e23 mol^-1.</value>
        public static Quantity NA
        {
            get
            {
                if (_NA == null)
                    lock (syncRoot)
                        if (_NA == null)
                            _NA = 6.02214129e23.Unit(Unit.None / SI.mol);
                return _NA;
            }
        }
        private static Quantity _NA;

        /// <summary>
        /// Gets the <see cref="Quantity"/> that represents the ideal gas constant (R).
        /// </summary>
        /// <value>The <see cref="Quantity"/> that represents the ideal gas constant (R), that is, 8.3144621 J/(mol*K).</value>
        public static Quantity R
        {
            get
            {
                if (_R == null)
                    lock (syncRoot)
                        if (_R == null)
                            _R = 8.3144621.Unit(SI.J / SI.mol / SI.K);
                return _R;
            }
        }
        private static Quantity _R;

        /// <summary>
        /// Gets the <see cref="Quantity"/> that represents the Boltzmann constant (kB).
        /// </summary>
        /// <value>The <see cref="Quantity"/> that represents the Boltzmann constant (kB), that is, 1.3806488e-23 J/K.</value>
        public static Quantity kB
        {
            get
            {
                if (_kB == null)
                    lock (syncRoot)
                        if (_kB == null)
                            _kB = 1.3806488e-23.Unit(SI.J / SI.K);
                return _kB;
            }
        }
        private static Quantity _kB;

        /// <summary>
        /// Gets the <see cref="Quantity"/> that represents the vacuum permeability (μ0).
        /// </summary>
        /// <value>The <see cref="Quantity"/> that represents the vacuum permeability (μ0), that is, 4*π*1e-7 N/A^2.</value>
        public static Quantity mu0
        {
            get
            {
                if (_mu0 == null)
                    lock (syncRoot)
                        if (_mu0 == null)
                            _mu0 = (4 * System.Math.PI * 1e-7).Unit(SI.N / SI.A / SI.A);
                return _mu0;
            }
        }
        private static Quantity _mu0;

        /// <summary>
        /// Gets the <see cref="Quantity"/> that represents the vacuum permittivity (ε0).
        /// </summary>
        /// <value>The <see cref="Quantity"/> that represents the vacuum permittivity (ε0), that is, 8.85418781762e-12 F/m.</value>
        public static Quantity epsilon0
        {
            get
            {
                if (_epsilon0 == null)
                    lock (syncRoot)
                        if (_epsilon0 == null)
                            _epsilon0 = 8.85418781762e-12.Unit(SI.F / SI.m);
                return _epsilon0;
            }
        }
        private static Quantity _epsilon0;


        private static readonly object syncRoot = new object();
    }
}
