
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace PageantVotingSystem.Sources.FormNavigators
{
    public class FormNavigator
    {
        protected readonly static Dictionary<string, Form> forms = new Dictionary<string, Form>();

        protected readonly static Stack<Form> history = new Stack<Form>();

        public static void BeginDisplayingForm(string foregroundFormName)
        {
            ThrowIfFormDoesNotExist(foregroundFormName);

            Form form = forms[foregroundFormName];
            history.Push(form);
            Application.Run(form);
        }

        public static void StopDisplay()
        {
            foreach (Form form in forms.Values)
            {
                form.Close();
            }
            Application.Exit();
        }

        public static void DisplayNextForm(string formName)
        {
            ThrowIfFormDoesNotExist(formName);

            Form form = forms[formName];
            form.Show();
            history.Peek().Hide();
            history.Push(form);
        }

        public static void DisplayNextForm(Form form)
        {
            ThrowIfFormIsNull(form);

            form.Show();
            history.Peek().Hide();
            history.Push(form);
        }

        public static void DisplayPreviousForm()
        {
            if (history.Count == 1)
            {
                return;
            }

            Form formA = history.Pop();
            Form formB = history.Peek();
            formB.Show();
            formA.Hide();
        }

        public static void AddForm(Form form)
        {
            ThrowIfFormIsNull(form);
            ThrowIfFormAlreadyExists(form.Name);

            forms.Add(form.Name, form);
        }

        public static bool IsFormFound(string formName)
        {
            return forms.ContainsKey(formName);
        }

        public static bool IsFormNotFound(string formName)
        {
            return !IsFormFound(formName);
        }

        public static Form GetForm(string formName)
        {
            ThrowIfFormDoesNotExist(formName);

            return forms[formName];
        }

        protected static void ThrowIfOneFormIsNull(List<Form> forms)
        {
            if (forms == null || forms.Count == 0)
            {
                throw new Exception("'FormNavigator' - Form list cannot be null or empty");
            }
        }

        protected static void ThrowIfFormDoesNotExist(string formName)
        {
            if (!forms.ContainsKey(formName))
            {
                throw new Exception($"'FormNavigator' - Form '{formName}' does not exist");
            }
        }

        protected static void ThrowIfFormIsNull(Form form)
        {
            if (form == null)
            {
                throw new Exception($"'FormNavigator' - Form cannot be null");
            }
        }

        protected static void ThrowIfFormAlreadyExists(string formName)
        {
            if (forms.ContainsKey(formName))
            {
                throw new Exception($"'FormNavigator' - Form '{formName}' already exists");
            }
        }
    }
}
