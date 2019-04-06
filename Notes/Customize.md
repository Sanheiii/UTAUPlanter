# 自定义音源信息
此程序理论上支持所有音源,你只需要修改Lyric.xml即可达到这一目的<br/>
如果修改该文件导致程序闪退，可能是因为你所使用的文本编辑器默认的文本编码不同<br/><br/>
在Lyric.xml中,有\<sentence\>标签和\<word\>标签，其中：<br/><br/>
\<word\>标签是音源中的单个字，内容是填词时应填入的字段，属性"title"是它实际的字符。<br/>
比如:<br/>
`<word title="我">wof</word>`<br/><br/>
\<sentence\>标签是素材中的原句,由若干个\<word\>组成，其属性"background"是它在程序中显示的背景色，使用十六进制颜色值表示<br/>
比如：<br/>
`<sentence background="#FF99CC">`<br/>
&nbsp;&nbsp;&nbsp;&nbsp;`<word title="我">wof</word>`<br/>
&nbsp;&nbsp;&nbsp;&nbsp;`<word title="要">yaof</word>`<br/>
&nbsp;&nbsp;&nbsp;&nbsp;`<word title="金">jinf</word>`<br/>
&nbsp;&nbsp;&nbsp;&nbsp;`<word title="坷">kef</word>`<br/>
&nbsp;&nbsp;&nbsp;&nbsp;`<word title="垃">laf</word>`<br/>
`</sentence>`<br/>
只要给word的值留空，就可以形成一个占位符，即便它不能点击，它也会以灰色的背景显示在它所在的句子里。