﻿namespace BootstrapMvc.Forms
{
    using System;
    using BootstrapMvc.Core;

    public class Form<TModel>: ContentElement<FormContent<TModel>>, ILink, IForm
    {
        private GridSize labelWidth;

        public Form()
        {
            LabelWidth = FormDefaults.HorizontalLabelWidth;
            Type = FormDefaults.Type;
        }

        public SubmitMethod Method { get; set; } = SubmitMethod.Post;

        public FormEnctype Enctype { get; set; }

        public FormType Type { get; set; }

        public string Href { get; set; }

        public GridSize LabelWidth
        {
            get
            {
                return labelWidth;
            }

            set
            {
                labelWidth = value;
                ControlsWidth = labelWidth.Invert();
            }
        }

        public GridSize ControlsWidth { get; set; }

        protected override FormContent<TModel> CreateContentContext(IBootstrapContext context)
        {
            return new FormContent<TModel>((IBootstrapContext<TModel>)context, this);
        }

        protected override void WriteSelfStart(System.IO.TextWriter writer)
        {
            var tb = Helper.CreateTagBuilder("form");
            if (Type != FormType.DefaultNone)
            {
                tb.AddCssClass(Type.ToCssClass());
            }

            if (Method != SubmitMethod.Get)
            {
                tb.MergeAttribute("method", Method.ToString().ToLowerInvariant(), true);
            }

            if (Enctype != FormEnctype.NoValue)
            {
                tb.MergeAttribute("enctype", Enctype.ToEnctype(), true);
            }

            if (!string.IsNullOrEmpty(Href))
            {
                tb.MergeAttribute("action", Href, true);
            }

            ApplyCss(tb);
            ApplyAttributes(tb);

            tb.WriteStartTag(writer);
        }

        protected override void WriteSelfEnd(System.IO.TextWriter writer)
        {
            writer.Write("</form>");
        }
    }
}
