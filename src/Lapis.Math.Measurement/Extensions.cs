/********************************************************************************
 * Module      : Lapis.Math.Measurement
 * Class       : Extensions
 * Description : Provides extension methods for Quantity.
 * Created     : 2015/5/22
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lapis.Math.Algebra.Expressions;

namespace Lapis.Math.Measurement
{
    /// <summary>
    /// Provides extension methods for <see cref="Quantity"/>.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Creates a <see cref="Quantity"/> object using the specified value and unit.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="unit">The unit.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> with the unit <paramref name="unit"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Quantity Unit(this Expression value, Unit unit)
        {
            return Quantity.Create(value, unit);
        }

        /// <summary>
        /// Creates a <see cref="Quantity"/> object using the specified value and unit.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="unit">The unit.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> with the unit <paramref name="unit"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Quantity Unit(this int value, Unit unit)
        {
            return Quantity.Create(value, unit);
        }

        /// <summary>
        /// Creates a <see cref="Quantity"/> object using the specified value and unit.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="unit">The unit.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> with the unit <paramref name="unit"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Quantity Unit(this double value, Unit unit)
        {
            return Quantity.Create(value, unit);
        }

        #region SI

        /// <summary>
        /// Returns a <see cref="Quantity"/> object using the specified value and the length unit metre (m).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> m.</returns>
        public static Quantity m(this double value)
        {
            return Quantity.Create(value, SI.m);
        }
        /// <summary>
        /// Returns a <see cref="Quantity"/> object using the specified value and the length unit centimetre (cm).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> cm.</returns>
        public static Quantity cm(this double value)
        {
            return Quantity.Create(value, SI.cm);
        }
        /// <summary>
        /// Returns a <see cref="Quantity"/> object using the specified value and the length unit millimetre (mm).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> mm.</returns>
        public static Quantity mm(this double value)
        {
            return Quantity.Create(value, SI.mm);
        }
        /// <summary>
        /// Returns a <see cref="Quantity"/> object using the specified value and the length unit kilometre (km).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> km.</returns>
        public static Quantity km(this double value)
        {
            return Quantity.Create(value, SI.km);
        }
        /// <summary>
        /// Returns a <see cref="Quantity"/> object using the specified value and the mass unit gram (g).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> g.</returns>
        public static Quantity g(this double value)
        {
            return Quantity.Create(value, SI.g);
        }
        /// <summary>
        /// Returns a <see cref="Quantity"/> object using the specified value and the mass unit milligram (mg).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> mg.</returns>
        public static Quantity mg(this double value)
        {
            return Quantity.Create(value, SI.mg);
        }
        /// <summary>
        /// Returns a <see cref="Quantity"/> object using the specified value and the mass unit kilogram (kg).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> kg.</returns>
        public static Quantity kg(this double value)
        {
            return Quantity.Create(value, SI.kg);
        }
        /// <summary>
        /// Returns a <see cref="Quantity"/> object using the specified value and the time unit second (s).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> s.</returns>
        public static Quantity s(this double value)
        {
            return Quantity.Create(value, SI.s);
        }
        /// <summary>
        /// Returns a <see cref="Quantity"/> object using the specified value and the time unit minute (min).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> min.</returns> 
        public static Quantity min(this double value)
        {
            return Quantity.Create(value, SI.min);
        }
        /// <summary>
        /// Returns a <see cref="Quantity"/> object using the specified value and the time unit hour (h).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> h.</returns> 
        public static Quantity h(this double value)
        {
            return Quantity.Create(value, SI.h);
        }
        /// <summary>
        /// Returns a <see cref="Quantity"/> object using the specified value and the volume unit litre (L).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> L.</returns> 
        public static Quantity L(this double value)
        {
            return Quantity.Create(value, SI.L);
        }
        /// <summary>
        /// Returns a <see cref="Quantity"/> object using the specified value and the volume unit millilitre (mL).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> mL.</returns> 
        public static Quantity mL(this double value)
        {
            return Quantity.Create(value, SI.mL);
        }
        /// <summary>
        /// Returns a <see cref="Quantity"/> object using the specified value and the frequency unit hertz (Hz).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> Hz.</returns> 
        public static Quantity Hz(this double value)
        {
            return Quantity.Create(value, SI.Hz);
        }
        /// <summary>
        /// Returns a <see cref="Quantity"/> object using the specified value and the force unit newton (N).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> N.</returns> 
        public static Quantity N(this double value)
        {
            return Quantity.Create(value, SI.N);
        }
        /// <summary>
        /// Returns a <see cref="Quantity"/> object using the specified value and the pressure unit pascal(Pa).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> Pa.</returns> 
        public static Quantity Pa(this double value)
        {
            return Quantity.Create(value, SI.Pa);
        }
        /// <summary>
        /// Returns a <see cref="Quantity"/> object using the specified value and the energy unit joule (J).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> J.</returns> 
        public static Quantity J(this double value)
        {
            return Quantity.Create(value, SI.J);
        }
        /// <summary>
        /// Returns a <see cref="Quantity"/> object using the specified value and the energy unit kilojoule (kJ).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> kJ.</returns> 
        public static Quantity kJ(this double value)
        {
            return Quantity.Create(value, SI.kJ);
        }
        /// <summary>
        /// Returns a <see cref="Quantity"/> object using the specified value and the power unit watt (W).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> W.</returns> 
        public static Quantity W(this double value)
        {
            return Quantity.Create(value, SI.W);
        }
        /// <summary>
        /// Returns a <see cref="Quantity"/> object using the specified value and the power unit kilowatt (kW).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> kW.</returns> 
        public static Quantity kW(this double value)
        {
            return Quantity.Create(value, SI.kW);
        }
        /// <summary>
        /// Returns a <see cref="Quantity"/> object using the specified value and the electric current unit ampere (A).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> A.</returns> 
        public static Quantity A(this double value)
        {
            return Quantity.Create(value, SI.A);
        }
        /// <summary>
        /// Returns a <see cref="Quantity"/> object using the specified value and the electric charge unit coulomb (C).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> C.</returns> 
        public static Quantity C(this double value)
        {
            return Quantity.Create(value, SI.C);
        }
        /// <summary>
        /// Returns a <see cref="Quantity"/> object using the specified value and the voltage unit volt (V).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> V.</returns> 
        public static Quantity V(this double value)
        {
            return Quantity.Create(value, SI.V);
        }
        /// <summary>
        /// Returns a <see cref="Quantity"/> object using the specified value and the electric capacity unit farad (F).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> F.</returns> 
        public static Quantity F(this double value)
        {
            return Quantity.Create(value, SI.F);
        }
        /// <summary>
        /// Returns a <see cref="Quantity"/> object using the specified value and the electrical resistance unit ohm (Ω).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> Ω.</returns> 
        public static Quantity Ohm(this double value)
        {
            return Quantity.Create(value, SI.Ohm);
        }
        /// <summary>
        /// Returns a <see cref="Quantity"/> object using the specified value and the magnetic field unit tesla (T).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> T.</returns> 
        public static Quantity T(this double value)
        {
            return Quantity.Create(value, SI.T);
        }
        /// <summary>
        /// Returns a <see cref="Quantity"/> object using the specified value and the magnetic flux unit weber (Wb).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> Wb.</returns> 
        public static Quantity Wb(this double value)
        {
            return Quantity.Create(value, SI.Wb);
        }
        #endregion
    }
}
