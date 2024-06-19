using System.Diagnostics;

namespace _253504_Patrebka_23;

public partial class Calculator : ContentPage
{
	private short condition = 1; // -2–OnOperatorSelection -1–OnCalculate 0–TypingOverlays, results used 1-Initial, onClear, entering 1st operand 2– entering 2nd operand
	private short conditionPrev = 1; 
	private double firstOperand = 0;
	private double secondOperand = 0;
	private string operatorName = "+";

	public Calculator()
	{
		InitializeComponent();
	}

	private double doCalc(double fstOperand, double sndOperand, string oprtr){
		switch(oprtr){
			case "+":
				return fstOperand+sndOperand;
			case "*":
				return fstOperand*sndOperand;
			case "/":
				return fstOperand/sndOperand;
			case "-":
				return fstOperand-sndOperand;
			default:
				return double.NaN;
		}
	}

	private void OnClicked(object sender, EventArgs e)
	{
		Button button = (Button) sender;
		string btnPressed = button.Text;
		string tmp;

		switch(btnPressed){
			case "Bksp":
				if(this.result.Text == "0")
					return;
				if(this.result.Text.Length == 1){
					this.result.Text = "0";
					return;
				}
				this.result.Text = this.result.Text.Remove(this.result.Text.Length-1);
				break;
			case "+":
				operatorName="+";
				condition=-2;
				firstOperand=Convert.ToDouble(this.result.Text);
				break;
			case "-":
				operatorName="-";
				condition=-2;
				firstOperand=Convert.ToDouble(this.result.Text);
				break;
			case "*":
				operatorName="*";
				condition=-2;
				firstOperand=Convert.ToDouble(this.result.Text);
				break;
			case "/":
				operatorName="/";
				condition=-2;
				firstOperand=Convert.ToDouble(this.result.Text);
				break;
			case "sqrt(x)":
				tmp = this.result.Text;
				this.previous.Text = "sqrt(" + tmp + ")";
				this.result.Text = Math.Sqrt(Convert.ToDouble(this.result.Text)).ToString();
				conditionPrev = condition;
				condition = 0;
				break;
			case "x^2":
				tmp = this.result.Text;
				this.previous.Text = tmp + "^2";
				this.result.Text = (Convert.ToDouble(this.result.Text)*Convert.ToDouble(this.result.Text)).ToString();
				conditionPrev = condition;
				condition = 0;
				break;
			case "%":
				Debug.WriteLine("Condition entering calculation sign is {0}", condition);
				tmp = this.result.Text;
				if(condition==2){
					this.previous.Text = firstOperand.ToString() + "*" +tmp + "%";
					this.result.Text = (firstOperand*Convert.ToDouble(this.result.Text)*0.01).ToString();
				}
				else{
					this.previous.Text = tmp + "%";
					this.result.Text = (Convert.ToDouble(this.result.Text)*0.01).ToString();
				}
				conditionPrev = condition;
				condition = 0;
				break;
			case "1/x":
				tmp = this.result.Text;
				this.previous.Text = "1/" + tmp;
				this.result.Text = (1/Convert.ToDouble(this.result.Text)).ToString();
				conditionPrev = condition;
				condition = 0;
				break;
			case "10^x":
				tmp = this.result.Text;
				this.previous.Text = "10^" + tmp;
				this.result.Text = Math.Pow(10, Convert.ToDouble(this.result.Text)).ToString();
				conditionPrev = condition;
				condition = 0;
				break;
			case "CE":
				this.result.Text = "0";
				break;
			case "C":
				this.result.Text = "0";
				this.previous.Text = string.Empty;
				firstOperand = 0;
				secondOperand = 0;
				condition = 1;
				break;
			case ",":
				if(condition==0){
					this.result.Text = "0";
						if(conditionPrev > 0)
							condition=conditionPrev;
						else
							condition=Convert.ToInt16(-1*conditionPrev);
				}
				else{
					if(this.result.Text.Contains(","))
						return;
					this.result.Text += ",";
				}
				break;
			case "=":
				Debug.WriteLine("Condition entering calculation sign is {0}", condition);
				if(condition==1){
					this.previous.Text = Convert.ToDouble(this.result.Text).ToString() + "=";
					this.result.Text = Convert.ToDouble(this.result.Text).ToString();
				}
				else if(condition==-2){
					condition=-1;
					secondOperand = Convert.ToDouble(this.result.Text);
					this.previous.Text = firstOperand.ToString() + operatorName + secondOperand.ToString() + "=";
					firstOperand = doCalc(firstOperand, firstOperand, operatorName);
					this.result.Text = firstOperand.ToString();
				}
				else if(condition == -1){
					firstOperand = Convert.ToDouble(this.result.Text);
					this.previous.Text = firstOperand.ToString() + operatorName + secondOperand.ToString() + "=";
					firstOperand = doCalc(firstOperand, secondOperand, operatorName);
					this.result.Text = firstOperand.ToString();
				}
				else{
					secondOperand = Convert.ToDouble(this.result.Text);
					this.previous.Text = firstOperand.ToString() + operatorName + secondOperand.ToString() + "=";
					firstOperand = doCalc(firstOperand, secondOperand, operatorName);
					this.result.Text = firstOperand.ToString();
					condition = -1;
				}
				break;
			default:
				if(this.result.Text == "0" || condition <= 0){
				this.result.Text = string.Empty;
					if(condition < 0)
						condition *= -1;
					if(condition==0){
						if(conditionPrev > 0)
							condition=conditionPrev;
						else
							condition=Convert.ToInt16(-1*conditionPrev);
					}
				}
				this.result.Text += btnPressed;
				break;
		}
		

	}
}