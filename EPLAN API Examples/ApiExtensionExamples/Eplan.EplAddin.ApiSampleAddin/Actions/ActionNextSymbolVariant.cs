﻿using Eplan.EplAddin.ApiSampleAddin.Helpers;
using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using System;
using System.Linq;

namespace Eplan.EplAddin.ApiSampleAddin.Actions
{
    /// <summary>
    /// This class implements a EPLAN action.  The Action will register the Addins in that  <seealso cref="IEplAddIn.OnRegister"/> Registerst.
    /// <seealso cref="Eplan::EplApi::ApplicationFramework::IEplAction"/> 
    /// </summary>
    public class ActionNextSymbolVariant : IEplAction, IEplActionEnable
    {
        /// <summary>
        /// Execution of the Action.  
        /// </summary>
        /// <returns>True:  Execution of the Action was successful</returns>
        public bool Execute(ActionCallingContext ctx)
        {
            try
            {
                SelectionSet selection = new SelectionSet();

                foreach (var function in selection.Selection.OfType<Function>())
                {
                    function.SymbolVariant = SymbolVariantHelper.GetNext(function.SymbolVariant);
                }

                ApiExtHelpers.RefreshDrawing();
            }
            catch (Exception ex)
            {
                MessageDisplayHelper.Show(string.Format("Symbol Change Error{1}error=[{0}]", ex.Message, Environment.NewLine), "ActionNextSymbolVariant", EnumDecisionIcon.eEXCLAMATION);
            }

            return true;
        }
        /// <summary>
        /// Function is called through the ApplicationFramework
        /// </summary>
        /// <param name="Name">Under this name, this Action in the system is registered</param>
        /// <param name="Ordinal">With this overload priority, this Action is registered</param>
        /// <returns>true: the return parameters are valid</returns>
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = "ActionNextSymbolVariant";
            Ordinal = 20;
            return true;
        }

        /// <summary>
        /// Documentation function for the Action; is called of the system as required 
        /// Bescheibungstext delivers for the Action itself and if the Action String-parameters ("Kommandozeile")
        /// also name and description of the single parameters evaluates
        /// </summary>
        /// <param name="actionProperties"> This object must be filled with the information of the Action.</param>
        public void GetActionProperties(ref ActionProperties actionProperties)
        {
            // Description 1st parameter
            // ActionParameterProperties firstParam= new ActionParameterProperties();
            // firstParam.set("Param1", "1. Parameter for ActionNextSymbolVariant"); 
            // actionProperties.addParameter(firstParam);

            // Description 2nd parameter
            // ActionParameterProperties firstParam= new ActionParameterProperties();
            // firstParam.set("Param2", "2. Parameter for ActionNextSymbolVariant"); 
            // actionProperties.addParameter(firstParam);
        }

        #region IEplActionEnable Implementations

        public bool Enabled(string strActionName, ActionCallingContext actionContext)
        {
            SelectionSet selection = new SelectionSet();

            return selection.Selection.OfType<Function>().Count() > 0;
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
