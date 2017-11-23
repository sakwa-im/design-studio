using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;

namespace kms
{
    public class KeyUtils
    {
        public static byte[] GetRandomBytes(int length)
        {
            byte[] result = SecureRandom.GetSeed(length);
            return result;

        }

        public static byte[] GetBytes(string input)
        {
            return Hex.Decode(input);
        }

        public static string GetString(byte[] input)
        {
            return Hex.ToHexString(input).ToUpper();
        }

        public static byte[] CombineByteArrays(byte[] array1, params byte[][] arrays)
        {
            int length = array1.Length;
            if (arrays != null)
                foreach (byte[] ba in arrays)
                    if(ba != null)
                        length += ba.Length;

            byte[] result = new byte[length];

            System.Array.Copy(array1, result, array1.Length);
            int ofs = array1.Length;

            foreach (byte[] ba in arrays)
                if(ba != null)
                {
                    System.Array.Copy(ba, 0, result, ofs, ba.Length);
                    ofs += ba.Length;

                } //foreach (byte[] ba in arrays)

            return result;

        } //public static byte[] CombineByteArrays(byte[] array1, params byte[][] arrays)
        public static byte[] MergeByteArrays(byte[] array1, params byte[][] arrays)
        {
            int length = array1.Length;
            if (arrays != null)
                foreach (byte[] ba in arrays)
                    length += ba.Length;

            byte[] result = new byte[length];

            System.Array.Copy(array1, result, array1.Length);
            int ofs = array1.Length;

            foreach (byte[] ba in arrays)
            {
                System.Array.Copy(ba, 0, result, ofs, ba.Length);
                ofs += ba.Length;

            } //foreach (byte[] ba in arrays)

            return result;

        } //public static byte[] CombineByteArrays(byte[] array1, params byte[][] arrays)

        public static byte[] PadByteArray(byte[] input, byte pad = 0x80, byte padLength = 16)
        {
            byte[] result = null;

            int length = input.Length;
            switch (pad)
            {
                case 0x80:
                    result = (length + 1) % padLength == 0 ? new byte[length + 1] : new byte[(length / padLength + 1) * padLength];

                    System.Array.Copy(input, result, length);
                    result[length] = pad;

                break;

                case 0x00:
                    result = length % padLength == 0 ? new byte[length] : new byte[(length / padLength + 1) * padLength];
                    System.Array.Copy(input, result, length);
                break;

                default:
                break;

            }

            return result;

        } //public static byte[] PadByteArray(
        public static byte[] TrimByteArray(byte[] input, int length = 8)
        {
            byte[] result = new byte[length];

            System.Array.Copy(input, 0, result, 0, Math.Min(length, input.Length));

            return result;

        } //public static byte[] trimByteArray(byte[] input, int length)

        public static byte[] AddTLV(byte[] input)
        {
            if(input.Length == 0)
                return new byte[] {0x00};

            byte[] length = null;
            if(input.Length < 128)
                length = new byte[] {(byte)input.Length};
            else
            if(input.Length < 256)
                length = new byte[] {(byte)0x81, (byte)input.Length};
            else
                length = new byte[] { (byte)0x82, (byte)(input.Length >> 8), (byte)(input.Length & 0xFF) };

            if (length == null)
                return input;

            byte[] result = new byte[length.Length + input.Length];

            System.Array.Copy(length, 0, result, 0, length.Length);
            System.Array.Copy(input, 0, result, length.Length, input.Length);
            
            return result;

        } //public static byte[] AddTLV(byte[] input)

        public static string ToOddParity(string input)
        {
            return Hex.ToHexString(ToOddParity(Hex.Decode(input))).ToUpper();
        }
        public static byte[] ToOddParity(byte[] input)
        {
            byte[] result = new byte[input.Length];

            for (int i = 0; i < result.Length; ++i)
                result[i] = ToOddParity(input[i]);

            return result;

        }
        public static byte ToOddParity(byte b)
        {
            int parity = 0;
            byte test = b;
            for (int i = 0; i < 8; ++i)
            {
                byte testBit = (byte)(test & 0x80);
                if (testBit == (byte)0x80)
                {
                    ++parity;
                }
                test <<= 1;
            }

            if (parity % 2 == 0)
                return (b % 2 == 0) 
                    ? (byte)(b | 0x01)
                    :(byte)(b & 0xfe);

            return b;

        }
        public static byte ToEvenParity(byte b)
        {
            int parity = 0;
            byte test = b;
            for (int i = 0; i < 8; ++i)
            {
                byte testBit = (byte)(test & 0x80);
                if (testBit == (byte)0x80)
                {
                    ++parity;
                }
                test <<= 1;
            }

            if (parity % 2 != 0)
                return (b % 2 == 0) 
                    ? (byte)(b | 0x01)
                    :(byte)(b & 0xfe);

            return b;

        }
        public static byte[]  ClearParity(byte[] input)
        {
            byte[] result = new byte[input.Length];

            for (int i = 0; i < result.Length; i++)
                result[i] = Convert.ToByte(input[i] & 0xFE);

            return result;

        }
        /// <summary>
        /// 0x00, 0x01, 0x02, 0x03, 0x04 => 0x01, 0x02, 0x03, 0x04, 0x00
        /// </summary>
        /// <param name="input"></param>
        /// <param name="shiftBits"></param>
        /// <returns></returns>
        public static byte[] RotateLeft(byte[] input, int shiftBits = 8)
        {

            byte[] result = new byte[input.Length];

            System.Array.Copy(input, 1, result, 0, result.Length - 1);
            result[result.Length - 1] = input[0];

            return result;
        }

