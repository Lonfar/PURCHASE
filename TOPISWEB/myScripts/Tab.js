function Tab_OnMouseOver(eventTarget)
{
	if(eventTarget.className != "tab hover selected")
	{
		eventTarget.className = "tab hover"
	}
}
function Tab_OnMouseOut(eventTarget)
{
	if(eventTarget.className != "tab hover selected")
	{
		eventTarget.className = "tab"
	}
}
function Tab_OnSelectServerClick(eventTarget,TabPageID)
{
		eventTarget.parentElement.parentElement.children[0].value = TabPageID;
}
function Tab_OnSelectClientClick(eventTarget,TabPageID)
{
		RootTabDiv = eventTarget.parentElement.parentElement.children[1];
		RootTabPageDiv = eventTarget.parentElement.parentElement.children[2];
		for(i=0;i<RootTabDiv.children.length;i++)
		{
			RootTabDiv.children[i].className = "tab";
			RootTabDiv.children[i].children[0].className = "background_normal";
		}
		for(i=0;i<RootTabPageDiv.children.length;i++)
		{
			RootTabPageDiv.children[i].style.display = "none";
		}

		document.getElementById(TabPageID).style.display = 'block';
		document.getElementById(TabPageID+"_H2").className = 'tab hover selected';
		document.getElementById(TabPageID+"_ZHB").className = 'background_selected';
		eventTarget.parentElement.parentElement.children[0].value = TabPageID;
}
