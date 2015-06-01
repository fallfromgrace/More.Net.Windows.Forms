using More.Net.Linq.Expressions;
using System;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace Light.Windows.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class ControlExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TControl"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <typeparam name="TDataSource"></typeparam>
        /// <param name="control"></param>
        /// <param name="controlPropertySelector"></param>
        /// <param name="dataSource"></param>
        /// <param name="dataSourcePropertySelector"></param>
        /// <returns></returns>
        public static Binding AddBinding<TControl, TProperty, TDataSource>(
            this TControl control,
            Expression<Func<TControl, TProperty>> controlPropertySelector,
            TDataSource dataSource,
            Expression<Func<TDataSource, TProperty>> dataSourcePropertySelector)
            where TControl : Control
        {
            String controlPropertyPath = controlPropertySelector.GetPropertyPath();
            String dataSourcePropertyPath = dataSourcePropertySelector.GetPropertyPath();
            return control.AddBinding(controlPropertyPath, dataSource, dataSourcePropertyPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TControl"></typeparam>
        /// <typeparam name="TControlProperty"></typeparam>
        /// <typeparam name="TDataSource"></typeparam>
        /// <typeparam name="TDataSourceProperty"></typeparam>
        /// <param name="control"></param>
        /// <param name="controlPropertySelector"></param>
        /// <param name="dataSource"></param>
        /// <param name="dataSourcePropertySelector"></param>
        /// <param name="convertToControlProperty"></param>
        /// <param name="convertToDataSourceProperty"></param>
        /// <returns></returns>
        public static Binding AddBinding<TControl, TControlProperty, TDataSource, TDataSourceProperty>(
            this TControl control,
            Expression<Func<TControl, TControlProperty>> controlPropertySelector,
            TDataSource dataSource,
            Expression<Func<TDataSource, TDataSourceProperty>> dataSourcePropertySelector,
            Func<TDataSourceProperty, TControlProperty> convertToControlProperty,
            Func<TControlProperty, TDataSourceProperty> convertToDataSourceProperty)
            where TControl : Control
        {
            String controlPropertyPath = controlPropertySelector.GetPropertyPath();
            String dataSourcePropertyPath = dataSourcePropertySelector.GetPropertyPath();
            return control.AddBinding(
                controlPropertyPath, 
                dataSource, 
                dataSourcePropertyPath, 
                convertToControlProperty, 
                convertToDataSourceProperty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TControl"></typeparam>
        /// <typeparam name="TDataSource"></typeparam>
        /// <param name="control"></param>
        /// <param name="controlPropertyPath"></param>
        /// <param name="dataSource"></param>
        /// <param name="dataSourcePropertyPath"></param>
        /// <returns></returns>
        private static Binding AddBinding<TControl, TDataSource>(
            this TControl control,
            String controlPropertyPath,
            TDataSource dataSource,
            String dataSourcePropertyPath)
            where TControl : Control
        {
            return control.DataBindings.Add(controlPropertyPath, dataSource, dataSourcePropertyPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TControl"></typeparam>
        /// <typeparam name="TControlProperty"></typeparam>
        /// <typeparam name="TDataSource"></typeparam>
        /// <typeparam name="TDataSourceProperty"></typeparam>
        /// <param name="control"></param>
        /// <param name="controlPropertyPath"></param>
        /// <param name="dataSource"></param>
        /// <param name="dataSourcePropertyPath"></param>
        /// <param name="convertToControlProperty"></param>
        /// <param name="convertToDataSourceProperty"></param>
        /// <returns></returns>
        private static Binding AddBinding<TControl, TControlProperty, TDataSource, TDataSourceProperty>(
            this TControl control,
            String controlPropertyPath,
            TDataSource dataSource,
            String dataSourcePropertyPath,
            Func<TDataSourceProperty, TControlProperty> convertToControlProperty,
            Func<TControlProperty, TDataSourceProperty> convertToDataSourceProperty)
            where TControl : Control
        {
            Binding binding = new Binding(controlPropertyPath, dataSource, dataSourcePropertyPath);
            binding.Format += (s, e) =>  e.Value = convertToControlProperty((TDataSourceProperty)e.Value);
            binding.Parse += (s, e) => e.Value = convertToDataSourceProperty((TControlProperty)e.Value);
            control.DataBindings.Add(binding);
            return binding;
        }
    }
}
