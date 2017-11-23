namespace SourceMeta
{
    public class ViewMetaObject : AbstractDatabaseMetaObject
    {
        public ViewMetaObject(IDataSourceMetadata source) : base(source)
        {
            this._type = "view";
        }

        protected override void getChildren()
        {
            MetaObject mo = new ColumnsMetaObject(this.source);
            mo.parent = this;
            this._children.Add(mo);
        }

    }
}
