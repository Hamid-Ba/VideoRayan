﻿using System;
using Framework.Application;

namespace VideoRayan.Application.Contract.MeetingAgg.Contracts
{
	public interface ICategoryApplication
	{
		Task<CategoryDto> GetBy(Guid id);
		Task<IEnumerable<CategoryDto>> GetAll();
		Task<EditCategoryDto> GetDetailForEditBy(Guid id);
		Task<OperationResult> Edit(EditCategoryDto command);
		Task<OperationResult> Create(CreateCategoryDto command);
	}
}