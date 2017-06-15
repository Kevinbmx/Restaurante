using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for CodigoControl
/// </summary>
public class CodigoControl
{
    public CodigoControl()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static String generateControlCode(String numeroAutorizacion, String numeroFactura, String nitci,
                         String fechaTransaccion, String montoTransaccion, String llavedosificacion)
    {

        //redondea monto de transaccion 
        montoTransaccion = roundUp(montoTransaccion);
        /* ========== PASO 1 ============= */
        numeroFactura = addVerhoeffDigit(numeroFactura, 2);
        nitci = addVerhoeffDigit(nitci, 2);
        fechaTransaccion = addVerhoeffDigit(fechaTransaccion, 2);
        montoTransaccion = addVerhoeffDigit(montoTransaccion, 2);
        //se suman todos los valores obtenidos                  
        Int64 sumOfVariables = 0;
        try
        {
            sumOfVariables = Int64.Parse(numeroFactura)
                              + Int64.Parse(nitci)
                              + Int64.Parse(fechaTransaccion)
                              + Int64.Parse(montoTransaccion);
        }
        catch (FormatException e)
        {
            if (e.Source != null)
                Console.WriteLine("FormatException source: {0}", e.Source);
            throw;
        }

        //A la suma total se añade 5 digitos Verhoeff
        String sumOfVariables5Verhoeff = addVerhoeffDigit(sumOfVariables.ToString(), 5);

        /* ========== PASO 2 ============= */
        String fiveDigitsVerhoeff = sumOfVariables5Verhoeff.Substring(sumOfVariables5Verhoeff.Length - 5);
        int[] numbers = new int[5];
        for (int i = 0; i < 5; i++)
        {
            numbers[i] = Int32.Parse(fiveDigitsVerhoeff[i].ToString()) + 1;
        }

        String string1 = llavedosificacion.Substring(0, numbers[0]);
        String string2 = llavedosificacion.Substring(numbers[0], numbers[1]);
        String string3 = llavedosificacion.Substring(numbers[0] + numbers[1], numbers[2]);
        String string4 = llavedosificacion.Substring(numbers[0] + numbers[1] + numbers[2], numbers[3]);
        String string5 = llavedosificacion.Substring(numbers[0] + numbers[1] + numbers[2] + numbers[3], numbers[4]);

        String authorizationNumberDKey = numeroAutorizacion + string1;
        String invoiceNumberdKey = numeroFactura + string2;
        String NITCIDKey = nitci + string3;
        String dateOfTransactionDKey = fechaTransaccion + string4;
        String transactionAmountDKey = montoTransaccion + string5;

        /* ========== PASO 3 ============= */
        //se concatena cadenas de paso 2
        String stringDKey = authorizationNumberDKey + invoiceNumberdKey + NITCIDKey + dateOfTransactionDKey + transactionAmountDKey;
        //Llave para cifrado + 5 digitos Verhoeff generado en paso 2
        String keyForEncryption = llavedosificacion + fiveDigitsVerhoeff;
        //se aplica AllegedRC4
        String allegedRC4String = AllegedRC4.encryptMessageRC4(stringDKey, keyForEncryption, true);

        /* ========== PASO 4 ============= */
        //se suman valores ascii
        int totalAmount = 0;
        int sp1 = 0;
        int sp2 = 0;
        int sp3 = 0;
        int sp4 = 0;
        int sp5 = 0;
        byte[] asciiBytes = Encoding.ASCII.GetBytes(allegedRC4String);
        int tmp = 1;
        for (int i = 0; i < asciiBytes.Length; i++)
        {
            totalAmount += asciiBytes[i];
            switch (tmp)
            {
                case 1: sp1 += asciiBytes[i]; break;
                case 2: sp2 += asciiBytes[i]; break;
                case 3: sp3 += asciiBytes[i]; break;
                case 4: sp4 += asciiBytes[i]; break;
                case 5: sp5 += asciiBytes[i]; break;
            }
            tmp = (tmp < 5) ? tmp + 1 : 1;
        }

        /* ========== PASO 5 ============= */
        //suma total * sumas parciales dividido entre resultados obtenidos 
        //entre el dígito Verhoeff correspondiente más 1 (paso 2)
        int tmp1 = totalAmount * sp1 / numbers[0];
        int tmp2 = totalAmount * sp2 / numbers[1];
        int tmp3 = totalAmount * sp3 / numbers[2];
        int tmp4 = totalAmount * sp4 / numbers[3];
        int tmp5 = totalAmount * sp5 / numbers[4];
        //se suman todos los resultados
        int sumProduct = tmp1 + tmp2 + tmp3 + tmp4 + tmp5;
        //se obtiene base64
        String base64SIN = Base64SIN.convertBase64(sumProduct);

        /* ========== PASO 6 ============= */
        //Aplicar el AllegedRC4 a la anterior expresión obtenida
        return AllegedRC4.encryptMessageRC4(base64SIN, String.Concat(llavedosificacion, fiveDigitsVerhoeff), false);
    }

    /// <summary>
    /// Añade N digitos Verhoeff a una cadena de texto
    /// <param name="value">cadena donde se añadiran digitos Verhoeff</param>        
    /// <param name="max">cantidad de digitos a agregar</param>        
    /// <returns>cadena original + N digitos Verhoeff</returns>
    /// </summary>
    private static String addVerhoeffDigit(String value, int max)
    {
        for (int i = 1; i <= max; i++)
        {
            value = String.Concat(value, Verhoeff.generateVerhoeff(value));
        }
        return value;
    }
    private static String roundUp(String value)
    {
        char a = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
        value = (a == ',') ? value.Replace(".", ",") : (a == '.') ? value.Replace(",", ".") : value;
        decimal decimalVal = System.Convert.ToDecimal(value);
        //redondea a 0 decimales                                    
        return Math.Round(decimalVal, MidpointRounding.AwayFromZero).ToString();
    }
}