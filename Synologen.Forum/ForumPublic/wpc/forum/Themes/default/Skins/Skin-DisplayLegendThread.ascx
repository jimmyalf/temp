<%@ Control Language="C#" %>
<%@ Import Namespace="Spinit.Wpc.Forum" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Controls" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Components" %>
<!-- ********* Skin-DisplayLegendThread.ascx:Start ************* //-->  
<table width="200" cellpadding="0" cellspacing="0">
	<tr>
		<td>
			<table class="tableBorder" width="100%" cellspacing="1" cellpadding="3">
				<tr>
					<td width="100%" class="column"><%=ResourceManager.GetString("ViewThreads_Legend") %>&nbsp;</td>
				</tr>				
				<tr>
					<td class="fh">
						<table width="100%" cellspacing="0" border="0" cellpadding="0">
							<tr>    
								<td align="left">
									<table width="100%" cellpadding="0" cellspacing="0">
										<tr>
											<td width="*" nowrap align="right" valign="middle" class="txt4">
												&nbsp;<%=ResourceManager.GetString("ViewThreads_Legend_AnnouncementsStickies") %>&nbsp;
											</td>
											<td width="10" align="left" valign="middle">
												<%= Formatter.StatusIcon( 2 ) %>
											</td>	
											<td width="10" align="left" valign="middle">
												<%= Formatter.StatusIcon( 1 ) %>
											</td>	
											<td width="10" align="left" valign="middle">
												<%= Formatter.StatusIcon( 6 ) %>
											</td>	
											<td width="10" align="left" valign="middle">
												<%= Formatter.StatusIcon( 5 ) %>
											</td>																																												
										</tr>
										<tr>
											<td width="*" nowrap align="right" valign="middle" class="txt4">
												&nbsp;<%=ResourceManager.GetString("ViewThreads_Legend_PopularTopics") %>&nbsp;
											</td>
											<td width="10" align="left" valign="middle">
												<%= Formatter.StatusIcon( 10 ) %>
											</td>	
											<td width="10" align="left" valign="middle">
												<%= Formatter.StatusIcon( 9 ) %>
											</td>	
											<td width="10" align="left" valign="middle">
												<%= Formatter.StatusIcon( 4 ) %>
											</td>	
											<td width="10" align="left" valign="middle">
												<%= Formatter.StatusIcon( 3 ) %>
											</td>																																												
										</tr>
										<tr>
											<td width="*" nowrap align="right" valign="middle" class="txt4">
												&nbsp;<%=ResourceManager.GetString("ViewThreads_Legend_NormalTopics") %>&nbsp;
											</td>
											<td width="10" align="left" valign="middle">
												<%= Formatter.StatusIcon( 12 ) %>
											</td>	
											<td width="10" align="left" valign="middle">
												<%= Formatter.StatusIcon( 11 ) %>
											</td>	
											<td width="10" align="left" valign="middle">
												<%= Formatter.StatusIcon( 8 ) %>
											</td>	
											<td width="10" align="left" valign="middle">
												<%= Formatter.StatusIcon( 7 ) %>
											</td>																																												
										</tr>										
									</table>
								</td>
								<td width="1"><img height="85" width="1" src="<%# Globals.GetSkinPath() + "/images/spacer.gif"%>"></td>								
							</tr>
						</table>    
					</td>    
				</tr>
			</table>
		</td>    
    </tr>
</table>
<!-- ********* Skin-DisplayLegendThread.ascx:End ************* //-->  