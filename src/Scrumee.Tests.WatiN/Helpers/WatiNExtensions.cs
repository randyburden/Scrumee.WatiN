using System;
using WatiN.Core;
using WatiN.Core.Constraints;

namespace Scrumee.Tests.WatiN.Helpers
{
    public static class WatiNExtensions
    {
        public static bool TypeFast = false;

        /// <summary>
        /// Types text into a textfield
        /// </summary>
        /// <remarks>
        /// This extension method will either call the TypeText method
        /// or SetAttributeValue depending on the value of the TypeFast variable.
        /// To speed up the tests, set TypeFast to true.
        /// 
        /// If you need WatiN to actually fire the KeyPressDown and KeyPressUp events
        /// on the webpage, then you will want to set TypeFast to FALSE.
        /// </remarks>
        /// <param name="textField">An instance of a TextField</param>
        /// <param name="text">Text to type</param>
        public static void Type( this TextField textField, string text )
        {
            if ( TypeFast )
            {
                textField.SetAttributeValue( "value", text );
            }
            else
            {
                textField.TypeText( text );
            }
        }

        /// <summary>
        /// Determines if the element is visible by verifying that the Display
        /// style attribute is not set to none on this Element and all parent elements.
        /// </summary>
        /// <param name="element">A WatiN element</param>
        /// <returns>A Boolean value</returns>
        public static bool IsVisible( this Element element)
        {
            Element e = element;
            
            do
            {
                //Console.WriteLine( e.TagName );

                if ( e.Style.Display.ToLower().Contains( "none" ) )
                    return false;

                e = e.Parent;

            } while ( e.TagName.ToLower().StartsWith( "body" ) == false );

            return true;
        }
    }

    //[AttributeUsage( AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true )]
    //public class FindByAttribute : ComponentFinderAttribute
    //{
    //    private string _name;
    //    private string _value;

    //    public FindByAttribute( string name, string value )
    //    {
    //        _name = name;
    //        _value = value;
    //    }
        
    //    public override Component FindComponent( Type componentType, IElementContainer container )
    //    {
    //        return ComponentFinder( )
    //    }

    //    public static Component FindComponent(Type componentType, IElementContainer container, Constraint constraint)
    //    {
    //            if (componentType == typeof(Element))	                return FindUntypedElement(container, constraint);
	
    //            if (componentType.IsSubclassOf(typeof(Element)))
    //                return (Element) FindElementMethod.MakeGenericMethod(componentType).Invoke(null, new object[] { container, constraint });
	
    //            if (componentType.IsSubclassOf(typeof(Control)))
    //                return (Control) FindControlMethod.MakeGenericMethod(componentType).Invoke(null, new object[] { container, constraint });
	
    //            throw new NotSupportedException(string.Format("WatiN does not know how to find a component of type '{0}'.", componentType));
    //        }
    //}
}
