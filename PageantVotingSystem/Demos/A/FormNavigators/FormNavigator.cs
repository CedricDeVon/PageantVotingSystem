using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PageantVotingSystem.Source.FormNavigators
{
    public class FormNavigator
    {
        private readonly static Dictionary<string, Form> forms = new Dictionary<string, Form>();

        private readonly static Stack<Form> history = new Stack<Form>();

        public static void Start(string formName, string backgroundForm = "")
        {
            if (string.IsNullOrEmpty(formName) || !forms.ContainsKey(formName))
            {
                throw new Exception($"Form '{formName}' does not exist");
            }

            if (!string.IsNullOrEmpty(backgroundForm))
            {
                forms[backgroundForm].Show();
            }
            Form form = forms[formName];
            history.Push(form);
            Application.Run(form);
        }

        public static void Stop()
        {
            foreach (Form form in forms.Values)
            {
                form.Close();
            }
        }

        public static void Next(string formName)
        {
            if (string.IsNullOrEmpty(formName) || !forms.ContainsKey(formName))
            {
                throw new Exception($"Form '{formName}' does not exist");
            }

            Form form = forms[formName];
            form.Show();
            history.Peek().Hide();
            history.Push(form);
        }

        public static void Previous()
        {
            if (forms.Count == 1)
            {
                return;
            }

            Form formA = history.Pop();
            Form formB = history.Peek();
            formB.Show();
            formA.Hide();
        }

        public static void Add(Form form)
        {
            if (form == null)
            {
                throw new Exception($"Form cannot be null");
            }
            else if (forms.ContainsKey(form.Name))
            {
                throw new Exception($"Form '{form.Name}' already exists");
            }

            forms.Add(form.Name, form);
        }

        public static Form Get(string formName)
        {
            if (string.IsNullOrEmpty(formName) || !forms.ContainsKey(formName))
            {
                throw new Exception($"Form '{formName}' does not exist");
            }

            return forms[formName];
        }
    }
}
