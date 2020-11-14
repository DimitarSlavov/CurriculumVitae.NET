window.fileSaveAs = (filePath, filename) =>
{
	let link = document.createElement('a');
	link.classList.add('hidden');
	link.target = "_blank";
	link.download = filename;
	link.href = filePath + "/" + filename;
	document.body.appendChild(link);
	link.click();
	document.body.removeChild(link);
};