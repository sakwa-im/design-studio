using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace configuration
{
    #region Domain & Constraint implementations
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <history>
    /// 16-02-2012  Michel Roovers          Added
    /// </history>
    public class Domain<T>
    {
        public bool IsValid(T t) { return (_Constraint != null) ? _Constraint.IsValid(t) : false; }
        public T Value
        {
            get { return _Value; }
            set { if (_Constraint != null && _Constraint.IsValid(value)) _Value = value; }
        }

        public Constraint<T> Constraint { get { return _Constraint; } set { _Constraint = value; } }

        private Constraint<T> _Constraint = null;
        private T _Value = default(T);

    }

    public enum ConstraintTest { None, Any, MinMax, InRange, ValidNumber, 
        ValidPath, ValidPathFileName, ValidURL, ValidEmail }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <history>
    /// 09-02-2011  Michel Roovers          Added
    /// </history>
    public class Constraint<T>
    {
        public Constraint(ConstraintTest constraintTest)
        {
            _ConstraintTest = constraintTest;
        }
        public Constraint(T min, T max)
        {
            _Min = min;
            _Max = max;
            _ConstraintTest = ConstraintTest.MinMax;
        }
        public Constraint(T[] range)
        {
            _Range = range;
            _ConstraintTest = ConstraintTest.InRange;
        }
        /// <summary>
        /// Determines whether the specified t is valid.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>
        ///   <c>true</c> if the specified t is valid; otherwise, <c>false</c>.
        /// </returns>
        /// <history>
        /// 18-5-2011	m.roovers			Added allowing empty value for ValidPathFileName, ValidURL and ValidEmail
        /// </history>
        public bool IsValid(T t)
        {
            bool result = false;
            Comparer<T> comparer = Comparer<T>.Default;

            switch (_ConstraintTest)
            {
                case ConstraintTest.Any:
                    result = true;
                    break;

                case ConstraintTest.MinMax:
                    result = comparer.Compare(_Min, t) <= 0 && comparer.Compare(_Max, t) >= 0;
                    break;

                case ConstraintTest.InRange:
                    if (_Range != null)
                        foreach (T elm in _Range)
                            if (comparer.Compare(elm, t) == 0)
                            {
                                result = true;
                                break;

                            } //if (comparer.Compare(elm, t) == 0)
                    break;

                case ConstraintTest.ValidNumber:
                    double test = 0.0;
                    result = double.TryParse(ToString(), out test);
                    break;

                case ConstraintTest.ValidPath:
                    string pathName = t.ToString();
                    result = pathName != "" ? Directory.Exists(pathName) : true;
                    break;

                case ConstraintTest.ValidPathFileName:
                    string pathFileName = t.ToString();
                    result = pathFileName != "" ? File.Exists(pathFileName) : true;
                    break;

                case ConstraintTest.ValidURL:
                    string url = t.ToString().ToLower();
                    if (url.StartsWith("http") || url.StartsWith("ftp"))
                        result = Regex.IsMatch(t.ToString(), validURLPattern);

                    if (url == "")
                        result = true;

                    break;

                case ConstraintTest.ValidEmail:
                    string email = t.ToString();
                    result = email != "" ? Regex.IsMatch(email, validEmailPattern) : true;
                    break;

            } //switch(_ConstraintTest)

            return result;

        } //public bool IsValid(T t)

        private const string validURLPattern =
            "(([a-zA-Z][0-9a-zA-Z+\\-\\.]*:)?/{0,2}[0-9a-zA-Z;/?:@&=+$\\.\\-_!~*'()%]+)?(#[0-9a-zA-Z;/?:@&=+$\\.\\-_!~*'()%]+)?";

        private const string validEmailPattern =
            @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$";


        public T Min { get { return _Min; } set { _Min = value; } }
        public T Max { get { return _Max; } set { _Max = value; } }
        public T[] Range { get { return _Range; } set { _Range = value; } }

        new public string ToString()
        {
            string result = "";
            switch (_ConstraintTest)
            {
                case ConstraintTest.MinMax:
                    result = "[" + _Min.ToString() + "," + _Max.ToString() + "]";
                    break;

                case ConstraintTest.InRange:
                    if (_Range != null)
                    {
                        foreach (T elm in _Range)
                            result += (result != "") ? "," + elm.ToString() : elm.ToString();

                    } //if (_Range != null)

                    result = "{" + result + "}";

                    break;

                default:
                    result = _ConstraintTest.ToString();
                    break;

            } //switch (_ConstraintTest)

            return result;

        } //new public string ToString()
        public ConstraintTest ConstraintType { get { return _ConstraintTest; } }

        private T _Min = default(T);
        private T _Max = default(T);
        private T[] _Range = null;
        private ConstraintTest _ConstraintTest = ConstraintTest.None;

    }
    #endregion
}