        /// <summary>
        /// 0x00, 0x01, 0x02, 0x03, 0x04 =>  0x04, 0x00, 0x01, 0x02, 0x03,
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="shiftBits"></param>
        /// <returns></returns>
        public static byte[] RotateRight(byte[] input, int shiftBits = 8)
        {
            byte[] result = new byte[input.Length];
            
            System.Array.Copy(input, 0, result, 1, result.Length - 1);
            result[0] = input[result.Length - 1];

            return result;
        }
        public static byte[] ShiftLeft(byte[] input, int shiftBits = 1)
        {
            byte[] result = new byte[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                result[i] = Convert.ToByte((input[i] & 0x7F) * 2);
                if (i < input.Length - 1 && (input[i + 1] & 0x80) == 0x80)
                    result[i] |= 0x01;

            }
            return result;

        }
        public static byte[] ShiftRight(byte[] input, int shiftBits = 1)
        {
            byte[] result = new byte[input.Length];

            for (int i = input.Length - 1; i >= 0; i--)
            {
                result[i] = Convert.ToByte(input[i] / 2);
                if (i > 0 && (input[i] & 0x01) == 0x01)
                    result[i] |= 0x80;

            }
            return result;
        }

        public static byte[] xor(byte[] input1, byte[] input2)
        {
            if (input1.Length == input2.Length && input1.Length > 0)
            {
                byte[] result = new byte[input1.Length];
                System.Array.Copy(input1, 0, result, 0, result.Length);
                byte[] inputCopy = new byte[input1.Length];
                System.Array.Copy(input2, 0, inputCopy, 0, inputCopy.Length);

                for(int i = 0; i < result.Length; i++)
                    result[i] ^= inputCopy[i];

                return result;

            }
            
            return null;

        }
        public static bool CompareByteArrays(byte[] input1, byte[] input2)
        {
            if (input1 != null && input2 != null)
            {
                if (input1.Length == input2.Length)
                {
                    for (int i = 0; i < input1.Length; i++)
                        if (input1[i] != input2[i])
                            return false;

                    return true;

                }
            }

            return false;

        }
        public static bool CompareByteArrays(byte[] input1, int offset1, byte[] input2, int offset2, int length)
        {
            if (input1 != null && input2 != null)
            {
                if ((input1.Length - offset1 >= length) && (input2.Length - offset2 >= length))
                {
                    for (int i = 0; i < length; i++)
                        if (input1[i + offset1] != input2[i + offset2])
                            return false;

                    return true;

                }
            }

            return false;

        }
        public static byte[] SwapArray(byte[] input)
        {
            byte[] result = new byte[input.Length];

            for (int i = 0; i < input.Length; i++)
                result[(result.Length - 1) - i] = input[i];

                return result;

        }
        public static byte[] desfirePadByteArray(byte[] input, byte pad = 0x80, byte padLength = 16)
        {
            byte[] result = null;

            int length = input.Length;

            result = length % padLength == 0 ? new byte[length] : new byte[(length / padLength + 1) * padLength];
            System.Array.Copy(input, result, length);

            if (result.Length > input.Length)
                result[input.Length] = pad;

            return result;

        } //public static byte[] PadByteArray(
        public static byte[] desfireCRC16(byte[] input)
        {
            return BitConverter.GetBytes(CRC16.compute_fcs(input, 0x6363));
        }
        public static byte[] desfireCRC32(byte[] input)
        {
            byte[] result = new CRC32().ComputeHash(input);
            for (int i = 0; i < result.Length; i++)
                result[i] ^= 0xFF;

            return result;

        }

    } //public class KeyUtils
 }
