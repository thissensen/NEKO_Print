using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 翻译姬;
public class DataGridViewComboBoxColumnEx : DataGridViewColumn {

    public DataGridViewComboBoxColumnEx() : base(new DataGridViewComboBoxCellEx()) {
    }

    public override DataGridViewCell CellTemplate {
        get {
            return base.CellTemplate;
        }
        set {
            if (value != null && !value.GetType().IsAssignableFrom(typeof(DataGridViewComboBoxCellEx))) {
                throw new InvalidCastException("Must be a CalendarCell");
            }
            base.CellTemplate = value;
        }
    }
}

public class DataGridViewComboBoxCellEx : DataGridViewComboBoxCell {

    public override void InitializeEditingControl(int rowIndex, object
        initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle) {
        base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

        DataGridViewComboBoxEditingControlEx ctl = DataGridView.EditingControl as DataGridViewComboBoxEditingControlEx;
        if (Value == null) {
            ctl.ComboBox.Text = DefaultNewRowValue.ToString();
        } else {
            ctl.ComboBox.Text = Value.ToString();
        }

    }

    public override Type EditType {
        get {
            return typeof(DataGridViewComboBoxEditingControlEx);
        }
    }

    public override Type ValueType {
        get {
            return typeof(string);
        }
    }

    public override object DefaultNewRowValue {
        get {
            return "";
        }
    }
}

//共用
public class DataGridViewComboBoxEditingControlEx : 自定义下拉编辑控件, IDataGridViewEditingControl {

    public DataGridViewComboBoxEditingControlEx() {
        ComboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
    }

    private void ComboBox_SelectedIndexChanged(object sender, EventArgs e) {
        valueChanged = true;
        this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
    }

    //单元格正在修改的值
    public object EditingControlFormattedValue {
        get {
            return ComboBox.Text;
        }
        set {
            ComboBox.Text = value.ToString();
        }
    }

    //单元格格式化的值
    public object GetEditingControlFormattedValue(
        DataGridViewDataErrorContexts context) {
        return EditingControlFormattedValue;
    }

    //更改样式使其于单元格一致
    public void ApplyCellStyleToEditingControl(
        DataGridViewCellStyle dataGridViewCellStyle) {

    }

    //其父行单元格索引
    public int EditingControlRowIndex {
        get {
            return rowIndex;
        }
        set {
            rowIndex = value;
        }
    }
    private int rowIndex;

    public bool EditingControlWantsInputKey(
        Keys key, bool dataGridViewWantsInputKey) {
        switch (key & Keys.KeyCode) {
            case Keys.Left:
            case Keys.Up:
            case Keys.Down:
            case Keys.Right:
            case Keys.Home:
            case Keys.End:
            case Keys.PageDown:
            case Keys.PageUp:
                return true;
            default:
                return !dataGridViewWantsInputKey;
        }
    }

    public void PrepareEditingControlForEdit(bool selectAll) {
        ComboBox.Focus();
    }

    public bool RepositionEditingControlOnValueChange {
        get {
            return false;
        }
    }

    public DataGridView EditingControlDataGridView {
        get {
            return dataGridView;
        }
        set {
            dataGridView = value;
        }
    }
    private DataGridView dataGridView;

    public bool EditingControlValueChanged {
        get {
            return valueChanged;
        }
        set {
            valueChanged = value;
        }
    }
    private bool valueChanged = false;

    public Cursor EditingPanelCursor {
        get {
            return base.Cursor;
        }
    }

}
